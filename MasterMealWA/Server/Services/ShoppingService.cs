using MasterMealWA.Server.Data;
using MasterMealWA.Shared.Models;
using MasterMealWA.Server.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MasterMealWA.Server.Services
{
    public class ShoppingService : IShoppingService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMeasurementService _measurementService;

        public ShoppingService(ApplicationDbContext context, IMeasurementService measurementService)
        {
            _context = context;
            _measurementService = measurementService;
        }

        public Task<ShoppingList> CreateShoppingListForDateRangeAsync(DateTime EndDate, DateTime StartDate)
        {
            //Get Meals that are in date range, including recipe, qingredients, ingredients
            
            throw new NotImplementedException();
        }
        public Task<ShoppingList> CreateShoppingListFromMealsAsync(List<Meal> meals)
        {
            throw new NotImplementedException();
        }
        public List<QIngredient> CreateListOfQIngredientsForShopping(List<Meal> meals)
        {
            //First list is all qingredients from meal.Recipes
            List<QIngredient> qIngredients = new();
            foreach (var meal in meals)
            {
                foreach (var qIngredient in meal.Recipe.Ingredients)
                {
                    qIngredients.Add(qIngredient);
                }
            }
            //None are combined yet, to preserve the shopping notes for each Recipe
            //ordered to make it easier to parse in next step
            return qIngredients.OrderByDescending(q=>q.IngredientId).ToList();
        }

        public List<ShoppingIngredient> CreateShoppingIngredientsFromQIngredients(List<QIngredient> allIngredients)
        {
            //All ingredients that are the same Id need to be combined, and removed from the list until list is empty.  
            //For each unique IngredientId, make a new list.  
            //pass new list to method
            throw new NotImplementedException();
        }
        public ShoppingIngredient CreateOneShoppingIngredientFromMultipleQIngredients(List<QIngredient> listOfOneIngredient)
        {
            List<string> notes = new();
            int totalQuantity = 0; //TODO this needs to be changed to correct measurement
            foreach (var qingredient in listOfOneIngredient)
            {
                totalQuantity +=qingredient.NumberOfUnits;
                //If string has Notes (it always has shopping notes due to quantity)
                if (!string.IsNullOrWhiteSpace(qingredient.Notes) )
                {
                    notes.Add(qingredient.ShoppingNotes);
                }
            }
            ShoppingIngredient result = new()
            {
                Measurement =  _measurementService.DecodeVolumeMeasurement(totalQuantity),
                IngredientId = listOfOneIngredient.First().IngredientId,
                Notes = notes
            };

            return result;
        }



    }
}
