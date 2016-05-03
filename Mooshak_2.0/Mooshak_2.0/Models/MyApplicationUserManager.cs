using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Mooshak_2._0.Models.Entities;

namespace Mooshak_2._0.Models
{
    public class MyApplicationUserManager : UserManager<ApplicationUser>
    {
        public MyApplicationUserManager() : base(new UserStore<ApplicationUser>(new ApplicationDbContext()))
        {
            // TODO: Add our own custom UserValidator, PasswordValidator and ClaimsIdentityFactory
        }
    }
}