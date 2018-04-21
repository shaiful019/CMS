using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CMS.Web.Data;

namespace CMS.Web.Controllers
{
    [Route("[controller]/[action]")]
    public class AccountController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger _logger;
        private UserManager<ApplicationUser> _userManager;
        private ApplicationDbContext _applicationDbContext;

        public AccountController(SignInManager<ApplicationUser> signInManager, ILogger<AccountController> logger,UserManager<ApplicationUser> userManager,ApplicationDbContext applicationDbContext)
        {
            _signInManager = signInManager;
            _logger = logger;
            _userManager = userManager;
            _applicationDbContext = applicationDbContext;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation("User logged out.");
            return RedirectToAction("Index","Post");
        }

        public ActionResult Profile(string id)
        {
            var currentuser = _applicationDbContext.Users.SingleOrDefault(m =>m.UserName.Equals(id));
            var user = new ApplicationUser()
            {
                Email = currentuser.Email,
                UserName = currentuser.UserName,
                IsActive = currentuser.IsActive,
                PhoneNumber = currentuser.PhoneNumber,
                ProfileImage = currentuser.ProfileImage,
                FbID = currentuser.FbID,
                Gender = currentuser.Gender
            };
            return View(user);
        }
    }
}
