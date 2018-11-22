using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AplikasiLembur.Models;
using AplikasiLembur.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using System.Text;

namespace AplikasiLembur.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IKaryawanRepository _karyawanRepository;
        private readonly ITaskRepository _taskRepository;
        private readonly ILemburRepository _lemburRepository;

        public HomeController(UserManager<IdentityUser> userManager, IKaryawanRepository karyawanRepository, ITaskRepository taskRepository, ILemburRepository lemburRepository)
        {
            _userManager = userManager;
            _karyawanRepository = karyawanRepository;
            _taskRepository = taskRepository;
            _lemburRepository = lemburRepository;
        }

        [Route("/home")]
        [Authorize]
        public IActionResult Index(HomeViewModel homeViewModel)
        {
            
            if (User.IsInRole("admin"))
            {
                return RedirectToAction("Index", "Admin");
            }
            else
            {    
                return View(homeViewModel);
            }

        }


        [HttpPost]
        [Route("/home/add/employee")]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddKaryawanAsync(KaryawanModel karyawanModel)
        {
            if (ModelState.IsValid)
            {
                var user = await _karyawanRepository.GetKarywanByNIK(karyawanModel.NIK);
                if (user == null)
                {
                    var result = await _karyawanRepository.AddKaryawan(karyawanModel);
                    if (result.Succeeded)
                    {
                        return Json(new { type = "msg", messageType = "information", message = "Employe successfully added!!" });
                    }
                }
            }
            ModelState.AddModelError("karyawanModel.NIK", "Invalid NIK!");
            var modelState = ModelState.Select(p => new { key = p.Key, errors = p.Value.Errors.Select(e => e.ErrorMessage) })
                .ToDictionary(kv => kv.key, kv => kv.errors);
            string errors = JsonConvert.SerializeObject(modelState);
            return Json(new { type = "error", data = errors });
        }

        [HttpPost]
        [Route("/home/add/task")]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddTaskAsync(TaskModel taskModel)
        {
            if (ModelState.IsValid)
            {
                var countTask = await _taskRepository.SearchTaskAsync(taskModel.Task);
                if (countTask > 0)
                {
                    return Json(new { type = "msg", messageType = "error", message = "Task already exists!!" });
                }
                else
                {
                    var result = await _taskRepository.AddTaskAsync(taskModel);
                    if (result.Succeeded)
                    {
                        return Json(new { type = "msg", messageType = "information", message = "Task successfully added!!" });
                    }
                }
            }
            return Json(new { type = "msg", messageType = "error", message = "Something wrong !!" });
        }


        [HttpPost]
        [Route("/home/edit/task")]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditTaskAsync(TaskModel editTaskModel)
        {
            if (ModelState.IsValid)
            {
                var result = await _taskRepository.UpdateTaskAsync(editTaskModel);
                if (result.Succeeded)
                {
                    return Json(new { type = "msg", messageType = "information", message = "Edit task successfull!!" });
                }
            }
            return Json(new { type = "msg", messageType = "error", message = "Something wrong !!" });
        }


        [HttpPost]
        [Route("/home/edit/employee")]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditKaryawanAsync(KaryawanModel editKaryawanModel)
        {
            if (ModelState.IsValid)
            {
                var result = await _karyawanRepository.UpdateKaryawan(editKaryawanModel);
                if (result.Succeeded)
                {
                    return Json(new { type = "msg", messageType = "information", message = "Employee successfully changed!!" });
                }
            }
            ModelState.AddModelError("editKaryawanModel.NIK", "Invalid NIK!");
            var modelState = ModelState.Select(p => new { key = p.Key, errors = p.Value.Errors.Select(e => e.ErrorMessage) })
                .ToDictionary(kv => kv.key, kv => kv.errors);
            string errors = JsonConvert.SerializeObject(modelState);
            return Json(new { type = "error", data = errors });
        }

        [Route("/home/delete/employee/{id:int}")]
        [Authorize]
        public async Task<IActionResult> DelKaryawanAsync(int id)
        {

            var result = await _karyawanRepository.DelKaryawanAsync(id);
            if (result.Succeeded)
            {
                return Json(new { type = "msg", messageType = "information", message = "Delete employee successfully!" });
            }
            else
            {
                return Json(new { type = "msg", messageType = "error", message = "Delete employee failed!" });
            }
        }

        [HttpPost]
        [Route("/home/delete/task")]
        [Authorize]
        public async Task<IActionResult> DelTaskAsync([FromBody] int[] id)
        {
            var result = await _taskRepository.DelTaskAsync(id);
            if (result.Succeeded)
            {
                return Json(new { type = "msg", messageType = "information", message = "Delete task successfully!" });
            }
            else
            {
                return Json(new { type = "msg", messageType = "error", message = "Delete task failed!" });
            }
        }


        [HttpPost]
        [Route("/home/list/employee")]
        [Authorize]
        public IActionResult GetKaryawanList()
        {
            return Json(_karyawanRepository.GetAllKaryawanByUserId(_userManager.GetUserId(User).ToString()).ToList());
        }

        [HttpPost]
        [Route("/home/list/task")]
        [Authorize]
        public IActionResult GetTaskList()
        {
            return Json(_taskRepository.GetAllTaskByUserId(_userManager.GetUserId(User).ToString()).ToList());
        }

        [HttpPost]
        [Route("/home/save/lembur")]
        [Authorize]
        public async Task<IActionResult> SaveLemburAsync(string test)
        {
            if (test != null)
            {
                LemburModel lemburModel = new LemburModel(); 
                byte[] bytes = Convert.FromBase64String(test);
                lemburModel = JsonConvert.DeserializeObject<LemburModel>(Encoding.UTF8.GetString(bytes));
                
                if(lemburModel.Id == "")
                {
                    lemburModel.Id = Guid.NewGuid().ToString();
                }
                else if(await _lemburRepository.CheckIdAsync(lemburModel))
                {
                    var updateResult = await _lemburRepository.UpdateLemburAsync(lemburModel);
                    if (updateResult.Succeeded)
                    {
                        return Json(new { type = "msg", messageType = "success", title = "SUCCESS", message = "Overtime has been saved!", lemburId = lemburModel.Id });
                    }
                }

                foreach (var item in lemburModel.LemburDetails)
                {
                    item.LemburId = lemburModel.Id;
                }
                var result = await _lemburRepository.AddLemburAsync(lemburModel);
                if (result.Succeeded)
                {
                    return Json(new { type = "msg", messageType = "success", title = "SUCCESS",message = "Overtime has been saved!", lemburId = lemburModel.Id });
                } 
            } 
            return Json(new { type = "msg", messageType = "error", title = "ERROR", message = "Something Error!" });
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}