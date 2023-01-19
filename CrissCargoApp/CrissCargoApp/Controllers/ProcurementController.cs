using CrissCargoApp.IHelper;
using CrissCargoApp.Models;
using CrissCargoApp.SmtpMailServices;
using CrissCargoApp.ViewModel;
using MailKit;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CrissCargoApp.Controllers
{
    public class ProcurementController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserHelper _userHelper;
        private object mailService;

        public ProcurementController(UserManager<ApplicationUser> userManager, IUserHelper userHelper)
        {
            _userManager = userManager;
            _userHelper = userHelper;
        }

        [HttpGet]

        public IActionResult CreateProcurement()
        {
            return View();
        }
        [HttpGet]
        public IActionResult AddPicture()
        {
            return View();
        }



        [HttpPost]
        public JsonResult CreateProcurements(string model)
        {
            try
            {
                var customerID = _userManager.FindByNameAsync(User.Identity.Name).Result.Id;
                if (model != null)
                {


                    var unprocessedOrder = JsonConvert.DeserializeObject<List<ProcurementViewModel>>(model);
                    var result = _userHelper.CreateOrder(unprocessedOrder, customerID);
                    if (result)
                    {
                        return Json(new { isError = false, msg = "Created Successfully" });
                    }
                    else
                    {
                        return Json(new { isError = true, msg = "Procurement couldn't be processed" });
                    }
                }
                return Json(new { isError = true, msg = "Internal error occured" });

            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
