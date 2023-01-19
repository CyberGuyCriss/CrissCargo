using CrissCargoApp.Database;
using CrissCargoApp.Helper;
using CrissCargoApp.IHelper;
using CrissCargoApp.Models;
using CrissCargoApp.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.VisualBasic;
using Microsoft.EntityFrameworkCore;

namespace CrissCargoApp.Controllers
{
    public class Accountcontroller : Controller
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserHelper _userHelper;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ApplicationDbContext _context;
        private bool isPersistent;
        private bool lockoutOnFailure;
      
        public Accountcontroller(ApplicationDbContext context, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IUserHelper userHelper)
        {
            _userManager = userManager;
            _userHelper = userHelper;
            _signInManager = signInManager;
            _context = context;
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

        //Customer registration
        [HttpPost]
        public async Task<IActionResult> Register(ApplicationUserViewModel applicationUserViewModel)
        {

            if (ModelState.IsValid)
            {
                if (applicationUserViewModel.Name == null)
                {
                    return View("This field is required!");
                }
                if (applicationUserViewModel.Address == null)
                {
                    return View("This field is required!");
                }
                if (applicationUserViewModel.PhoneNumber == null)
                {
                    return View("This field is required!");
                }
                if (applicationUserViewModel.Password == null || applicationUserViewModel.ConfirmPassword == null)
                {
                    return View("This field is required!");
                }
                if (applicationUserViewModel.Password != applicationUserViewModel.ConfirmPassword)
                {
                    return View("Password Must Match!");
                }

                var emailAlreaddyExist = await _userHelper.FindUserByEmailAsync(applicationUserViewModel.Email);
                if (emailAlreaddyExist != null)
                {
                    TempData["success"] = "Email already belong to a user";
                    return View(applicationUserViewModel);
                }
                var creatUser = await _userHelper.UserRegistertion(applicationUserViewModel).ConfigureAwait(false);
                if (creatUser != null)
                {
                    await _userManager.AddToRoleAsync(creatUser, "User");
                    TempData["success"] = "Registration is Successful";
                    return RedirectToAction("Login");
                }
            }

            return View();
        } /*End of Customer registration*/



        [HttpGet]
        public IActionResult AdminRegister()
        {
            return View();
        }

        //new AdminController Reistration
        [HttpPost] 
        public async Task<IActionResult> AdminRegister(ApplicationUserViewModel applicationUserViewModel)
        {
            if (ModelState.IsValid)
            {
                if (applicationUserViewModel.Name == null)
                {
                    return View(applicationUserViewModel);
                }
                if (applicationUserViewModel.PhoneNumber == null)
                {
                    return View(applicationUserViewModel);
                }
                if (applicationUserViewModel.Email == null)
                {
                    return View(applicationUserViewModel);
                }
                if (applicationUserViewModel.Address == null)
                {
                    return View(applicationUserViewModel);
                }
                if (applicationUserViewModel.Password == null || applicationUserViewModel.ConfirmPassword == null)
                {
                    return View(applicationUserViewModel);
                }
                if (applicationUserViewModel.Password != applicationUserViewModel.ConfirmPassword)
                {
                    return View(applicationUserViewModel);
                }
                var emailAlreaddyExist = await _userHelper.FindUserByEmailAsync(applicationUserViewModel.Email);
                if (emailAlreaddyExist != null)
                {
                    TempData["success"] = "Admin Account Already Exist";
                    return View(applicationUserViewModel);
                }
                var creatUser = await _userHelper.UserRegistertion(applicationUserViewModel).ConfigureAwait(false);
                if (creatUser != null)
                {
                    await _userManager.AddToRoleAsync(creatUser, "Admin");
                    TempData["success"] = "New Admin has been added successfully!";
                    return RedirectToAction("Login");
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
        public async Task<IActionResult> Login(ApplicationUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user != null)
                {
                    var logger = _signInManager.PasswordSignInAsync(user.UserName, model.Password, isPersistent = true, lockoutOnFailure = false).Result;
                    if (logger.Succeeded)
                    {
                        var role = _userManager.IsInRoleAsync(user, "Admin").Result;
                        if(role){
                            return RedirectToAction("Index", "Admin");
                        }
                        else
                        {
                            return RedirectToAction("Index", "Customers");
                        }
                    }
                }
            }
            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            _signInManager.SignOutAsync();
            return Redirect("Login");
        }
    }
}





















        //[HttpPost]
        //public async Task<IActionResult> UserAccountRegistion(ApplicationUserViewModel model)
        //{
        //    try
        //    {
        //        if (model !=null)
        //        {
        //            var emailAlreaddyExist = await _userHelper.FindUserByEmailAsync(model.Email);
        //            if (emailAlreaddyExist !=null)
        //            {
        //                TempData["success"] = "Email alreadddy belong to a user";
        //                return View(model);
        //            }
        //            if (model.Password != model.ConfirmPassword)
        //            {
        //                return View("Password does not match");
        //            }
        //            var creatUser = await _userHelper.UserRegistertion(model);
        //            if (creatUser !=null)
        //            {
        //                TempData["success"] = "Registration is Successful";       
        //                return RedirectToAction("Login");
        //            }
        //        }
        //        return View();
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}
        //[HttpGet]
     
    //[HttpPost]
    //public IActionResult Login(userAccount user ) 
    //{
    
    //}


