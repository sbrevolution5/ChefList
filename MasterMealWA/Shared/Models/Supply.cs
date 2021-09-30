using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MasterMealWA.Shared.Models
{
    public class Supply
    {
        
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public ICollection<Chef> Chefs { get; set; } = new List<Chef>();
    }
}
