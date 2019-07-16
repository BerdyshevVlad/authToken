using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthToken.ViewModels.Models.Auth
{
    public class ConfirmEmailAuthViewModel
    {
        public string UserId { get; set; }
        public string Code { get; set; }
    }
}
