using MasterMealWA.Server.Data;
using MasterMealWA.Shared.Models;
using MasterMealWA.Server.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MasterMealWA.Shared.Enums;

namespace MasterMealWA.Server.Services
{
    public class ShoppingService : IShoppingService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMeasurementService _measurementService;
        private readonly IServingService _servingService;

        public ShoppingService(ApplicationDbContext context, IMeasurementService measurementService, IServingService servingService)
        {
            _context = context;
            _measurementService = measurementService;
            _servingService = servingService;
        }

        public async Task<ShoppingList> CreateShoppingListForDateRangeAsync(DateTime EndDate, DateTime StartDate, string userId)
        {
            //Get Meals that are in date range, including recipe, qingredients, ingredients
            List<Meal> meals = await _context.Meal.Include(m => m.Recipe)
                                                  .ThenInclude(r => r.Ingredients)
                                                  .ThenInclude(q => q.Ingredient)
                                                  .Where(m => m.Date >= StartDate && m.Date <= EndDate)
                                                  .AsNoTracking()
                                                  .ToListAsync();
            foreach (var meal in meals)
            {
                meal.Recipe.Ingredients = await GetScaledIngredientList(meal.RecipeId, meal.Serves);
            }
            ShoppingList list = CreateShoppingListFromMeals(meals);
            list.ChefId = userId;
            list.Name = $"Meals before {EndDate.ToShortDateString()}";
            list.Created = DateTime.Now;
            _context.Add(list);
            await _context.SaveChangesAsync();
            return list;
        }
        private async Task<ICollection<QIngredient>> GetScaledIngredientList(int recipeId, int desiredServings)
        {
            var result = await _servingService.ScaleRecipeAsync(recipeId, desiredServings);
            return result.Ingredients;
        }
        private ShoppingList CreateShoppingListFromMeals(List<Meal> meals)
        {
            List<QIngredient> qIngredients = new();
            foreach (var meal in meals)
            {
                qIngredients.AddRange(meal.Recipe.Ingredients);
            }
            List<ShoppingIngredient> list = CreateShoppingIngredientsFromQIngredients(qIngredients.OrderByDescending(q => q.IngredientId).ToList());
            ShoppingList dbList = new();
            dbList.ShoppingIngredients = list;
            return dbList;
        }

        private List<ShoppingIngredient> CreateShoppingIngredientsFromQIngredients(List<QIngredient> allIngredients)
        {
            //All ingredients that are the same Id need to be combined, and removed from the list until list is empty.
            var resultList = new List<ShoppingIngredient>();
            var uniqueList = allIngredients.Select(i => i.IngredientId).Distinct().ToList();
            foreach (var item in uniqueList)
            {
                List<QIngredient> thisIngredientList = allIngredients.Where(i => i.IngredientId == item).ToList();
                var shopIng = CreateOneShoppingIngredientFromMultipleQIngredients(thisIngredientList);
                resultList.Add(shopIng);
            }
            return resultList;
        }
        private ShoppingIngredient CreateOneShoppingIngredientFromMultipleQIngredients(List<QIngredient> listOfOneIngredient)
        {
            List<string> notes = new();
            int totalQuantity = 0;
            foreach (var qingredient in listOfOneIngredient)
            {
                totalQuantity += qingredient.NumberOfUnits;
                //If string has Notes (it always has shopping notes due to quantity)
                if (!string.IsNullOrWhiteSpace(qingredient.Notes))
                {
                    notes.Add($"{qingredient.ShoppingNotes} {qingredient.Recipe.Name}");
                }
            }

            Ingredient ingredient = listOfOneIngredient.First().Ingredient;
            ShoppingIngredient result = new()
            {
                IngredientTypeId = ingredient.TypeId,
                Notes = notes
            };
            var measure = ingredient.MeasurementType;
            if (measure == MeasurementType.Volume)
            {

                result.QuantityString = $"{_measurementService.DecodeVolumeMeasurement(totalQuantity)} {ingredient.Name}";
            }
            else if (measure == MeasurementType.Mass)
            {
                result.QuantityString = $"{_measurementService.DecodeVolumeMeasurement(totalQuantity)} {ingredient.Name}";
            }
            else if (measure == MeasurementType.Count)
            {
                result.QuantityString = $"{_measurementService.DecodeUnitMeasurement(totalQuantity)} {ingredient.Name}";
            }
            _context.Add(result);
            return result;
        }



    }
}
