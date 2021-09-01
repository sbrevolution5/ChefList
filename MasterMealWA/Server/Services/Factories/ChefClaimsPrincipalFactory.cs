using MasterMealWA.Shared.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MasterMealWA.Server.Services.Factories
{
    public class ChefClaimsPrincipalFactory : UserClaimsPrincipalFactory<Chef, IdentityRole>
    {
        public ChefClaimsPrincipalFactory(UserManager<Chef> userManager, RoleManager<IdentityRole> roleManager, IOptions<IdentityOptions> options) : base(userManager, roleManager, options)
        {
        }
    }
}

