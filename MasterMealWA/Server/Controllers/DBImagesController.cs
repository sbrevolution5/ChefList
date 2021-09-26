using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MasterMealWA.Server.Data;
using MasterMealWA.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using MasterMealWA.Client.Services.Interfaces;
using MasterMealWA.Server.Services.Interfaces;
using Microsoft.AspNetCore.StaticFiles;

namespace MasterMealWA.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DBImagesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IFileService _fileService;

        public DBImagesController(ApplicationDbContext context, IFileService fileService)
        {
            _context = context;
            _fileService = fileService;
        }
        [HttpPost]
        public async Task<IActionResult> Upload()
        {
            try
            {
                var file = Request.Form.Files[0];
                var imagedata = await _fileService.ConvertFileToByteArrayAsync(file);
                string contentType = "";
                if (file.Length > 0)
                {
                    if (file.ContentType is null)
                    {
                        var fileProvider = new FileExtensionContentTypeProvider();
                        if (!fileProvider.TryGetContentType(file.FileName, out contentType))
                        {
                            return BadRequest();
                        }
                    }
                    var image = new DBImage()
                    {
                        ContentType = file.ContentType ?? contentType,
                        ImageData = imagedata
                    };
                    _context.Add(image);
                    await _context.SaveChangesAsync();
                    return Ok(image.Id);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }
        //This method WILL NOT SAVE TO DATABASE, generates preview image.
        [HttpPost("/tempImage")]
        public async Task<IActionResult> UploadTemp()
        {
            try
            {
                var file = Request.Form.Files[0];
                var imagedata = await _fileService.ConvertFileToByteArrayAsync(file);
                string contentType = "";
                if (file.Length > 0)
                {
                    if (file.ContentType is null)
                    {
                        var fileProvider = new FileExtensionContentTypeProvider();
                        if (!fileProvider.TryGetContentType(file.FileName, out contentType))
                        {
                            return BadRequest();
                        }
                    }
                    var image = new DBImage()
                    {
                        ContentType = file.ContentType ?? contentType,
                        ImageData = imagedata
                    };
                    return Ok(image);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }
        // GET: api/DBImages
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DBImage>>> GetDBImage()
        {
            return await _context.DBImage.ToListAsync();
        }

        // GET: api/DBImages/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DBImage>> GetDBImage(int id)
        {
            var dBImage = await _context.DBImage.FindAsync(id);

            if (dBImage == null)
            {
                return NotFound();
            }

            return dBImage;
        }

        // PUT: api/DBImages/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDBImage(int id)
        {
            try
            {
                var dBImage = await _context.DBImage.FindAsync(id);
                var file = Request.Form.Files[0];
                var imagedata = await _fileService.ConvertFileToByteArrayAsync(file);
                string contentType = "";
                if (file.Length > 0)
                {
                    if (file.ContentType is null)
                    {
                        var fileProvider = new FileExtensionContentTypeProvider();
                        if (!fileProvider.TryGetContentType(file.FileName, out contentType))
                        {
                            return BadRequest();
                        }
                    }
                    var image = await _context.DBImage.FindAsync(id);

                    image.ContentType = file.ContentType ?? contentType;
                    image.ImageData = imagedata;
                    
                    _context.Entry(dBImage).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                    return Ok(image.Id);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DBImageExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }

           
        }

        

        // DELETE: api/DBImages/recipe/5
        [HttpDelete("/recipe/{id}/{recipeId}")]
        public async Task<IActionResult> DeleteRecipeDBImage(int id,int recipeId)
        {
            var dBImage = await _context.DBImage.FindAsync(id);
            if (dBImage == null)
            {
                return NotFound();
            }
            //Reset to default image
            var recipe = await _context.Recipe.FindAsync(recipeId);
            recipe.ImageId = 1;
            _context.DBImage.Remove(dBImage);

            await _context.SaveChangesAsync();

            return NoContent();
        }
        // DELETE: api/DBImages/recipe/5
        [HttpDelete("/recipe/{id}")]
        public async Task<IActionResult> DeleteUserDBImage(int id)
        {
            var dBImage = await _context.DBImage.FindAsync(id);
            if (dBImage == null)
            {
                return NotFound();
            }

            _context.DBImage.Remove(dBImage);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DBImageExists(int id)
        {
            return _context.DBImage.Any(e => e.Id == id);
        }
    }
}
