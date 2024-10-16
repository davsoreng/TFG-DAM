using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;
using DASOEN.provider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DASOEN
{
    public partial class Startup
    {
        public static OAuthAuthorizationServerOptions OAuthOptions { get; private set; }

        static Startup()
        {
            OAuthOptions = new OAuthAuthorizationServerOptions
            {
                TokenEndpointPath = new PathString("/Login"),
                Provider = new OAuthAppProvider(),
                AccessTokenExpireTimeSpan = TimeSpan.FromSeconds(60),
                AllowInsecureHttp = true,
                RefreshTokenProvider = new ApplicationRefreshTokenProvider()
            };
            //OAuthOptions = new OAuthAuthorizationServerOptions
            //{
            //    TokenEndpointPath = new PathString("/Refresh"),
            //    Provider = new OAuthAppProvider(),
            //    AccessTokenExpireTimeSpan = TimeSpan.FromMinutes(30),
            //    RefreshTokenProvider = new ApplicationRefreshTokenProvider()
            //};
        }

        public void ConfigureAuth(IAppBuilder app)
        {
            app.UseOAuthBearerTokens(OAuthOptions);
        }
    }
}