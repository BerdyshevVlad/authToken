using AuthToken.ViewModels.Account;
using AuthToken.ViewModels.Models.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthToken.Business.Services.Interfaces
{
    public interface IAuthService
    {
        LogInAuthView LogIn(LogInAuthViewModel model);
        void SignUp(SignUpAuthViewModel model);
        bool ConfirmEmail(ConfirmEmailAuthViewModel model);
    }
}
