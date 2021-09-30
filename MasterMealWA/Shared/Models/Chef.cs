using MasterMealWA.Shared.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MasterMealWA.Shared.Models
{
    public class Chef :IdentityUser
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string ScreenName { get; set; }
        public bool ShowFullName { get; set; }
        public int ImageId { get; set; }
        public virtual DBImage Image { get; set; }
        public List<Recipe> FavoriteRecipes { get; set; }
        public ICollection<Supply> ChefSupplies { get; set; } = new List<Supply>();
        [NotMapped]
        public string FullName
        {
            get
            {
                return $"{FirstName} {LastName}";
            }
        }
    }
}
