using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using MasterMealWA.Server.Data;
using MasterMealWA.Server.Services.Interfaces;
using MasterMealWA.Shared.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace MasterMealWA.Server.Areas.Identity.Pages.Account.Manage
{
    public partial class IndexModel : PageModel
    {
        private readonly UserManager<Chef> _userManager;
        private readonly SignInManager<Chef> _signInManager;
        private readonly ApplicationDbContext _context;
        private readonly IFileService _fileService;

        public IndexModel(
            UserManager<Chef> userManager,
            SignInManager<Chef> signInManager, ApplicationDbContext context, IFileService fileService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
            _fileService = fileService;
        }

        public string Username { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Phone]
            [Display(Name = "Phone number")]
            public string PhoneNumber { get; set; }
            [Display(Name = "First Name")]
            public string FirstName { get; set; }
            [Display(Name = "Last Name")]
            public string LastName { get; set; }
            public IFormFile ImageFile { get; set; }
            public byte[] ImageData { get; set; }
            public string ContentType { get; set; }
        }

        private async Task LoadAsync(Chef user)
        {
            var userName = await _userManager.GetUserNameAsync(user);
            var userImageData = await _context.DBImage.Where(i => i.Id == user.ImageId).FirstOrDefaultAsync();
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);

            Username = userName;

            Input = new InputModel
            {
                PhoneNumber = phoneNumber,
                LastName = user.LastName,
                FirstName = user.FirstName,
                ImageData = userImageData.ImageData,
                ContentType = userImageData.ContentType
            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            if (Input.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
                if (!setPhoneResult.Succeeded)
                {
                    StatusMessage = "Unexpected error when trying to set phone number.";
                    return RedirectToPage();
                }
            }
            if (Input.FirstName is not null)
            {
                user.FirstName = Input.FirstName;
                await _userManager.UpdateAsync(user);
            }
            if (Input.LastName is not null)
            {
                user.LastName = Input.LastName;
                await _userManager.UpdateAsync(user);
            }
            if (Input.ImageFile is not null)
            {
                var newImage = new DBImage()
                {
                    ContentType = Input.ImageFile.ContentType,
                    ImageData = await _fileService.ConvertFileToByteArrayAsync(Input.ImageFile)
                };
                _context.Add(newImage);
                user.ImageId = newImage.Id;
            }
            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }
    }
}
