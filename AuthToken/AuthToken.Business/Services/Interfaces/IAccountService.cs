using AuthToken.ViewModels.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthToken.Business.Services.Interfaces
{
    public interface IAccountService
    {
        SignInAccountView SignIn(string email, string password);
    }
}
