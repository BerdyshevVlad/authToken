using AuthToken.ViewModels.Account.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthToken.ViewModels.Account
{
    public class LogInAuthView
    {
        public string Token { get; set; }
        public LogInAuthViewItem User { get; set; }
    }
}
