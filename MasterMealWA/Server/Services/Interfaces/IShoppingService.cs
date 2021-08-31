using MasterMealWA.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MasterMealWA.Server.Services.Interfaces
{
    interface IShoppingService
    {
        public Task<ShoppingList> CreateShoppingListForDateRangeAsync(DateTime EndDate, DateTime StartDate);

    }
}
