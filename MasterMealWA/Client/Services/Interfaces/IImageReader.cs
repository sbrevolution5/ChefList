using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace MasterMealWA.Client.Services.Interfaces
{
    public interface IImageReader
    {
        string ReadImageData(byte[] data, string ContentType);
        string CreatePreviewImage(MultipartFormDataContent content);
    }
}
