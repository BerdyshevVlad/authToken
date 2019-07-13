using AuthToken.Business.Services.Interfaces;
using AuthToken.DataAccess.Entities;
using AuthToken.ViewModels.Account;
using AuthToken.ViewModels.Account.Items;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace AuthToken.Business.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountService(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public SignInAccountView SignIn(string email,string password)
        {
            ApplicationUser identityUser = _userManager.Users.Include(u => u.UserRoles).ThenInclude(ur => ur.Role)
                .SingleOrDefault(x => x.NormalizedUserName == email.ToUpper());

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

            var signInResult =  _signInManager.PasswordSignInAsync(identityUser, password, false, true).GetAwaiter().GetResult();
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
            var userData = new SignInAccountViewItem
            {
                UserEmail = identityUser.Email,
                UserId = identityUser.Id,
                UserName = $@"{identityUser.FirstName ?? string.Empty}
                              {identityUser.LastName ?? string.Empty}",
                UserRoleId = identityRole
            };

            var authData = new SignInAccountView
            {
                Token = authToken,
                User = userData
            };

            return authData;
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
                "http://localhost:1194",
                "http://localhost:1194",
                claims,
                expires: expires,
                signingCredentials: creds
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
