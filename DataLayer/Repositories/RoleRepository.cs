using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace DataLayer.Repositories
{
    public interface IRoleRepository
    {
        Task<IdentityRole<Guid>> GetRoleByNameAsync(string roleName);
        Task<IList<Claim>> GetClaimsByRoleAsync(IdentityRole<Guid> role);
    }

    public class RoleRepository : IRoleRepository
    {
        private readonly RoleManager<IdentityRole<Guid>> _roleManager;

        public RoleRepository(RoleManager<IdentityRole<Guid>> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task<IdentityRole<Guid>> GetRoleByNameAsync(string roleName)
        {
            return await _roleManager.FindByNameAsync(roleName);
        }

        public async Task<IList<Claim>> GetClaimsByRoleAsync(IdentityRole<Guid> role)
        {
            return await _roleManager.GetClaimsAsync(role);
        }
    }
}
