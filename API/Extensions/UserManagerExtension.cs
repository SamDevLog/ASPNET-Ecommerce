using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace API.Extensions {
    public static class UserManagerExtension {
        public static async Task<AppUser> FindUserByClaimsPrincipalWithAddressAsync (this UserManager<AppUser> input, ClaimsPrincipal user) {
            var email = user.FindFirstValue (ClaimTypes.Email);

            return await input.Users.Include (a => a.Address).SingleOrDefaultAsync (e => e.Email == email);
        }

        public static async Task<AppUser> FindByEmailFromClaimsPrincipalAsync (this UserManager<AppUser> input, ClaimsPrincipal user) {
            var email = user.FindFirstValue (ClaimTypes.Email);

            return await input.Users.SingleOrDefaultAsync (e => e.Email == email);

        }

    }
}