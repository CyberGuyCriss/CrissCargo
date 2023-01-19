using CrissCargoApp.Models;
using CrissCargoApp.ViewModel;

namespace CrissCargoApp.IHelper
{
    public interface IUserHelper
    {
        Task<ApplicationUser> FindUserByNameAsync(string name);
        Task<ApplicationUser> UserRegistertion(ApplicationUserViewModel applicationUserViewModel);
        Task<ApplicationUser> AdminRegistertion(ApplicationUserViewModel applicationUserViewModel);
        Task<ApplicationUser> FIndUserByPhoneNumber(string phonenumber);
        Task<ApplicationUser> FindUserByEmailAsync(string? email);
        bool CreateOrder(List<ProcurementViewModel> procurementViewModel, string orderNumber);
        string LoggedInUserFullName(string userName);
        List<Procurement> CustomerOrder();
        List<Procurement> FindOrdersByOrderNumber(int orderNumber);
        Task<ApplicationUser> FindUserByUserNameAsync(string userName);
        Procurement FindOrderByOrderNumber(int orderNumber);
        ApplicationUser GetFullCustomerDetailsByCustomerId(string customerId);
        //List<Procurement> FindOrdersByCustomerId(string customerId);
    }
}
