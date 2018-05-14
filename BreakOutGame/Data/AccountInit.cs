using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BreakOutGame.Models;
using Microsoft.AspNetCore.Identity;

namespace BreakOutGame.Data
{
    public class AccountInit
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<ApplicationUser> _userManager;
        public AccountInit(ApplicationDbContext dbContext, UserManager<ApplicationUser> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }
        public async Task InitializeUsersAndCustomers()
        {
            string eMailAddress = "pieterPod@hogent.be";
            ApplicationUser user = new ApplicationUser { UserName = eMailAddress, Email = eMailAddress };
            await _userManager.CreateAsync(user, "P@ssword1");
            await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.Role, "teacher"));
            eMailAddress = "ano.niem@hogent.be";
            user = new ApplicationUser { UserName = eMailAddress, Email = eMailAddress };
            await _userManager.CreateAsync(user, "P@ssword1");
            await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.Role, "teacher"));

            _dbContext.SaveChanges();
        }
    }
}
