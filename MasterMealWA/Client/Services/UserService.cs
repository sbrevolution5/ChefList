using MasterMealWA.Client.Services.Interfaces;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MasterMealWA.Client.Services
{
    public class UserService : IUserService
    {
        private readonly AuthenticationStateProvider _authState;

        public UserService(AuthenticationStateProvider authState)
        {
            _authState = authState;
        }

        public async Task<string> GetUserIdAsync()
        {
            var id = (await _authState.GetAuthenticationStateAsync()).User.FindFirst(c => c.Type == "sub")?.Value;
            return id;
        }
    }
}
