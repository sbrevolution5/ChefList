using MasterMealWA.Shared.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MasterMealWA.Server.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string DisplayName { get; set; }
        [Required]
        public string ScreenName { get; set; }
        public bool ShowFullName { get; set; }
        public int ImageId { get; set; }
        public virtual DBImage Image { get; set; }
        public List<Recipe> FavoriteRecipes { get; set; }
    }
}
