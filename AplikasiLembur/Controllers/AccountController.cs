using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AplikasiLembur.Models;
using AplikasiLembur.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace AplikasiLembur.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        [Route("/")]
        [Route("/login")]
        public IActionResult Login()
        {
            if (_signInManager.IsSignedIn(User))
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        [Produces("application/json")]
        [Route("/login")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid)
            {
                Thread.Sleep(3000);
                var user = await _userManager.FindByNameAsync(loginViewModel.Username);

                if (user != null)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, loginViewModel.Password, false, false);

                    if (result.Succeeded)
                    {
                        if (user.UserName.ToUpper() == "ADMIN")
                        {
                            return Json(new{type="url",data= "/admin"});
                            //return RedirectToAction("Index", "Admin");
                        }
                        else
                        {
                            return Json(new {type = "url", data = "/home" });
                            //return RedirectToAction("Index", "Home");
                        }
                    }

                }
            }
            ModelState.AddModelError("Password", "Wrong password. Try again.");
            var modelState = ModelState.Select(p => new { key = p.Key, errors = p.Value.Errors.Select(e => e.ErrorMessage) })
                .ToDictionary(kv => kv.key, kv => kv.errors);
            string errors = JsonConvert.SerializeObject(modelState);
            return Json(new {type = "error", data = errors});
        }


        [HttpPost]
        [Authorize(Roles = "admin")]
        [Route("/account/adduser")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(Register register)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(register.Username.ToLower());

                if (user == null)
                {
                    var users = new IdentityUser(register.Username.ToLower());
                    var result = await _userManager.CreateAsync(users, "12345678");

                    if(await _roleManager.FindByNameAsync("user") == null)
                    {
                        await _roleManager.CreateAsync(new IdentityRole("user"));
                    }

                    var roleAdd = await _userManager.AddToRoleAsync(users, "user");


                    if (result.Succeeded)
                    {
                        return Json(new { type = "msg", messageType = "information", message = "User successfully created!!" });
                    }

                }
            }
            ModelState.AddModelError("register.Username", "Invalid Username!");
            var modelState = ModelState.Select(p => new { key = p.Key, errors = p.Value.Errors.Select(e => e.ErrorMessage) })
                .ToDictionary(kv => kv.key, kv => kv.errors);
            string errors = JsonConvert.SerializeObject(modelState);
            return Json(new { type = "error", data = errors });
        }
        
        [Authorize(Roles = "admin")]
        [Route("/account/deluser/{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {  
            var user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                var result = await _userManager.DeleteAsync(user);
                if (result.Succeeded)
                {
                    
                    return Json(new { type = "msg", messageType = "information", message = "Delete user successfully!" });
                }
            }
            return Json(new { type = "msg", messageType = "error", message = "Delete user failed!" });
        }
        
        //[HttpPost]
        //[Authorize(Roles = "admin")]
        //[Route("/account/user")]
        //public async Task<IActionResult> UserList()
        //{
        //    List<UserList> users = new List<UserList>();
        //    foreach (var user in _userManager.Users)
        //    {
        //        if (!await _userManager.IsInRoleAsync(user, "admin"))
        //        {
        //            users.Add(new UserList()
        //            {
        //                ResetPassword = "<a id='resetPassButton' class='btn btn-outline-primary border-0' href='#' data='" + user.Id + "' data-tooltip='show' data-placement='auto' title='' data-original-title='Reset Password'><i class='fas fa-unlock-alt fa-lg'></i></a>",
        //                Username = user.NormalizedUserName,
        //                Delete = "<a id='deleteUserButton' class='btn btn-outline-danger border-0' href='#' data='" + user.Id + "' data-tooltip='show' data-placement='auto' title='' data-original-title='Delete'><i class='fas fa-trash fa-lg'></i></a>"
        //            });
        //        } 
        //    }
        //    return Json(users);
        //}

        [Authorize(Roles = "admin")]
        [Route("/account/resetpass/{id}")]
        public async Task<IActionResult> ResetPassword(string id)
        {
            var user =await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                var result=await _userManager.ResetPasswordAsync(user, token, "12345678");
                if (result.Succeeded)
                {
                    return Json(new { type = "msg", messageType = "information", message = "Password reset successfully!" });
                }
            }
            return Json(new { type = "msg", messageType = "error", message = "Password reset failed!" });
        }

        [HttpPost]
        [Authorize]
        [Route("/account/cp")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ChangePasswordModel changePasswordModel)
        {
            if (ModelState.IsValid)
            {

                var user = await _userManager.GetUserAsync(User);
                var result = await _userManager.ChangePasswordAsync(user, changePasswordModel.OldPassword,
                    changePasswordModel.NewPassword);
                if (result.Succeeded)
                {
                    //return URL
                    return Json(new { type = "msg", messageType = "information", message = "Password Berhasil Dirubah!" });
                }

            }
            ModelState.AddModelError("changePasswordModel.OldPassword", "Wrong Password. Try again!");

            var modelState = ModelState.Select(p => new { key = p.Key, errors = p.Value.Errors.Select(e => e.ErrorMessage) })
                .ToDictionary(kv => kv.key, kv => kv.errors);
            string errors = JsonConvert.SerializeObject(modelState);
            return Json(new { type = "error", data = errors });
        }

        [HttpPost]
        [Route("/logout")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }


        [Route("/account/admin")]
        public async Task<IActionResult> admin()
        {
            var users = new IdentityUser("administrator");
            //var users = await _userManager.FindByNameAsync("administrator");
            var result = await _userManager.CreateAsync(users, "12345678");

            var role = new IdentityRole("admin");

            var roleresult = await _roleManager.CreateAsync(role);

            if (roleresult.Succeeded)
            {
                await _userManager.AddToRoleAsync(users, "admin");
                return Content("ok");
            }
            //await _userManager.AddToRoleAsync(users, "admin");
            return Content("gagal");
        }

    }
}