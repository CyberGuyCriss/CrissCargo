using CrissCargoApp.Database;
using CrissCargoApp.IHelper;
using CrissCargoApp.Migrations;
using CrissCargoApp.Models;
using CrissCargoApp.SmtpMailServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Net.Mail;
using static NuGet.Packaging.PackagingConstants;

namespace CrissCargoApp.Controllers
{
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserHelper _userHelper;
        private readonly IEmailHelper _emailHelper;


        public AdminController(UserManager<ApplicationUser> userManager, ApplicationDbContext context, IUserHelper userHelper, IEmailHelper emailHelper)
        {
            _context = context;
            _userManager = userManager;
            _userHelper = userHelper;
            _emailHelper = emailHelper;


        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        //to get the list of submitted orders
        [HttpGet]
        public IActionResult AllOders()
        {
            var order = _userHelper.CustomerOrder();
            return View(order);
        }

        [HttpGet]
        public IActionResult EditOrder(int id)
        {
            if (id == 0) 
            {
                return NotFound();
            }
            var OrderToBeEditred = _context.Procurements.Where(x => x.Id == Guid.Empty).FirstOrDefault();
            return View(OrderToBeEditred);
        }

        [HttpPost]
        public IActionResult EditOrders(Procurement model)
        {
            if (model == null)
            {
                return NotFound(model);
            }
            var OrderToBeEditred = _context.Procurements.Where(x => x.Id == Guid.Empty).FirstOrDefault();

            if (OrderToBeEditred != null)
            {
                _context.Procurements.Update(OrderToBeEditred);
                _context.SaveChanges();
                return View(AllUsers());

            }
            return View();

        }


        
        //to get a list of all registered Users
        [HttpGet]
        public IActionResult AllUsers()
        {
            IEnumerable<ApplicationUser> user = _context.ApplicationUsers.ToList();
            return View(user);
        }

        [HttpGet]
        public IActionResult Edit(string? id)
        {

            if (id == null)
            {
                return NotFound();
            }
            var userToBeEdited = _context.ApplicationUsers.Where(x => x.Id == id).FirstOrDefault();
            return View(userToBeEdited);
        }

        [HttpPost]
        public IActionResult EditPost(ApplicationUser model)
        {

            if (model == null)
            {
                return NotFound();
            }
            var userToBeEdited = _context.ApplicationUsers.Where(x => x.Id == model.Id).FirstOrDefault();
            if (userToBeEdited != null)
            {

                userToBeEdited.Name = model.Name;
                userToBeEdited.Email = model.Email;
                userToBeEdited.Address = model.Address;
                userToBeEdited.PhoneNumber = model.PhoneNumber;
                userToBeEdited.DateCreated = model.DateCreated;
            }
            _context.ApplicationUsers.Update(userToBeEdited);
            _context.SaveChanges();
            return View();
        }

        //public IActionResult PrepareQuotation()
        //{
        //    return View();
        //}

        public JsonResult? GetOrderByOrderNumber(int orderNo) 
        {
            if (orderNo > 0)
            {
                var orders = _userHelper.FindOrdersByOrderNumber(orderNo);
                return Json(orders);
            }
            return null;
        } 
        
        public IActionResult PrepareQuotation(int orderNo) 
        {
            if (orderNo > 0)
            {
                var orders = _userHelper.FindOrdersByOrderNumber(orderNo);
                return View(orders);
            }
            return null;
        }
        //public JsonResult GetOrderByCustomerId(string customerId)
        //{
        //    if (customerId != null)
        //    {
        //        var customers = _userHelper.FindOrdersByCustomerId(customerId);
        //        return Json(customers);
        //    }
        //    return null;
        //}


        //public IActionResult AllPayment()
        //{
        //    var model = new orderPayments();
        //    var AllPayment = _context.OrderPayments.ToList();
        //    if (AllPayment.Any())
        //    {
        //        return View(AllPayment);
        //    }
        //    else
        //    {
        //        return View(model);
        //    }
        //}
        //GET || Search
        [HttpGet]
        public IActionResult AllPayment()
        {
            var AllPayment = _context.OrderPayments.Where(s => s.Id != null && s.PaymentImageFileUrl != null).ToList();
            if (AllPayment != null && AllPayment.Count() > 0)
            {
                return View(AllPayment);
            }

            return View();
        }

        [HttpPost]
        public IActionResult PrepareQuotation(int orderNo, int totalAmount)
        {
            if (orderNo > 0)
            {

                var procurement = _userHelper.FindOrderByOrderNumber(orderNo);
                if (procurement.CustomerId != null)
                {
                    var customerDetails = _userHelper.GetFullCustomerDetailsByCustomerId(procurement.CustomerId);
                    if(customerDetails != null)
                    {
                        _emailHelper.SendQuotationToCustomer(customerDetails, totalAmount, orderNo);
                    }
                }
                return Json(new { isError = false, msg = "Quotaion has been sent successfully!" });
            }
            return Json(new { isError = true, msg = "Quotation could not be sent, Please Check your Quotation page for Error!" });
        }
    }  
}

