using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace DASOEN
{
    public partial class Startup
    {
        /*public void Configuration(IAppBuilder app)
        {
            HttpConfiguration config = new HttpConfiguration();       

            ConfigureAuth(app);

            WebApiConfig.Register(config);
            //app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            app.UseWebApi(config);
        }*/
        public void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration();
            ConfigureAuth(app);
            app.UseWebApi(config);
            //Controllers.ManteController.SetTimer();

            //HttpConfiguration config = new HttpConfiguration();

            //ConfigureAuth(app);
            //WebApiConfig.Register(config);
            //app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            //app.UseWebApi(config);
            //Controllers.ManteController.SetTimer();
        }


    }
}