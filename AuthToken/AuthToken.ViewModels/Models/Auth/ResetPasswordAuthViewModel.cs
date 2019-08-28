using System.ComponentModel.DataAnnotations;

namespace AuthToken.ViewModels.Models.Auth
{
    public class ResetPasswordAuthViewModel
    {
        [Required]
        public string UserId { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string Code { get; set; }
    }
}
