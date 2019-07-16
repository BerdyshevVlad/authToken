using System;

namespace AuthToken.ViewModels.Models.Auth
{
    public class SignUpAuthViewModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Birthday { get; set; }
    }
}
