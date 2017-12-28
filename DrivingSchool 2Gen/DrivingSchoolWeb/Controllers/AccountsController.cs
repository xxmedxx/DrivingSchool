using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DrivingSchoolWeb.ViewModel;
using DrivingSchoolDB;
using Microsoft.AspNetCore.Identity;

namespace DrivingSchoolWeb.Controllers
{
    public class AccountsController : Controller
    {

        private readonly SignInManager<ApplicationUser> _SingInManager;
        private readonly UserManager<ApplicationUser> _UserManager;

        public AccountsController(SignInManager<ApplicationUser> singInManager, UserManager<ApplicationUser> userManager)
        {
            _SingInManager = singInManager;
            _UserManager = userManager;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(UserViewModel u)
        {
            if (!ModelState.IsValid)
                return View(u);
            var user = new ApplicationUser();
            user.Email = u.Email;
            user.UserName = u.UserName;
            var rc = await _UserManager.CreateAsync(user, u.Password);

            if (rc.Succeeded)
            {
                await _SingInManager.SignInAsync(user, false);
                return RedirectToAction("index", "Home");
            }
            else
            {
                foreach (var err in rc.Errors)
                {
                    ModelState.AddModelError("", err.Description);
                }
            }

            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel u)
        {
            if (!ModelState.IsValid)
                return View(u);
            var user = await _UserManager.FindByEmailAsync(u.Email);
            if (user == null)
            {
                ModelState.AddModelError("", "Wrong user name or password.");
                return View(u);
            }

            var rc = await _SingInManager.CheckPasswordSignInAsync(user, u.Password, false);
            if (rc.Succeeded)
            {
               await _SingInManager.SignInAsync(user,false);
                return RedirectToAction("index", "Home");
            }
            ModelState.AddModelError("", "Wrong user name or password.");
            return View(u);
        }

        [HttpGet]
        public async Task<IActionResult> LogOut()
        {
            await _SingInManager.SignOutAsync();
            return RedirectToAction("index", "Home");
        }
         
    }
}