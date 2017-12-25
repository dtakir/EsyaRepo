using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using Microsoft.Owin.Security.Cookies;

[assembly: OwinStartup(typeof(EsyaStore.App_Start.Startup))]

namespace EsyaStore.App_Start
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
           app.UseCookieAuthentication(new CookieAuthenticationOptions

            {

                AuthenticationType = "ApplicationCookie",

                LoginPath = new PathString("/Account/Login")

            });
        }
    }
}
