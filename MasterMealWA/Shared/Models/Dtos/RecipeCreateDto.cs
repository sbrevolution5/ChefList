using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MasterMealWA.Shared.Models.Dtos
{
    public class RecipeCreateDto
    {
        public MultipartFormDataContent ImageData { get; set; }
        public byte[] Image { get; set; }
        public Recipe Recipe { get; set; }
        public string ImageContentType { get; set; }
    }
}
