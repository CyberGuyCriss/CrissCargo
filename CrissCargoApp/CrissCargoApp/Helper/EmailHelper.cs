using CrissCargoApp.Config;
using CrissCargoApp.Controllers;
using CrissCargoApp.Database;
using CrissCargoApp.IHelper;
using CrissCargoApp.Models;
using CrissCargoApp.SmtpMailServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CrissCargoApp.Helper
{
    public class EmailHelper : IEmailHelper
    {
        private readonly ApplicationDbContext _context;

        private readonly IGeneralConfiguration _generalConfiguration;
        private string Customer;
        private string orderNo;
        private readonly IEmailService _emailService;

        public EmailHelper( ApplicationDbContext context , IEmailService emailService, IGeneralConfiguration generalConfiguration )
        {
            _context = context;
            _emailService = emailService;
            _generalConfiguration = generalConfiguration;
        }
        public bool SendProcurementConfirmationEmail(int orderNo)
        {
            if(orderNo > 0)
            {
                var orderDetail = _context.Procurements.Where(o => o.OrderNo == orderNo).Include(c=>c.Customers).FirstOrDefault();
                string toEmail = orderDetail.Customers.Email;
                string subject = "PROCUREMENT ORDER SUBMITTED SUCEFFULY";
                string message = "Dear " + orderDetail.Customers.Name + "," + "Your order with " + orderNo + "has  been submitted Successfully and being processed." + "<br>" + "Your quotation will be ready within 24 hours. Thank you </br>";
                _emailService.SendEmail(toEmail, subject, message);  
                return true;
            }
            return false;
        }

        public bool SendRegistrationConfirmationEmail(ApplicationUser userRegisteringDetail)
        {
            //if (customerId > 0)
            //{
                string toEmail = userRegisteringDetail.Email;
                string subject = "REGISTRATION SUCCESSFFUL";
                string message = "Dear"+", " + userRegisteringDetail.Name +", " +"Your Account has been succefully created, Click the link below to login Create Create your first Order." + "<br>"+ "User Login: <a href=\"https://localhost:44350/Account/Login\">Click Here to LOGIN</a>" + "<br>" ;
            _emailService.SendEmail(toEmail, subject, message);
                return true;
            //}
            //return false;
        }

        public bool SendQuotationToCustomer(ApplicationUser customerDetail, int totalAmount, int orderNo)
        {
            //if (customerId > 0)
            //{
            string toEmail = customerDetail.Email;
            string subject = "YOUR INVOICE FOR Order Number: " + orderNo + "IS RAEDY";
            string message = "Dear " + customerDetail.Name + ", " + "Your order with invoice number " + orderNo + "is ready for procurement with a total Amount of "+totalAmount+"."+"Kindly payment click the link provided below to proceed with Your order payment"+ "<br>" + "User Login: <a href=\"https://localhost:44350/Customers/PayForQuotation?orderNumber="+orderNo +"\">Click Here to Make PAYMENT</a>" + "<br>";
            _emailService.SendEmail(toEmail, subject, message);
            return true;
            //}
            //return false;
        }

    }
}
