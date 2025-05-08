using BB208MVCIntro.DAL;
using BB208MVCIntro.Models;
using BB208MVCIntro.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BB208MVCIntro.Controllers
{
    public class AccountController : Controller
    {
        private readonly AppDbContext _db;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        public AccountController(AppDbContext appDbContext,
            UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _db = appDbContext;
            _userManager = userManager;
            _signInManager = signInManager;
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
            return RedirectToAction(nameof(Index), "Home");
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginUserVM loginUserVM)
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

            return RedirectToAction("Index", "Home");
        }


        public async Task<IActionResult> LogOut()
        {
           await  _signInManager.SignOutAsync();
           return RedirectToAction("Index", "Home");
        }

    }
}
