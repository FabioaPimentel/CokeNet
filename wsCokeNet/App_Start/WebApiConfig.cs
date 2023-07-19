using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;

namespace wsCokeNet
{
	public static class WebApiConfig
	{
		public static void Register(HttpConfiguration config)
		{
			// Configuração e serviços de API Web

			// Rotas de API Web
			config.MapHttpAttributeRoutes();
			var cors = new EnableCorsAttribute("*", "*", "*");
			config.EnableCors(cors);

			config.Routes.MapHttpRoute(
				name: "DefaultApi",
				routeTemplate: "api/{controller}/{action}/{id}/{id2}",
				defaults: new { controller = "Home", action = "Index", id = RouteParameter.Optional, id2 = RouteParameter.Optional }
			);
		}
	}
}
