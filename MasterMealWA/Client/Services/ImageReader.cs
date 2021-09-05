using MasterMealWA.Client.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MasterMealWA.Client.Services
{
    public class ImageReader : IImageReader
    {
        public string ReadImageData(byte[] data, string ContentType)
        {
            if (data == null)
            {
                return null;
            }
            if (ContentType == null)
            {
                return null;
            }
            string imageBase64Data = Convert.ToBase64String(data);
            return string.Format($"data:image/{ContentType};base64,{imageBase64Data}");


        }
    }
}
