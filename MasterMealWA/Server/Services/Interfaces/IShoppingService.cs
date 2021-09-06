using MasterMealWA.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MasterMealWA.Server.Services.Interfaces
{
    public interface IShoppingService
    {
        public Task<ShoppingList> CreateShoppingListForDateRangeAsync(DateTime EndDate, DateTime StartDate,string userId);

    }
}
