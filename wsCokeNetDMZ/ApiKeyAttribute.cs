using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using wsCokeNetDMZ.Services;
using System.Text;

namespace wsCokeNetDMZ
{
	[AttributeUsage(validOn: AttributeTargets.Class | AttributeTargets.Method)]
	public class ApiKeyAttribute : Attribute, IAsyncActionFilter
	{
		private const string ApiKeyName = "api_key";
		//private const string ApiKey = "balta_demo_IlTevUM/z0ey3NwCV/unWg==";

		public async Task OnActionExecutionAsync(
		ActionExecutingContext context,
		ActionExecutionDelegate next)
		{
			var appSettings = context.HttpContext.RequestServices.GetRequiredService<IConfiguration>();
			var ambiente = appSettings.GetValue<string>("AppAmbiente");
			//if (!ambiente.ToUpper().Equals("DEV"))
			//{

				if (!context.HttpContext.Request.Headers.TryGetValue(ApiKeyName, out var extractedApiKey))
				{
					context.Result = new ContentResult()
					{
						StatusCode = 401,
						Content = "ApiKey não encontrada"
					};
					return;
				}
				/*
				if (!ApiKey.Equals(extractedApiKey))
				{
					context.Result = new ContentResult()
					{
						StatusCode = 403,
						Content = "Acesso não autorizado"
					};
					return;
				}
				var plainTextBytes2 = System.Text.Encoding.UTF8.GetBytes("Fabio|123456");
				var te = System.Convert.ToBase64String(plainTextBytes2);
				*/
				var base64EncodedBytes = System.Convert.FromBase64String(extractedApiKey);
				var conta = System.Text.Encoding.UTF8.GetString(base64EncodedBytes);

				var lstUsuario = conta.Split("|");
				string mensagem = "";

				Seguranca seg = new Seguranca(ambiente);
				if (!seg.IsAuthenticated(lstUsuario[0], lstUsuario[1], out mensagem))
				{
					context.Result = new ContentResult()
					{
						StatusCode = 403,
						Content = mensagem
					};
					return;
				}
		//	}

			await next();
		}
	}
}
