using CrissCargoApp.Database;
using CrissCargoApp.IHelper;
using CrissCargoApp.Migrations;
using CrissCargoApp.Models;
using CrissCargoApp.ViewModel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Numerics;

namespace CrissCargoApp.Controllers
{
    public class CustomersController : Controller
    {

        private readonly IUserHelper _userHelper;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public CustomersController(ApplicationDbContext context, IUserHelper userHelper, UserManager<ApplicationUser> userManager, IWebHostEnvironment webHostEnvironment)
        {
            _userHelper = userHelper;
            _context = context;
            _userManager = userManager;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
       
        [HttpGet]
        public IActionResult MyOrders() 
        {
            return View();
        }
        [HttpGet]
        public IActionResult AddParcel()
        {
            return View();
        }
        public JsonResult GetLoggedInUuerName()
        {
            try
            {
                var username = User.Identity.Name;
                if (username != null)
                {
                    var result = _userHelper.LoggedInUserFullName(username);
                    return Json(result);
                }
                return Json(new { isError = true, msg = "Failed" });
            }
            catch (Exception ex)
            {
                return Json(new { isError = true, msg = "Failed" + ex.Message});
            }

        }
        public IActionResult ContactUs()
        {
            return View();
        }
        [HttpGet]
        public IActionResult UserProfile()
        {
            var loggedInUser = _userHelper.FindUserByUserNameAsync(User.Identity.Name).Result;
            if (loggedInUser != null)
            {
                var application = new ApplicationUser();
                {
                    application.Id = loggedInUser.Id;
                    application.Name = loggedInUser.Name;
                    application.Email = loggedInUser.Email;
                    application.PhoneNumber= loggedInUser.PhoneNumber;
                    application.Address= loggedInUser.Address;
                    application.DateCreated= loggedInUser.DateCreated;
                    return View(application);
                };
            }
            return View();
            
        }

        [HttpGet]
        public IActionResult Faq()
        {
            return View();
        }

        [HttpGet]
        public IActionResult PayForQuotation(int? orderNum)
        {
            if (orderNum != null)
            {
                var model = new OrderPayment();
                model.OrderNo = orderNum;
                return View(model);
            }
            return View();
        }

        [HttpGet]
        public IActionResult PaymentConfirmation(int? orderNum)
        {
            if(orderNum != null)
            {
                var model = new OrderPaymentViewModel();
                model.OrderNo=orderNum;
                return View(model);
            }
            return View();
        }
        [HttpPost]
        public IActionResult PaymentConfirmations(OrderPaymentViewModel model)
        {
            try
            {
                string orderPaymentProofPath = string.Empty;

                if (model.PaymentImageUrl != null)
                {
                    orderPaymentProofPath = UploadedFile(model);
                }
                var saveOrderPayment = new OrderPayment()
                {
                   customersName= model.customersName,
                   CustomersAccountNumber = model.CustomersAccountNumber,
                   CustomersBank = model.CustomersBank,
                   CompanyAccountPaidTo = model.CompanyAccountPaidTo,
                   AmountPaid = model.AmountPaid,   
                   PaymentMethod = model.PaymentMethod, 
                   PaymentImageFileUrl = orderPaymentProofPath,

                };
                if (saveOrderPayment != null)
                {
                    _context.OrderPayments.Add(saveOrderPayment);
                    _context.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(model);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public string UploadedFile(OrderPaymentViewModel filesSender)
        {

            string uniqueFileName = string.Empty;

            if (filesSender.PaymentImageUrl != null)
            {
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "orderpaymentProof");
                string pathString = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "orderpaymentProof");
                if (!Directory.Exists(pathString))
                {
                    Directory.CreateDirectory(pathString);
                }
                uniqueFileName = Guid.NewGuid().ToString() + "_" + filesSender.PaymentImageUrl.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                     filesSender.PaymentImageUrl.CopyTo(fileStream);
                }
            }
            var generatedPictureFilePath = "/orderpaymentProof/" + uniqueFileName;
            return generatedPictureFilePath;
        }
    }
}
