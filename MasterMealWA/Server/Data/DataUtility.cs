using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MasterMealWA.Shared.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System.IO;
using MasterMealWA.Shared.Enums;
using MasterMealWA.Server.Services;
using System.Diagnostics;
using MasterMealWA.Server.Data;
using MasterMealWA.Server.Models;

namespace MasterMealWA.Server.Data
{
    public static class DataUtility
    {
        public static async Task ManageDataAsync(IHost host)
        {

            using var svcScope = host.Services.CreateScope();
            var svcProvider = svcScope.ServiceProvider;
            //Service: An instance of DBContext
            var dbContextSvc = svcProvider.GetRequiredService<ApplicationDbContext>();
            //Service: An instance of RoleManager
            var roleManagerSvc = svcProvider.GetRequiredService<RoleManager<IdentityRole>>();
            //Service: An instance of the UserManager
            var userManagerSvc = svcProvider.GetRequiredService<UserManager<ApplicationUser>>();
            //TsTEP 1: This is the programmatic equivalent to Update-Database
            await dbContextSvc.Database.MigrateAsync();
            await SeedDefaultImagesAsync(dbContextSvc);
            await SeedRolesAsync(userManagerSvc, roleManagerSvc);
            await SeedAdminUserAsync(userManagerSvc, roleManagerSvc);
            await SeedModeratorUserAsync(userManagerSvc, roleManagerSvc);
            await SeedRegularUserAsync(userManagerSvc, roleManagerSvc);
            await SeedRecipeTypesAsync(dbContextSvc);
            await SeedIngredientTypesAsync(dbContextSvc);
            await SeedIngredientsAsync(dbContextSvc);
            await SeedSuppliesAsync(dbContextSvc);
            await SeedRecipesAsync(dbContextSvc, userManagerSvc);
        }
        private static async Task SeedAdminUserAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            //Seed Default Admin User
            var defaultUser = new ApplicationUser
            {
                UserName = "sethbcoding@gmail.com",
                DisplayName = "sethbcoding@gmail.com",
                Email = "sethbcoding@gmail.com",
                FirstName = "Seth",
                LastName = "Burleson",
                ScreenName = "Master Admin",
                EmailConfirmed = true,
                ImageId = 2,
            };
            try
            {
                var user = await userManager.FindByEmailAsync(defaultUser.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(defaultUser, "Abc&123!");
                    await userManager.AddToRoleAsync(defaultUser, UserRoles.Admin.ToString());
                    await userManager.AddToRoleAsync(defaultUser, UserRoles.Moderator.ToString());
                    await userManager.AddToRoleAsync(defaultUser, UserRoles.User.ToString());
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("*************  ERROR  *************");
                Debug.WriteLine("Error Seeding Default Admin User.");
                Debug.WriteLine(ex.Message);
                Debug.WriteLine("***********************************");
                throw;
            }
        }private static async Task SeedModeratorUserAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            //Seed Default Admin User
            var defaultUser = new ApplicationUser
            {
                UserName = "softballcc11@yahoo.com",
                DisplayName = "softballcc11@yahoo.com",
                Email = "softballcc11@yahoo.com",
                FirstName = "Cydney",
                LastName = "Burleson",
                ScreenName = "CydModerator",
                EmailConfirmed = true,
                ImageId = 2,
            };
            try
            {
                var user = await userManager.FindByEmailAsync(defaultUser.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(defaultUser, "Abc&123!");
                    await userManager.AddToRoleAsync(defaultUser, UserRoles.Moderator.ToString());
                    await userManager.AddToRoleAsync(defaultUser, UserRoles.User.ToString());
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("*************  ERROR  *************");
                Debug.WriteLine("Error Seeding Default Moderator User.");
                Debug.WriteLine(ex.Message);
                Debug.WriteLine("***********************************");
                throw;
            }
        }private static async Task SeedRegularUserAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            //Seed Default Admin User
            var defaultUser = new ApplicationUser
            {
                UserName = "sbrevolution5@aim.com",
                DisplayName = "sbrevolution5@aim.com",
                Email = "sbrevolution5@aim.com",
                FirstName = "Seth",
                LastName = "Burleson",
                ScreenName = "sbrevolution5",
                EmailConfirmed = true,
                ImageId = 2,
            };
            try
            {
                var user = await userManager.FindByEmailAsync(defaultUser.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(defaultUser, "Abc&123!");
                    await userManager.AddToRoleAsync(defaultUser, UserRoles.User.ToString());
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("*************  ERROR  *************");
                Debug.WriteLine("Error Seeding Default User.");
                Debug.WriteLine(ex.Message);
                Debug.WriteLine("***********************************");
                throw;
            }
        }
        public static async Task SeedRolesAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            //Seed Roles
            await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin.ToString()));
            await roleManager.CreateAsync(new IdentityRole(UserRoles.User.ToString()));
            await roleManager.CreateAsync(new IdentityRole(UserRoles.Moderator.ToString()));


        }
        private static async Task SeedDefaultImagesAsync(ApplicationDbContext context)
        {
            var mealImage = await context.DBImage.FirstOrDefaultAsync(i => i.Id == 1);
            if (mealImage == null)
            {
                var file = $"{Directory.GetCurrentDirectory()}/Assets/DefaultRecipe.jpg";
                var fileData = await File.ReadAllBytesAsync(file);
                var newImage = new DBImage()
                {
                    ImageData = fileData,
                    ContentType = "jpg",
                    Id = 1
                };
                context.Add(newImage);
                await context.SaveChangesAsync();
            }
            var userImage = await context.DBImage.FirstOrDefaultAsync(i => i.Id == 2);
            if (userImage == null)
            {
                var file = $"{Directory.GetCurrentDirectory()}/Assets/DefaultUser.png";
                var fileData = await File.ReadAllBytesAsync(file);
                var newImage = new DBImage()
                {
                    ImageData = fileData,
                    ContentType = "png",
                    Id = 2
                };
                context.Add(newImage);
                await context.SaveChangesAsync();
            }

        }
        private static async Task SeedIngredientTypesAsync(ApplicationDbContext context)
        {
            if ((await context.IngredientType.ToListAsync()).Count() < 1)
            {
                var types = new List<IngredientType>();
                types.Add(NewType("Dairy"));
                types.Add(NewType("Meat"));
                types.Add(NewType("Cold Products"));
                types.Add(NewType("Frozen Products"));
                types.Add(NewType("Produce"));
                types.Add(NewType("Bread"));
                types.Add(NewType("Spice"));
                types.Add(NewType("Breadcrumb"));
                types.Add(NewType("Oil"));
                types.Add(NewType("Baking Needs"));
                types.Add(NewType("Deli Meat"));
                types.Add(NewType("Condiments and Sauces"));
                types.Add(NewType("Cheese"));
                types.Add(NewType("Canned Good"));
                types.Add(NewType("Soup"));
                types.Add(NewType("Pasta"));
                await context.AddRangeAsync(types);
                await context.SaveChangesAsync();
            }

        }
        private static IngredientType NewType(string name)
        {
            return new IngredientType() { Name = name };
        }
        private static async Task SeedIngredientsAsync(ApplicationDbContext context)
        {
            if ((await context.Ingredient.ToListAsync()).Count() < 1)
            {
                var ingredients = new List<Ingredient>();
                var ingTypes = await context.IngredientType.ToListAsync();
                //Ancho BBQ Sloppy Joes Ingredients
                ingredients.Add(MakeIngredient("BBQ Sauce", "Condiments and Sauces", MeasurementType.Volume, context, ingTypes));
                ingredients.Add(MakeIngredient("Ketchup", "Condiments and Sauces", MeasurementType.Volume, context, ingTypes));
                ingredients.Add(MakeIngredient("Cornstarch", "Baking Needs", MeasurementType.Volume, context, ingTypes));
                ingredients.Add(MakeIngredient("Yellow Onion", "Produce", MeasurementType.Count, context, ingTypes));
                ingredients.Add(MakeIngredient("Ancho Chili Powder", "Spice", MeasurementType.Volume, context, ingTypes));
                ingredients.Add(MakeIngredient("Gold Potatoes", "Produce", MeasurementType.Volume, context, ingTypes));
                ingredients.Add(MakeIngredient("Dill Pickle", "Produce", MeasurementType.Count, context, ingTypes));
                ingredients.Add(MakeIngredient("Potato Buns", "Bread", MeasurementType.Count, context, ingTypes));
                ingredients.Add(MakeIngredient("Beef Stock Concentrate", "Soup", MeasurementType.Count, context, ingTypes));
                ingredients.Add(MakeIngredient("Ground Beef", "Meat", MeasurementType.Mass, context, ingTypes));
                ingredients.Add(MakeIngredient("Olive Oil", "Oil", MeasurementType.Volume, context, ingTypes));
                //Buffalo Spiced Crispy Chicken
                ingredients.Add(MakeIngredient("Scallions", "Produce", MeasurementType.Count, context, ingTypes));
                ingredients.Add(MakeIngredient("Gold Potatoes", "Produce", MeasurementType.Count, context, ingTypes));
                ingredients.Add(MakeIngredient("Chicken Breast", "Meat", MeasurementType.Mass, context, ingTypes));
                ingredients.Add(MakeIngredient("Sour Cream", "Cold Products", MeasurementType.Volume, context, ingTypes));
                ingredients.Add(MakeIngredient("Butter", "Dairy", MeasurementType.Volume, context, ingTypes));
                ingredients.Add(MakeIngredient("Montrey Jack (Shredded)", "Cheese", MeasurementType.Volume, context, ingTypes));
                ingredients.Add(MakeIngredient("Panko Breadcrumbs", "Breadcrumb", MeasurementType.Volume, context, ingTypes));
                ingredients.Add(MakeIngredient("Honey", "Condiments and Sauces", MeasurementType.Volume, context, ingTypes));
                ingredients.Add(MakeIngredient("Green Beans", "Produce", MeasurementType.Volume, context, ingTypes));
                ingredients.Add(MakeIngredient("Franks Seasoning Blend", "Spice", MeasurementType.Volume, context, ingTypes));
                //Beef and cheese tostadas
                ingredients.Add(MakeIngredient("Roma Tomato", "Produce", MeasurementType.Count, context, ingTypes));
                ingredients.Add(MakeIngredient("Green Bell Pepper", "Produce", MeasurementType.Count, context, ingTypes));
                ingredients.Add(MakeIngredient("Flour Tortillas", "Bread", MeasurementType.Count, context, ingTypes));
                ingredients.Add(MakeIngredient("Mexican Cheese (Shredded)", "Cheese", MeasurementType.Volume, context, ingTypes));
                ingredients.Add(MakeIngredient("Chili Powder", "Spice", MeasurementType.Volume, context, ingTypes));
                ingredients.Add(MakeIngredient("Southwest Spice Blend", "Spice", MeasurementType.Volume, context, ingTypes));
                ingredients.Add(MakeIngredient("Lime", "Produce", MeasurementType.Count, context, ingTypes));
                ingredients.Add(MakeIngredient("Hot Sauce", "Condiments and Sauces", MeasurementType.Volume, context, ingTypes));
                ingredients.Add(MakeIngredient("Cilantro", "Produce", MeasurementType.Volume, context, ingTypes));
                await context.AddRangeAsync(ingredients);
                await context.SaveChangesAsync();
            }
        }
        private static Ingredient MakeIngredient(string name, string type, MeasurementType measurement, ApplicationDbContext context, List<IngredientType> types)
        {
            var typeId = types.Where(t => t.Name == type).FirstOrDefault().Id;
            return new Ingredient() { Name = name, TypeId = typeId, MeasurementType = measurement };
        }
        private static async Task SeedSuppliesAsync(ApplicationDbContext context)
        {
            if ((await context.Supply.ToListAsync()).Count < 1)
            {
                var supplies = new List<Supply>();
                supplies.Add(NewSupply("Large Pan"));
                supplies.Add(NewSupply("Baking Sheet"));
                supplies.Add(NewSupply("Small Bowl"));
                supplies.Add(NewSupply("Medium Bowl"));
                supplies.Add(NewSupply("Olive Oil"));
                supplies.Add(NewSupply("Salt And Pepper"));
                supplies.Add(NewSupply("Strainer"));
                supplies.Add(NewSupply("Paper Towels"));
                supplies.Add(NewSupply("Large Bowl"));
                supplies.Add(NewSupply("Medium Pot"));
                supplies.Add(NewSupply("Potato Masher"));
                await context.AddRangeAsync(supplies);
                await context.SaveChangesAsync();
            }
        }

        private static Supply NewSupply(string v)
        {
            var sup = new Supply()
            {
                Name = v
            };
            return sup;
        }

        private static async Task SeedRecipesAsync(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            if ((await context.Recipe.ToListAsync()).Count < 1)
            {
                var types = await context.RecipeType.ToListAsync();
                var ing = await context.Ingredient.ToListAsync();
                var admin = await userManager.FindByEmailAsync("Sethbcoding@gmail.com");
                var sup = await context.Supply.ToListAsync();
                await SeedBeefTostadaAsync(context, types, ing, admin, sup);
                await SeeedAnchoBBQ(context, types, ing, admin, sup);
                await SeedBuffChickenAsync(context, types, ing, admin, sup);
            }
        }

        private static async Task SeedBeefTostadaAsync(ApplicationDbContext context, List<RecipeType> types, List<Ingredient> ing, ApplicationUser admin, List<Supply> sup)
        {
            #region BeefTostada
            var beefTostada = new Recipe()
            {
                Name = "Beef & Cheese Tostadas",
                Description = "With Green Bell Pepper, Tomato Salsa, & Hot Sauce Crema",
                RecipeSource = "HelloFresh",
                AuthorId = admin.Id,
                Servings = 2,
                CookingTime = 30,
                TypeId = types.Where(t => t.Name == "Mexican").First().Id,
                ImageId = 1
            };
            await context.AddAsync(beefTostada);
            await context.SaveChangesAsync();
            var beefIng = new List<QIngredient>();
            beefIng.Add(NewQIngredient("Roma Tomato", 1, ing, beefTostada.Id));
            beefIng.Add(NewQIngredient("Yellow Onion", 1, ing, beefTostada.Id));
            beefIng.Add(NewQIngredient("Green Bell Pepper", 1, ing, beefTostada.Id));
            beefIng.Add(NewQIngredient("Lime", 1, ing, beefTostada.Id));
            beefIng.Add(NewQIngredient("Beef Stock Concentrate", 1, ing, beefTostada.Id));
            beefIng.Add(NewQIngredient("Flour Tortillas", 6, ing, beefTostada.Id));
            beefIng.Add(NewQIngredient("Ground Beef", 10, Fraction.NoFraction, MassMeasurementUnit.ounce, ing, beefTostada.Id));
            beefIng.Add(NewQIngredient("Sour Cream", 4, Fraction.NoFraction, VolumeMeasurementUnit.Tablespoon, ing, beefTostada.Id));
            beefIng.Add(NewQIngredient("Mexican Cheese (Shredded)", 0, Fraction.Half, VolumeMeasurementUnit.Cup, ing, beefTostada.Id));
            beefIng.Add(NewQIngredient("Cilantro", 0, Fraction.Half, VolumeMeasurementUnit.Ounce, ing, beefTostada.Id));
            beefIng.Add(NewQIngredient("Hot Sauce", 1, Fraction.NoFraction, VolumeMeasurementUnit.Teaspoon, ing, beefTostada.Id));
            beefIng.Add(NewQIngredient("Chili Powder", 1, Fraction.NoFraction, VolumeMeasurementUnit.Teaspoon, ing, beefTostada.Id));
            beefIng.Add(NewQIngredient("Southwest Spice Blend", 1, Fraction.NoFraction, VolumeMeasurementUnit.Tablespoon, ing, beefTostada.Id));
            var beefSupplies = new List<QSupply>();
            beefSupplies.Add(await AddSupplyAsync("Large Pan", sup, beefTostada.Id, context));
            beefSupplies.Add(await AddSupplyAsync("Baking Sheet", sup, beefTostada.Id, context));
            beefSupplies.Add(await AddSupplyAsync("Small Bowl", sup, beefTostada.Id, context));
            beefSupplies.Add(await AddSupplyAsync("Medium Bowl", sup, beefTostada.Id, context));
            beefSupplies.Add(await AddSupplyAsync("Olive Oil", sup, beefTostada.Id, context));
            beefSupplies.Add(await AddSupplyAsync("Salt And Pepper", sup, beefTostada.Id, context));

            var beefSteps = new List<Step>();
            beefSteps.Add(AddStep("Preheat Oven to 450F.", 1, beefTostada.Id));
            beefSteps.Add(AddStep("Dice tomato. Roughly chop cilantro. Halve and peel onion; thinly slice one half. Finely chop remaining onion until you have 2 TBS. Quarter lime. Halve, core, and thinly slice bell pepper into strips.", 2, beefTostada.Id));
            beefSteps.Add(AddStep("In a medium bowl, combine tomato, cilantro, chopped onion, juice from half the lime, and a pinch of salt and pepper.", 3, beefTostada.Id));
            beefSteps.Add(AddStep("In a small bowl, combine sour cream with as much hot sauce as you like.Stir in water 1 tsp.at a time until mixture reaches a drizzling consistency.Season with salt.", 4, beefTostada.Id));
            beefSteps.Add(AddStep("Heat a drizzle of olive oil in a large pan over medium - high heat.Add beef, Southwest Spice, chili powder, and a few big pinches of salt.Cook, breaking up meat into pieces, until browned, 4 - 5 minutes.", 5, beefTostada.Id));
            beefSteps.Add(AddStep("Once beef is browned, add bell pepper, sliced onion, and a pinch of salt to pan.Cook, stirring, until veggies are tender and beef is cooked through, 5 - 7 minutes. ", 6, beefTostada.Id));
            beefSteps.Add(AddStep("Add stock concentrate and ¼ cup water.Simmer until thickened, 1 - 2 minutes.Season with salt; remove pan from heat and set aside.", 7, beefTostada.Id));
            beefSteps.Add(AddStep("Drizzle tortillas with 1 TBS olive oil; brush or rub to coat all over.Arrange on a baking sheet.Gently prick each tortilla in a few places with a fork.", 8, beefTostada.Id));
            beefSteps.Add(AddStep("Bake on top rack, flipping halfway through, until lightly golden, 4 - 5 minutes per side.", 9, beefTostada.Id));
            beefSteps.Add(AddStep("Serve and top with pico de gallo and lime crema", 10, beefTostada.Id));
            beefTostada.Supplies =beefSupplies;
            await context.AddRangeAsync(beefSteps);
            await context.AddRangeAsync(beefIng);
            await context.SaveChangesAsync();
            #endregion
        }

        private static async Task SeeedAnchoBBQ(ApplicationDbContext context, List<RecipeType> types, List<Ingredient> ing, ApplicationUser admin, List<Supply> sup)
        {
            #region AnchoBBQ
            var anchoBBQ = new Recipe()
            {
                Name = "Ancho BBQ Sloppy Joes",
                Description = "With Pickle Slices & Oven Gold Potatoes",
                RecipeSource = "HelloFresh",
                AuthorId = admin.Id,
                Servings = 2,
                CookingTime = 30,
                TypeId = types.Where(t => t.Name == "American").FirstOrDefault().Id,
                ImageId = 1
            };
            await context.AddAsync(anchoBBQ);
            await context.SaveChangesAsync();

            var anchIng = new List<QIngredient>();
            anchIng.Add(NewQIngredient("Yellow Onion", 1, ing, anchoBBQ.Id));
            anchIng.Add(NewQIngredient("Gold Potatoes", 6, ing, anchoBBQ.Id));
            anchIng.Add(NewQIngredient("Dill Pickle", 2, ing, anchoBBQ.Id));
            anchIng.Add(NewQIngredient("Beef Stock Concentrate", 1, ing, anchoBBQ.Id));
            anchIng.Add(NewQIngredient("Potato Buns", 2, ing, anchoBBQ.Id));
            anchIng.Add(NewQIngredient("Ground Beef", 10, Fraction.NoFraction, MassMeasurementUnit.ounce, ing, anchoBBQ.Id));
            anchIng.Add(NewQIngredient("BBQ Sauce", 4, Fraction.NoFraction, VolumeMeasurementUnit.Tablespoon, ing, anchoBBQ.Id));
            anchIng.Add(NewQIngredient("Ketchup", 2, Fraction.NoFraction, VolumeMeasurementUnit.Tablespoon, ing, anchoBBQ.Id));
            anchIng.Add(NewQIngredient("Ancho Chili Powder", 1, Fraction.NoFraction, VolumeMeasurementUnit.Teaspoon, ing, anchoBBQ.Id));
            anchIng.Add(NewQIngredient("Cornstarch", 1, Fraction.NoFraction, VolumeMeasurementUnit.Tablespoon, ing, anchoBBQ.Id));
            var anchSupplies = new List<QSupply>();
            anchSupplies.Add(await AddSupplyAsync("Large Pan", sup, anchoBBQ.Id, context));
            anchSupplies.Add(await AddSupplyAsync("Baking Sheet", sup, anchoBBQ.Id, context));
            anchSupplies.Add(await AddSupplyAsync("Small Bowl", sup, anchoBBQ.Id, context));
            anchSupplies.Add(await AddSupplyAsync("Olive Oil", sup, anchoBBQ.Id, context));
            anchSupplies.Add(await AddSupplyAsync("Salt And Pepper", sup, anchoBBQ.Id, context));

            var anchSteps = new List<Step>();
            anchSteps.Add(AddStep("Preheat oven to 450°.", 1, anchoBBQ.Id));
            anchSteps.Add(AddStep("Cut potatoes into ¼-inch-thick rounds. Toss on a baking sheet with a large drizzle of oil, salt, and pepper.", 2, anchoBBQ.Id));
            anchSteps.Add(AddStep("Roast on top rack until lightly browned and tender, 18-20 minutes.", 3, anchoBBQ.Id));
            anchSteps.Add(AddStep(" While potatoes roast, half, peel, and dice onion. Thinly slice pickle into rounds. Halve buns.", 4, anchoBBQ.Id));
            anchSteps.Add(AddStep("In a small bowl, combing BBQ Sauce, ketchup, chili powder, stock concentrate, half the cornstarch, and 1 TBS water.", 5, anchoBBQ.Id));
            anchSteps.Add(AddStep("Heat a drizzle of oil in a large pan over medium-high heat. Add onion; cook stirring, until softened, 4-5 minutes.", 6, anchoBBQ.Id));
            anchSteps.Add(AddStep("Add beef; season with salt and pepper. Cook breaking up meat into pieces, until browned, 3-5 minutes.", 7, anchoBBQ.Id));
            anchSteps.Add(AddStep("Add BBQ sauce mixture to pan. Cook, stirring, until sauce has thickened and beef is cooked through, 2-3 minutes. Taste and season with salt and pepper.", 8, anchoBBQ.Id));
            anchSteps.Add(AddStep("While filling cooks, toast buns until golden brown.", 9, anchoBBQ.Id));
            anchSteps.Add(AddStep("Serve meat on buns, topped with pickle ", 10, anchoBBQ.Id));
            anchoBBQ.Supplies = anchSupplies;
            await context.AddRangeAsync(anchSteps);
            await context.AddRangeAsync(anchIng);
            await context.SaveChangesAsync();
            #endregion
        }

        private static async Task SeedBuffChickenAsync(ApplicationDbContext context, List<RecipeType> types, List<Ingredient> ing, ApplicationUser admin, List<Supply> sup)
        {
            #region Buffalochicken
            var buffaloChk = new Recipe()
            {
                Name = "Buffalo Spiced Crispy Chicken",
                Description = "With Mashed Potatoes, Buttery Green Beans, & Honey Drizzle",
                RecipeSource = "HelloFresh",
                AuthorId = admin.Id,
                Servings = 2,
                CookingTime = 35,
                TypeId = types.Where(t => t.Name == "Chicken").FirstOrDefault().Id,
                ImageId = 1
            };
            await context.AddAsync(buffaloChk);
            await context.SaveChangesAsync();
            var buffIng = new List<QIngredient>();
            buffIng.Add(NewQIngredient("Scallions", 2, ing, buffaloChk.Id));
            buffIng.Add(NewQIngredient("Gold Potatoes", 6, ing, buffaloChk.Id));
            buffIng.Add(NewQIngredient("Chicken Breast", 10, Fraction.NoFraction, MassMeasurementUnit.ounce, ing, buffaloChk.Id));
            buffIng.Add(NewQIngredient("Sour Cream", 4, Fraction.NoFraction, VolumeMeasurementUnit.Tablespoon, ing, buffaloChk.Id));
            buffIng.Add(NewQIngredient("Butter", 3, Fraction.NoFraction, VolumeMeasurementUnit.Tablespoon, ing, buffaloChk.Id));
            buffIng.Add(NewQIngredient("Montrey Jack (Shredded)", 0, Fraction.OneQuarter, VolumeMeasurementUnit.Cup, ing, buffaloChk.Id));
            buffIng.Add(NewQIngredient("Panko Breadcrumbs", 0, Fraction.OneQuarter, VolumeMeasurementUnit.Cup, ing, buffaloChk.Id));
            buffIng.Add(NewQIngredient("Honey", 2, Fraction.NoFraction, VolumeMeasurementUnit.Teaspoon, ing, buffaloChk.Id));
            buffIng.Add(NewQIngredient("Green Beans", 6, Fraction.NoFraction, VolumeMeasurementUnit.Ounce, ing, buffaloChk.Id));
            buffIng.Add(NewQIngredient("Franks Seasoning Blend", 1, Fraction.NoFraction, VolumeMeasurementUnit.Teaspoon, ing, buffaloChk.Id));


            buffaloChk.Supplies.Add(await AddSupplyAsync("Large Bowl", sup, buffaloChk.Id, context));
            buffaloChk.Supplies.Add(await AddSupplyAsync("Baking Sheet", sup, buffaloChk.Id, context));
            buffaloChk.Supplies.Add(await AddSupplyAsync("Paper Towels", sup, buffaloChk.Id, context));
            buffaloChk.Supplies.Add(await AddSupplyAsync("Strainer", sup, buffaloChk.Id, context));
            buffaloChk.Supplies.Add(await AddSupplyAsync("Small Bowl", sup, buffaloChk.Id, context));
            buffaloChk.Supplies.Add(await AddSupplyAsync("Medium Bowl", sup, buffaloChk.Id, context));
            buffaloChk.Supplies.Add(await AddSupplyAsync("Medium Pot", sup, buffaloChk.Id, context));
            buffaloChk.Supplies.Add(await AddSupplyAsync("Olive Oil", sup, buffaloChk.Id, context));
            buffaloChk.Supplies.Add(await AddSupplyAsync("Salt And Pepper", sup, buffaloChk.Id, context));

            var buffSteps = new List<Step>();
            buffSteps.Add(AddStep("Preheat oven to 425°.", 1, buffaloChk.Id));
            buffSteps.Add(AddStep("Trim and thinly slice scallions, separating whites from greens", 2, buffaloChk.Id));
            buffSteps.Add(AddStep("In a small bowl, combine half the sour cream, ½ tsp Frank’s Seasoning, and a big pinch of salt. Stir in water 1 tsp at a time until mixture reaches drizzling consistency", 3, buffaloChk.Id));
            buffSteps.Add(AddStep("Place 1 TBS butter in a medium bowl, microwave until melted. Stir in panko, Monterey Jack, remaining Frank’s Seasoning, and a big pinch of salt and pepper.", 4, buffaloChk.Id));
            buffSteps.Add(AddStep("Dice potatoes in ½-inch pieces. Place in a medium pot with enough salted water to cover by 2 inches. Bring to a boil; cook until tender, 15-20 minutes. Reserve ½ cup potato cooking liquid, then drain", 5, buffaloChk.Id));
            buffSteps.Add(AddStep("Add a drizzle of oil and scallion whites to empty pot over low heat; cook until softened, 1 minute.", 6, buffaloChk.Id));
            buffSteps.Add(AddStep("Return potatoes to pot; mash with remaining sour cream and 1 TBS butter until smooth and creamy, adding splashes of reserved potato cooking liquid as needed.Season with salt and pepper. Keep covered off heat.", 7, buffaloChk.Id));
            buffSteps.Add(AddStep("While potatoes cook, pat chicken dry with paper towels and season all over with salt and pepper.Place on one side of a lightly oiled baking sheet.", 8, buffaloChk.Id));
            buffSteps.Add(AddStep("Mound tops of chicken with panko mixture, pressing firmly to adhere to top. ", 9, buffaloChk.Id));
            buffSteps.Add(AddStep("Toss green beans on opposite side of sheet from chicken with a large drizzle of oil and a pinch of salt & pepper.", 10, buffaloChk.Id));
            buffSteps.Add(AddStep("Roast on top rack until chicken is golden brown and cooked through and green beans are tender, 15 - 18 minutes.", 11, buffaloChk.Id));
            buffSteps.Add(AddStep("Transfer roasted green beans to a large bowl; add 1 TBS butter and toss until melted.", 12, buffaloChk.Id));
            buffSteps.Add(AddStep("Drizzle chicken with creamy buffalo sauce and honey.", 13, buffaloChk.Id));

            await context.AddRangeAsync(buffSteps);
            await context.AddRangeAsync(buffIng);
            #endregion

            await context.SaveChangesAsync();
        }

        private static Step AddStep(string v, int stepNum, int id)
        {
            var step = new Step()
            {
                Text = v,
                RecipeId = id,
                StepNumber = stepNum
            };
            return step;
        }

        private static async Task<QSupply> AddSupplyAsync(string v, List<Supply> sup, int rId, ApplicationDbContext context)
        {
            Supply supplyRoot = sup.Where(s => s.Name == v).FirstOrDefault();
            QSupply supply = new()
            {
                Quantity= 1,
                RecipeId = rId,
                SupplyId = supplyRoot.Id
            };
            await context.SaveChangesAsync();
            return supply;
        }

        private static QIngredient NewQIngredient(string name, int count, List<Ingredient> ingredients, int rId)
        {
            var _measurementService = new MeasurementService();
            var ingId = ingredients.Where(i => i.Name == name).FirstOrDefault().Id;
            var qing = new QIngredient()
            {
                IngredientId = ingId,
                MeasurementType = MeasurementType.Count,
                QuantityNumber = count,
                NumberOfUnits = _measurementService.EncodeUnitMeasurement(count, Fraction.NoFraction)
            };
            qing.RecipeId = rId;
            return qing;
        }
        private static QIngredient NewQIngredient(string name, int count, Fraction frac, VolumeMeasurementUnit unit, List<Ingredient> ingredients, int rId)
        {
            var _measurementService = new MeasurementService();
            var ingId = ingredients.Where(i => i.Name == name).FirstOrDefault().Id;
            var qing = new QIngredient()
            {
                IngredientId = ingId,
                Fraction = frac,
                MeasurementType = MeasurementType.Volume,
                QuantityNumber = count,
                NumberOfUnits = _measurementService.EncodeVolumeMeasurement(count, frac, unit)
            };
            qing.Quantity = _measurementService.DecodeVolumeMeasurement(qing.NumberOfUnits);
            qing.RecipeId = rId;
            return qing;
        }
        private static QIngredient NewQIngredient(string name, int count, Fraction frac, MassMeasurementUnit mass, List<Ingredient> ingredients, int rId)
        {
            var _measurementService = new MeasurementService();
            var ingId = ingredients.Where(i => i.Name == name).FirstOrDefault().Id;
            var qing = new QIngredient()
            {
                IngredientId = ingId,
                Fraction = frac,
                MeasurementType = MeasurementType.Mass,
                QuantityNumber = count,
                NumberOfUnits = _measurementService.EncodeMassMeasurement(count, frac, mass)
            };
            qing.Quantity = _measurementService.DecodeMassMeasurement(qing.NumberOfUnits);
            qing.RecipeId = rId;
            return qing;
        }
        private static async Task SeedRecipeTypesAsync(ApplicationDbContext context)
        {
            if ((await context.RecipeType.ToListAsync()).Count() < 1)
            {
                var types = new List<RecipeType>();
                types.Add(new()
                {
                    Name = "American"
                });
                types.Add(new()
                {
                    Name = "Chicken"
                });

                types.Add(new()
                {
                    Name = "Mexican"
                });
                types.Add(new()
                {
                    Name = "Seafood"
                });
                types.Add(new()
                {
                    Name = "Italian"
                });
                types.Add(new()
                {
                    Name = "Breakfast"
                });
                types.Add(new()
                {
                    Name = "Asian"
                });
                context.AddRange(types);
                await context.SaveChangesAsync();
            }
        }
    }
}
