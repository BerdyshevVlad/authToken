using AuthToken.ViewModels.Account;
using AuthToken.ViewModels.Models.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthToken.Business.Services.Interfaces
{
    public interface IAccountService
    {
        LogInAuthView LogIn(string email, string password);
        void SignUp(SignUpAuthViewModel model);
        bool ConfirmEmail(ConfirmEmailAuthViewModel model);
    }
}
