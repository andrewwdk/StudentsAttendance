using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(StudentsAttendanceWebsite.Startup))]
namespace StudentsAttendanceWebsite
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
