using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthToken.ViewModels.Models.Auth
{
    public class ChangePasswordAuthViewModel
    {
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
