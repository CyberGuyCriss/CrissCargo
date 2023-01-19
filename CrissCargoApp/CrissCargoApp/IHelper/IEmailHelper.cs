using CrissCargoApp.Models;

namespace CrissCargoApp.IHelper
{
    public interface IEmailHelper
    {
        bool SendProcurementConfirmationEmail(int orderNo);
        bool SendRegistrationConfirmationEmail(ApplicationUser userRegisteringDetail);
        bool SendQuotationToCustomer(ApplicationUser customerDetail, int totalAmount, int orderNo);
    }
}
