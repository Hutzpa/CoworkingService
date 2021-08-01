using CoworkingService.Data;
using CoworkingService.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoworkingService.Controllers
{
    public class AuthController : Controller
    {
        private UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private ApplicationDbContext _context;

        public AuthController(UserManager<User> userManager, SignInManager<User> signInManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }

        [HttpGet]
        public IActionResult Login() => View(new LoginViewModel());

        [HttpPost]
        public async Task<IActionResult> LoginAsync(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var res = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, false);

            if (!res.Succeeded)
            {
                ModelState.AddModelError("UserName", "Incorrect username or password");
                return View(model);
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> LogoutAsync()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Register() => View(new RegisterViewModel());

        [HttpPost]
        public async Task<IActionResult> RegisterAsync(RegisterViewModel rvm)
        {
            if (!ModelState.IsValid)
                return View(rvm);

            var User = new User
            {
                UserName = rvm.Email,
                Name = rvm.Name,
                Email = rvm.Email,
            };

            var result = await _userManager.CreateAsync(User, rvm.Password);

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(User, false);
                return RedirectToAction("Index", "Home");
            }

            return BadRequest();
        }

        [AcceptVerbs("Get", "Post")]
        [AllowAnonymous]
        public async Task<IActionResult> IsEmailInUse(string email) => Json(data: await _userManager.FindByEmailAsync(email) == null);


    }
}
