using MasterMealWA.Shared.Models;
using Microsoft.AspNetCore.Http;
using SixLabors.ImageSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace MasterMealWA.Server.Services.Interfaces
{
    public interface IFileService
    {
        public Task<byte[]> ConvertFileToByteArrayAsync(IFormFile file);
        public Task<byte[]> ConvertFileToByteArrayAsync(Image file, string contentType);

        public string ConvertByteArrayToFile(byte[] fileData, string extension);
        public string GetUserAvatar(Chef user);

        public string GetFileIcon(string file);

        public string FormatFileSize(long bytes);
        public Task<byte[]> EncodeFileAsync(string filename);
    }
}
