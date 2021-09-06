using IdentityModel;
using IdentityServer4.Models;
using IdentityServer4.Services;
using System.Threading.Tasks;

namespace MasterMealWA.Server.Services
{

    public class ProfileService : IProfileService
    {
        public ProfileService()
        {
        }

        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var idClaim = context.Subject.FindAll(JwtClaimTypes.Id);
            context.IssuedClaims.AddRange(idClaim);
            await Task.CompletedTask;
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            await Task.CompletedTask;
        }
    }
}