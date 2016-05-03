using AutoMapper;
using Microsoft.Owin;
using Mooshak_2._0.Controllers;
using Mooshak_2._0.Models.Entities;
using Mooshak_2._0.Models.ViewModels;
using Owin;

[assembly: OwinStartupAttribute(typeof(Mooshak_2._0.Startup))]
namespace Mooshak_2._0
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);

            // Mapper config
            Mapper.Initialize(config =>
            {
                config.CreateMap<Course, CourseViewModel>().ReverseMap();
				config.CreateMap<ApplicationUser, ApplicationUserViewModel>().ReverseMap();
			});
        }
    }
}
