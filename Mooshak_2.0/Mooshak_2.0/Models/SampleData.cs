using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using Microsoft.AspNet.Identity;
using Mooshak_2._0.Models.Entities;
using Mooshak_2._0.Repositories;

namespace Mooshak_2._0.Models
{
    public class SampleData
    {
        private ApplicationDbContext _db;
        MyApplicationUserManager mgr = new MyApplicationUserManager();
        private readonly ApplicationUserRepository _userRepository = new ApplicationUserRepository();

        public SampleData()
        {
            _db = new ApplicationDbContext();
        }

        public void Seed()
        {
            // Create the admin user
            var user = new ApplicationUser()
            {
                FullName = "Admin Adminsson",
                UserName = "Admin",
                Email = "admin@admin.is"
            };


            var result = mgr.Create(user, "admin123");

            if (result.Succeeded)
            {
               // Find the newly created user
                var adminUser = _userRepository.GetUserByUsername("Admin");
                if (adminUser != null)
                {
                    var adminRole = new Claim("role", "admin");

                    // Add admin role to admin user
                    var result2 = mgr.AddClaim(adminUser.Id, adminRole);

                    if (result2.Succeeded)
                    {
                        
                    }
                }


            }
        }
    }
}