using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterMealWA.Shared.Models.Dtos
{
    public class RecipeCreateDto
    {
        public byte[] Image { get; set; }
        public Recipe Recipe { get; set; }
    }
}
