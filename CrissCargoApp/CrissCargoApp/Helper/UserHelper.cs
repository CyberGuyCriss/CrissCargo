using CrissCargoApp.Database;
using CrissCargoApp.Enum;
using CrissCargoApp.IHelper;
using CrissCargoApp.Models;
using CrissCargoApp.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.Build.ObjectModelRemoting;
using Microsoft.EntityFrameworkCore;
using NuGet.Packaging;
using System.Diagnostics;
using System.Drawing;

namespace CrissCargoApp.Helper
{
    public class UserHelper: IUserHelper
    {
        public readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;
        private readonly IEmailHelper _emailHelper;
        private object u;

        public UserHelper(UserManager<ApplicationUser> userManager, ApplicationDbContext context, IEmailHelper emailHelper)
        {
            _userManager = userManager;
            _context = context;
            _emailHelper = emailHelper;

        }
        public async Task<ApplicationUser> FindUserByNameAsync(string name)
        {
            return await _userManager.Users.Where(x => x.Name == name)?.FirstOrDefaultAsync();
        }

        public async Task<ApplicationUser> FindUserByUserNameAsync(string userName)
        {
            return await _userManager.Users.Where(x => x.UserName == userName)?.FirstOrDefaultAsync();
        }
        public async Task<ApplicationUser> FindUserByEmailAsync(string? email) 
        {
           var fullUserDetails = await _userManager.Users.Where(x => x.Email == email).FirstOrDefaultAsync();
            if (fullUserDetails != null)
            {
                return fullUserDetails;
            }
            else
            {
                return null;
            }
        }

        public async Task<ApplicationUser> UserRegistertion(ApplicationUserViewModel applicationUserViewModel)
        {
            if (applicationUserViewModel != null)
            {
                var applicationUser = new ApplicationUser()
                {

                    Name = applicationUserViewModel.Name,
                    Email = applicationUserViewModel.Email,
                    UserName = applicationUserViewModel.Email,
                    PhoneNumber = applicationUserViewModel.PhoneNumber,
                    Address= applicationUserViewModel.Address,
                    IsDeleted = false,
                    DateCreated= DateTime.Now,
                };

                if (applicationUser.Email != null && applicationUserViewModel.Password != null)
                {

                    var result = await _userManager.CreateAsync(applicationUser, applicationUserViewModel.Password).ConfigureAwait(false);
                    if (result.Succeeded)
                    {
                        _emailHelper.SendRegistrationConfirmationEmail(applicationUser);
                        return applicationUser;
                    }
                }  
          
            }

            return null;
        }






        public async Task<ApplicationUser> AdminRegistertion(ApplicationUserViewModel applicationUserViewModel)
        {
            if (applicationUserViewModel != null)
            {
                var applicationUser = new ApplicationUser()
                {

                    Name = applicationUserViewModel.Name,
                    Email = applicationUserViewModel.Email,
                    UserName = applicationUserViewModel.Email,
                    PhoneNumber = applicationUserViewModel.PhoneNumber,
                    Address = applicationUserViewModel.Address,
                    IsDeleted = false,
                    DateCreated = DateTime.Now,
                };

                if (applicationUser.Email != null && applicationUserViewModel.Password != null)
                {

                    var result = await _userManager.CreateAsync(applicationUser, applicationUserViewModel.Password).ConfigureAwait(false);
                    if (result.Succeeded)
                    {
                        _emailHelper.SendRegistrationConfirmationEmail(applicationUser);
                        return applicationUser;
                    }
                }

            }

            return null;
        }








        public async Task<ApplicationUser?> FIndUserByPhoneNumber(string phonenumber)
        {
            if(phonenumber != null)
            {
                var user = _userManager.Users.Where(u => u.PhoneNumber == phonenumber).FirstOrDefault();
                if (user != null)
                {

                    return user;
                }
                
            }
            return null;
        }

        public bool CreateOrder(List<ProcurementViewModel> procurementViewModel, string customerID)
        {
            if (procurementViewModel.Count > 0 && customerID != null)
            {
                var orderNumber = Generate();
                var orders = new List<Procurement>();
                foreach (var order in procurementViewModel)
                {
                    var newOrder = new Procurement()
                    {
                        OrderNo = orderNumber,
                        CustomerId = customerID,
                        ProductName = order.ProductName,
                        ProductLink = order.ProductLink,
                        ProductPicture = order.ProductPicture,
                        Quantity = order.Quantity,
                        Colour = order.Colour,
                        Size = order.Size,
                        MoreDescription = order.MoreDescription,
                        DateSubmitted = DateTime.Now,
                        ProcurementStatus = OrderStatus.PendingOrder,
                        PaymentForProcurement = PaymentStatus.Unpaid,

                    };
                    orders.Add(newOrder);
                }
                _context.Procurements.AddRange(orders);
                _context.SaveChanges();
                _emailHelper.SendProcurementConfirmationEmail(orderNumber);
                return true;
            }
            return false;
        }

        public static int Generate()
        {
            Random rand = new Random((int)DateTime.Now.Ticks);
            return rand.Next(100000000, 999999999);
        }

        public string LoggedInUserFullName(string userName)
        {
            if(userName != null)
            {
                var myName = _userManager.Users.Where(u => u.UserName == userName).FirstOrDefault().Name;
                if (myName != null)
                {
                    return myName;
                }
            }
            return "User Name?";
        }

        public List<Procurement> CustomerOrder()
        {
            var myOrder = new List<Procurement>();
            var list = _context.Procurements.Where(p => p.Id != Guid.Empty).Include(c=>c.Customers).OrderByDescending(o=>o.DateSubmitted).ToList();
            if(list.Any())
            {
                myOrder = list.GroupBy(o => o.OrderNo).Select(o => o.First()).Distinct().ToList();
                return myOrder;
            }
            return myOrder;
        }
        public List<Procurement> FindOrdersByOrderNumber(int orderNumber)
        {
            var myOrder = new List<Procurement>();
            if (orderNumber > 0)
            {
                var list = _context.Procurements.Where(p => p.Id != Guid.Empty && p.OrderNo == orderNumber).ToList();
                if (list.Any())
                {
                    myOrder = list;
                    return myOrder;
                }
            }
            return myOrder;
        }
        public Procurement FindOrderByOrderNumber(int orderNumber)
        {
            var myOrder = new Procurement();
            if (orderNumber > 0)
            {
                var customer = _context.Procurements.Where(p => p.Id != Guid.Empty && p.OrderNo == orderNumber).FirstOrDefault();
                if (customer != null)
                {
                    myOrder = customer;
                    return myOrder;
                }
            }
            return myOrder;
        }

        public ApplicationUser GetFullCustomerDetailsByCustomerId(string customerId)
        {
            
            if (customerId != null)
            {
                var customer= _userManager.Users.Where(u => u.Id == customerId).FirstOrDefault();
                if (customer != null)
                {
                    return customer;
                }
            }
            return null;
        }
        //public List<Procurement> FindOrdersByCustomerId(string customerId)
        //{
        //    var myOrders = new List<Procurement>();
        //    if (customerId != null)
        //    {
        //        var list = _context.Procurements.Where(s => s.Id != Guid.Empty && s.CustomerId == customerId).ToList();
        //        if (list.Any())
        //        {
        //            myOrders = list;
        //            return myOrders;
        //        }
        //    }
        //    return myOrders;
        //}




    }
}
