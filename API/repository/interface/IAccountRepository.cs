using API.ViewModel;

namespace API.repository.Interface
{
    public interface IAccountRepository
    {
        bool login(string email, string password);
        
        //bool sendEmail(string email, string otp);
        bool forgotPassword(string email);

        bool changePassword(ChangePasswordVm changePassword); // buat interface untuk changepasswrod dengan parameter change password dari model
    }
}
