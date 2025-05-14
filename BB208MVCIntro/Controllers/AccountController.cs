using BB208MVCIntro.DAL;
using BB208MVCIntro.Models;
using BB208MVCIntro.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.JSInterop.Implementation;

namespace BB208MVCIntro.Controllers
{
    public class AccountController : Controller
    {
        private readonly AppDbContext _db;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;   
        public AccountController(AppDbContext appDbContext,
            UserManager<AppUser> userManager, SignInManager<AppUser> signInManager,
            RoleManager<IdentityRole> roleManager)
        {
            _db = appDbContext;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(CreateUserVM createUserVM)
        {
            if (!ModelState.IsValid)
            {
                return View(createUserVM);
            }
            AppUser appUser = new AppUser();
            appUser.FirstName = createUserVM.FirstName;
            appUser.LastName = createUserVM.LastName;
            appUser.Email = createUserVM.Email;
            appUser.UserName = createUserVM.Username;
            var result = await _userManager.CreateAsync(appUser, createUserVM.Password);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                    return View(createUserVM);
                }
            }
            await _userManager.AddToRoleAsync(appUser, "User");
            return RedirectToAction(nameof(Index), "Home");
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginUserVM loginUserVM, string? returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(loginUserVM);
            }
           AppUser? user = await _userManager.FindByNameAsync(loginUserVM.UsernameOrEmailAddress);
            if (user == null) 
            {
               user =  await _userManager.FindByEmailAsync(loginUserVM.UsernameOrEmailAddress);
                if (user == null)
                {
                    ModelState.AddModelError(string.Empty, "Username or password is wrong");
                    return View(loginUserVM);
                }
            }
            var result = await _signInManager.PasswordSignInAsync(user, loginUserVM.Password, loginUserVM.IsPersistent, true);
            if (!result.Succeeded)
            {
                ModelState.AddModelError(string.Empty, "Username or password is wrong");
                return View();
            }
           if(returnUrl != null) {return Redirect(returnUrl); }

            return RedirectToAction("Index", "Home");
        }


        public async Task<IActionResult> LogOut()
        {
           await  _signInManager.SignOutAsync();
           return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> AddRoles()
        {
            await _roleManager.CreateAsync(new IdentityRole() { Name = "Admin" });
            await _roleManager.CreateAsync(new IdentityRole() { Name = "Manager" });
            await _roleManager.CreateAsync(new IdentityRole() { Name = "User" });
            return Json("Successfully created user roles");
        }

        public async Task<IActionResult> CreateAdmin()
        {
            AppUser user = new AppUser()
            {
                UserName = "SuperAdmin",
                Email = "Admin@gmail.com",
                FirstName = "Revan",
                LastName = "Abdullayev"
            };
            var result = await _userManager.CreateAsync(user, "Salam123!");
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "Admin");
                return Json("Admin has been successfully created");
            }

            return Json("Something went wrong");
        }



    }
}
