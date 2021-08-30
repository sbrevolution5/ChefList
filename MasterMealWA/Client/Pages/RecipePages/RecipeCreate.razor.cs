using MasterMealWA.Shared.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace MasterMealWA.Client.Pages.RecipePages
{
    public partial class RecipeCreateCB : ComponentBase
    {
        public Recipe recipe { get; set; } = new();
        public List<RecipeType> Types { get; set; }
        public List<Ingredient> ingredients { get; set; }
        protected override async Task OnInitializedAsync()
        {
            Types = await _api.GetAllRecipeTypesAsync();
            ingredients = await _api.GetAllIngredientsAsync();

        }
        public async Task CreateRecipeAsync()
        {
            await _api.CreateNewRecipeAsync(recipe);
        }
        IList<IBrowserFile> files = new List<IBrowserFile>();
        private void UploadFiles(InputFileChangeEventArgs e)
        {
            foreach (var file in e.GetMultipleFiles())
            {
                files.Add(file);
            }
            //TODO upload the files to the server
        }
        private async Task OnValidSubmit(EditContext context)
        {
            await Http.PostAsJsonAsync<Recipe>("Api/recipes", recipe);
        }
    }
}
