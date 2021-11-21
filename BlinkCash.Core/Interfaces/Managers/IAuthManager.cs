using BlinkCash.Core.Models; 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlinkCash.Core.Interfaces.Managers
{
    public interface IAuthManager
    {
        bool IsAnExistingUser(string userName);
        Task<(bool Isvalid, IdentityUserExtension identityUserExtension, string message)> IsValidUserCredentials(string userName, string password);
        Task<(string role, long UseId)> GetUserRole(IdentityUserExtension signedUser);
    }
}
