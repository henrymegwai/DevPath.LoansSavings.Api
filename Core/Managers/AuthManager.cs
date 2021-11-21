using BlinkCash.Core.Interfaces.Managers;
using BlinkCash.Core.Models; 
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlinkCash.Core.Managers
{
    public class AuthManager : IAuthManager
    {
        private readonly ILogger<AuthManager> _logger;
        private readonly SignInManager<IdentityUserExtension> _signInManager;
        private readonly UserManager<IdentityUserExtension> _userManager;

        private readonly IDictionary<string, string> _users = new Dictionary<string, string>
        {
            { "test1", "password1" },
            { "test2", "password2" },
            { "admin", "securePassword" }
        }; 

        public AuthManager(ILogger<AuthManager> logger, SignInManager<IdentityUserExtension> signInManager,
            UserManager<IdentityUserExtension> userManager)
        {
            _logger = logger;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public async Task<(bool Isvalid, IdentityUserExtension identityUserExtension, string message)> IsValidUserCredentials(string userName, string password)
        {
            _logger.LogInformation($"Validating user [{userName}]");
            if (string.IsNullOrWhiteSpace(userName))
            {
                return (false, null, "Invalid credentials");
            }

            else if (string.IsNullOrWhiteSpace(password))
            {
                return (false, null, "Invalid credentials");
            }

            var signedUser = await _userManager.FindByNameAsync(userName);
            if (signedUser == null)
            {
                return (false, null, "User information could not be verified. Please contact an Administrator.");

            }
            //Get roles of User
            var role = await _userManager.GetRolesAsync(signedUser);
            if (!role.Any())
            {
                return (false, null, "You do not have an assigned role in this system. Please contact an Administrator.");

            }

            var result = await _signInManager.PasswordSignInAsync(userName, password, true, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                return (true, signedUser, "Login was successful.");

            }
            else
            {
                return (false, null, "User credentials could not be determined. Please cotact Administrator.");
            }
        }

        public bool IsAnExistingUser(string userName)
        {
            return _users.ContainsKey(userName);
        }

        public async Task<(string role, long UseId)> GetUserRole(IdentityUserExtension signedUser)
        {
            var roles = await _userManager.GetRolesAsync(signedUser);
            string role = roles.FirstOrDefault();
            long userId = 0;
            //switch (role)
            //{
            //    case UserRoles.Builder:
            //        userId = (await _builderManager.GetByIdentifier(signedUser.Id)).Id;
            //        break;
            //    case UserRoles.Contractor:
            //        userId = (await _contractorManager.GetByUserId(signedUser.Id)).Id;
            //        break;
            //    case UserRoles.ProjectManager:
            //        userId = (await _builderProjectsManager.GetByUserId(signedUser.Id)).ProjectManagerId;
            //        break;
            //    case UserRoles.Client:
            //        userId = (await _clientManager.GetbyUserId(signedUser.Id)).Id;
            //        break;
            //}

            return (role, userId);
        }



    }
}
