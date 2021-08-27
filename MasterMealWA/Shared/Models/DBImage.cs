using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MasterMealWA.Shared.Models
{
    public class DBImage
    {
        public int Id { get; set; }
        public byte[] ImageData { get; set; }
        public string ContentType { get; set; }
    }
}
