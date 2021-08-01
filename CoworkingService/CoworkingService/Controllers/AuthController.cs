using CoworkingService.Constants;
using CoworkingService.Data;
using CoworkingService.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        private RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<User> _signInManager;
        private ApplicationDbContext _context;

        public AuthController(UserManager<User> userManager,
            SignInManager<User> signInManager,
            ApplicationDbContext context,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
            _roleManager = roleManager;
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
        public async Task<IActionResult> RegisterAsync(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = new User
            {
                UserName = model.Email,
                Name = model.Name,
                Surname = model.Surname,
                Phone = model.Phone,
                Email = model.Email,
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                switch (model.Role)
                {
                    case RoleConstants.AdminUser:
                        {
                            await _userManager.AddToRoleAsync(user, RoleConstants.AdminUser);
                            break;
                        }
                    case RoleConstants.RegularUser:
                        {
                            await _userManager.AddToRoleAsync(user, RoleConstants.RegularUser);
                            break;
                        }
                }
                await _signInManager.SignInAsync(user, false);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("Password", "Password is too weak");
                return View(model);
            }
        }

        [AcceptVerbs("Get", "Post")]
        [AllowAnonymous]
        public async Task<IActionResult> IsEmailInUse(string email) => Json(data: await _userManager.FindByEmailAsync(email) == null);


    }
}
