﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MasterMealWA.Shared.Models
{
    public class RecipeTag
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public ICollection<Recipe> Recipes { get; set; } = new List<Recipe>();
    }
}
