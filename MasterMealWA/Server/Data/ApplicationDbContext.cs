using IdentityServer4.EntityFramework.Options;
using MasterMealWA.Server.Models;
using MasterMealWA.Shared.Models;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MasterMealWA.Server.Data
{
    public class ApplicationDbContext : ApiAuthorizationDbContext<ApplicationUser>
    {
        public ApplicationDbContext(
            DbContextOptions options,
            IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
        {

        }
        public DbSet<Comment> Comment { get; set; }
        public DbSet<Ingredient> Ingredient { get; set; }
        public DbSet<Meal> Meal { get; set; }
        public DbSet<QIngredient> QIngredient { get; set; }
        public DbSet<Rating> Rating { get; set; }
        public DbSet<Recipe> Recipe { get; set; }
        public DbSet<RecipeType> RecipeType { get; set; }
        public DbSet<Step> Step { get; set; }
        public DbSet<Supply> Supply { get; set; }
        public DbSet<IngredientType> IngredientType { get; set; }
        public DbSet<DBImage> DBImage { get; set; }
        public DbSet<MasterMealWA.Shared.Models.Chef> Chef { get; set; }
        public DbSet<MasterMealWA.Shared.Models.ShoppingIngredient> ShoppingIngredient { get; set; }
        public DbSet<MasterMealWA.Shared.Models.ShoppingList> ShoppingList { get; set; }
    }
}
