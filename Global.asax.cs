using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;
using SCAPI.MessageHandlers;
using System.Web.Mvc;

namespace SCAPI
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            GlobalConfiguration.Configuration.MessageHandlers.Add(new APIKeyMessageHandler());


            // WebApi Configuration to hook up formatters and message handlers
            RegisterApis(GlobalConfiguration.Configuration);
        }

        public static void RegisterApis(HttpConfiguration config)
        {
            // remove default Xml handler
            var matches = config.Formatters
                                .Where(f => f.SupportedMediaTypes
                                             .Where(m => m.MediaType.ToString() == "application/xml" ||
                                                         m.MediaType.ToString() == "text/xml" ||
                                                         m.MediaType.ToString() == "application/x-www-form-urlencoded")
                                             .Count() > 0)
                                .ToList();
            foreach (var match in matches)
                config.Formatters.Remove(match);
        }
    }
}
