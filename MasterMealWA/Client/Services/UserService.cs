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
        public async Task<bool> IsModeratorAsync()
        {
            var authState = await _authState.GetAuthenticationStateAsync();
            return authState.User.IsInRole("Admin") || authState.User.IsInRole("Moderator");
        }
    }
}
