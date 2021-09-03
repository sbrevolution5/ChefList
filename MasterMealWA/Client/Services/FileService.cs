using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using MasterMealWA.Shared.Models;
using MasterMealWA.Client.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using Microsoft.AspNetCore.Components.Forms;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Formats.Png;

namespace MasterMealWA.Client.Services
{

    public class FileService : IFileService
    {
        private readonly string[] suffixes = { "Bytes", "KB", "MB", "GB", "TB", "PB" };

        public async Task<byte[]> ConvertFileToByteArrayAsync(IFormFile file)
        {
            MemoryStream memoryStream = new MemoryStream();
            await file.CopyToAsync(memoryStream);
            var byteFile = memoryStream.ToArray();
            memoryStream.Close();
            memoryStream.Dispose();


            return byteFile;


        }
        public async Task<byte[]> ConvertFileToByteArrayAsync(IBrowserFile file,string contentType)
        {
            using var mstream = new MemoryStream();
            var stream = file.OpenReadStream();
            await stream.CopyToAsync(mstream);
            Image image=null;
            if (contentType == "image/png")
            {
                image = Image.Load(mstream, new PngDecoder());

            }
            else if (contentType == "image/jpeg")
            {
            image = Image.Load(mstream,new JpegDecoder());
            }
            image.Mutate(x => x.Resize(new ResizeOptions
            {
                Mode = ResizeMode.Min,
                Size = new Size(128)
            }));

            MemoryStream memoryStream = new MemoryStream();
            if (contentType == "image/png")
            {

                await image.SaveAsPngAsync(memoryStream);
            }
            else if (contentType == "image/jpeg")
            {
                await image.SaveAsJpegAsync(memoryStream);
            }
            var byteFile = memoryStream.ToArray();
            memoryStream.Close();
            memoryStream.Dispose();


            return byteFile;
        }
        public async Task<byte[]> ConvertFileToByteArrayAsync(Image file, string contentType)
        {
            //using var image = Image.Load(Input.ImageFile.OpenReadStream());
            file.Mutate(x => x.Resize(new ResizeOptions
            {
                Mode = ResizeMode.Min,
                Size = new Size(128)
            }));
            
            MemoryStream memoryStream = new MemoryStream();
            if (contentType == "image/png")
            {

                await file.SaveAsPngAsync(memoryStream);
            }
            else if (contentType == "image/jpeg")
            {
                await file.SaveAsJpegAsync(memoryStream);
            }
            var byteFile = memoryStream.ToArray();
            memoryStream.Close();
            memoryStream.Dispose();


            return byteFile;


        }


        public string ConvertByteArrayToFile(byte[] fileData, string extension)
        {
            if (fileData == null)
            {
                return null;
            }
            if (extension == null)
            {
                return null;
            }
            string imageBase64Data = Convert.ToBase64String(fileData);
            return string.Format($"data:image/{extension};base64,{imageBase64Data}");


        }


        public string GetFileIcon(string file)
        {
            string ext = Path.GetExtension(file).Replace(".", "");
            return $"/img/png/{ext}.png";
        }


        public string FormatFileSize(long bytes)
        {
            int counter = 0;
            decimal number = bytes;
            while (Math.Round(number / 1024) >= 1)
            {
                number /= 1024;
                counter++;
            }
            return string.Format("{0:n1}{1}", number, suffixes[counter]);
        }
        public async Task<byte[]> EncodeFileAsync(string filename)
        {
            var file = $"{Directory.GetCurrentDirectory()}/wwwroot/img/{filename}";
            return await File.ReadAllBytesAsync(file);
        }

        public string GetUserAvatar(Chef user)
        {
            throw new NotImplementedException();
            //return ConvertByteArrayToFile(user.AvatarFileData, user.AvatarFileContentType);
        }
    }
}
