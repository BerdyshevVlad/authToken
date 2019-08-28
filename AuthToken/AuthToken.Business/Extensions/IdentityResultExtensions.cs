using Microsoft.AspNetCore.Identity;
using System.Text;

namespace AuthToken.Business.Extensions
{
    public static class IdentityResultExtensions
    {
        public static string GetErrors(this IdentityResult result)
        {
            var message = new StringBuilder();
            foreach (var error in result.Errors)
            {
                message.Append($"{error.Description} ");
            }
            return message.ToString();
        }
    }
}
