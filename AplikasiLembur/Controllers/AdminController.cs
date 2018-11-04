using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using AplikasiLembur.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace AplikasiLembur.Controllers
{
    public class AdminController : Controller
    {

        private readonly UserManager<IdentityUser> _userManager;

        public AdminController(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        [Route("/admin")]
        [Authorize(Roles = "admin")]
        public IActionResult Index(AdminViewModel adminViewModel)
        {
                return View(adminViewModel);
        }
    }
}