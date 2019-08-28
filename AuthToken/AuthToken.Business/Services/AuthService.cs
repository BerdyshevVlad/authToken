using AuthToken.Business.Extensions;
using AuthToken.Business.Services.Interfaces;
using AuthToken.DataAccess.Entities;
using AuthToken.ViewModels.Account;
using AuthToken.ViewModels.Account.Items;
using AuthToken.ViewModels.Models.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Web;

namespace AuthToken.Business.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailSender _emailSender;
        private readonly IConfiguration _configuration;

        public AuthService(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager, IEmailSender emailSender,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _configuration = configuration;
        }

        public LogInAuthView LogIn(LogInAuthViewModel model)
        {
            //model.Password= "Qwe123!!!";
            //model.Email = "berdyshev1997@gmail.com";
            ApplicationUser identityUser = _userManager.Users.Include(u => u.UserRoles).ThenInclude(ur => ur.Role)
                .SingleOrDefault(x => x.NormalizedUserName == model.Email.ToUpper());

            if (identityUser == null)
            {
                throw new ApplicationException("User not found.");
            }

            var identityRole = identityUser.UserRoles.SingleOrDefault()?.Role.Name;

            var isConfirm = _userManager.IsEmailConfirmedAsync(identityUser).GetAwaiter().GetResult();
            if (!isConfirm)
            {
                throw new ApplicationException("Email not confirmed.");
            }

            var signInResult =  _signInManager.PasswordSignInAsync(identityUser, model.Password, false, true).GetAwaiter().GetResult();
            if (signInResult == SignInResult.Failed)
            {
                throw new ApplicationException("Invalid login attempt.");
            }
            if (signInResult == SignInResult.LockedOut)
            {
                throw new ApplicationException(@"Invalid login attempt.
                                Your account has been blocked for 10 minutes.");
            }

            var authToken = GenerateJwtToken(identityUser);
            var userData = new LogInAuthViewItem
            {
                UserEmail = identityUser.Email,
                UserId = identityUser.Id,
                UserName = $@"{identityUser.FirstName ?? string.Empty}
                              {identityUser.LastName ?? string.Empty}",
                UserRoleId = identityRole
            };

            var authData = new LogInAuthView
            {
                Token = authToken,
                User = userData
            };

            return authData;
        }


        public void SignUp(SignUpAuthViewModel model)
        {
            var user = new ApplicationUser
            {
                UserName = model.Email,
                Email = model.Email,
                LastName = model.LastName,
                FirstName = model.FirstName
            };
            var result = _userManager.CreateAsync(user, model.Password).GetAwaiter().GetResult();
            if (!result.Succeeded)
            {
                throw new ApplicationException(result.Errors.FirstOrDefault()?.Description);
            }

            var appUser = _userManager.Users.SingleOrDefault(r => r.Email == model.Email);
            _userManager.AddToRoleAsync(appUser, "user").GetAwaiter().GetResult();

            var code = _userManager.GenerateEmailConfirmationTokenAsync(user).GetAwaiter().GetResult();
            string codeHtmlVersion = HttpUtility.UrlEncode(code);

            var baseUrl = _configuration["BackEndUrl"];
            var confirmLink = $"{baseUrl}api/auth/confirmEmail?userId={user.Id}&code={codeHtmlVersion}";

            var subject = "Welcome to App";
            var test =_emailSender.SendMail(user.Email, subject, confirmLink);
        }


        public bool ConfirmEmail(ConfirmEmailAuthViewModel model)
        {
            if (string.IsNullOrEmpty(model.UserId) || string.IsNullOrEmpty(model.Code))
            {
                throw new ApplicationException("User not found.");
            }
            ApplicationUser user = _userManager.FindByIdAsync(model.UserId).GetAwaiter().GetResult();

            if (user == null)
            {
                throw new ApplicationException("User not found.");
            }

            var validCode = model.Code.Replace(" ", "+");
            var confirmEmailResult = _userManager.ConfirmEmailAsync(user, validCode).GetAwaiter().GetResult();
            if (!confirmEmailResult.Succeeded)
            {
                throw new ApplicationException("User confirm email error.");
            }

            return confirmEmailResult.Succeeded;
        }


        public void ForgotPassword(ForgotPasswordAuthViewModel model)
        {
            var user = _userManager.FindByEmailAsync(model.Email).Result;
            if (user == null)
            {
                throw new ApplicationException("User not found.");
            }
            var isEmailConfirmed = _userManager.IsEmailConfirmedAsync(user).Result;

            if (!isEmailConfirmed)
            {
                throw new ApplicationException("Email is not confirmed.");
            }

            var code = _userManager.GeneratePasswordResetTokenAsync(user).Result;

            var url =$"{_configuration["FrontEndUrl"]}authorization/reset-password?userId={HttpUtility.UrlEncode(user.Id)}&code={HttpUtility.UrlEncode(code)}";

            var subject = "ForgotPassword";
            _emailSender.SendMail(user.Email, subject, url);
        }


        public void ResetPassword(ResetPasswordAuthViewModel model)
        {
            var userId = HttpUtility.UrlDecode(model.UserId);
            var user = _userManager.FindByIdAsync(userId).Result;
            if (user == null)
            {
                throw new ApplicationException("User not found.");
            }

            var validCode = model.Code.Replace(" ", "+");
            var result = _userManager.ResetPasswordAsync(user, validCode, model.Password).Result;
            if (!result.Succeeded)
            {
                throw new ApplicationException(result.GetErrors());
            }
        }


        public void ChangePassword(string email, ChangePasswordAuthViewModel model)
        {
            var user = _userManager.FindByEmailAsync(email).GetAwaiter().GetResult();
            if (user == null)
            {
                throw new ApplicationException("User not found.");
            }

            var result = _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword).GetAwaiter().GetResult();
            if (!result.Succeeded)
            {
                throw new ApplicationException(result.GetErrors());
            }

            _signInManager.SignInAsync(user, isPersistent: false).GetAwaiter().GetResult();
        }


        private string GenerateJwtToken(ApplicationUser user)
        {
            var userRoles = _userManager.GetRolesAsync(user).GetAwaiter().GetResult();

            var userFullName = $"{user.FirstName ?? string.Empty} {user.LastName ?? string.Empty}";
            var claims = new List<Claim>
            {
                new Claim("UserId", user.Id),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Name, userFullName),
                new Claim(ClaimTypes.GivenName, user.FirstName ?? string.Empty),
                new Claim(ClaimTypes.Surname, user.LastName ?? string.Empty),
                new Claim(ClaimTypes.Role, user.UserRoles.SingleOrDefault().ToString() ?? string.Empty)
            };
            claims.AddRange(userRoles.Select(role => new Claim(ClaimTypes.Role, role)).ToList());

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("SOME_RANDOM_KEY_DO_NOT_SHARE"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(Convert.ToDouble("7"));

            var token = new JwtSecurityToken(
                _configuration["BackEndUrl"],
                _configuration["BackEndUrl"],
                claims,
                expires: expires,
                signingCredentials: creds
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
