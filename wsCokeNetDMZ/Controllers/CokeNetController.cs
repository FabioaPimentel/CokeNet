using Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Net.Http.Headers;
using System.Text;

namespace wsCokeNetDMZ.Controllers
{
    [Route("api/CokeNet")]
    [ApiController]
    public class CokeNetController : ControllerBase
    {

        HttpClient client;
		public  IConfiguration _configuration;

		/// <summary>
		/// 
		/// </summary>
		public CokeNetController(IConfiguration configuration)
        {
            if (client == null)
            {
				_configuration = configuration;
				client = new HttpClient();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var ambiente = "DEV";// ConfigurationManager.AppSettings.GetValues("AppAmbiente").GetValue(0).ToString();
                if (ambiente.ToUpper().Equals("DEV"))
                    client.BaseAddress = new Uri("https://wscokenetdev.solarbr.com.br");
                else client.BaseAddress = new Uri("https://wscokenet.solarbr.com.br");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        [HttpPost("~/OrdemVenda")]
		[ApiKey]
		public string OrdemVenda(OrdemVenda json)
        {
            try
            {
                var entrada = Util.Serializar<OrdemVenda>(json);
                var contentString = new StringContent(entrada, Encoding.UTF8, "application/json");
                contentString.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                HttpResponseMessage response = client.PostAsync("/api/CokeNet/OrdemVenda", contentString).Result;
                DataTable dt = new DataTable();
                string retorno = response.ReasonPhrase;
                if (response.IsSuccessStatusCode)
                {
                    if (!response.Content.Equals("null"))
                    {
                        return response.Content.ToString();
                    }
                }
                return response.RequestMessage.Content.ToString();
            }
            catch (Exception ex)
            {
                return "Erro:" + ex.Message;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        [HttpPost("~/OrdemVendaCancelamento")]
		[ApiKey]
		public string OrdemVendaCancelamento(OrdemVendaCancelamento json)
        {
            try
            {
                var entrada = Util.Serializar<OrdemVendaCancelamento>(json);
                var contentString = new StringContent(entrada, Encoding.UTF8, "application/json");
                contentString.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                HttpResponseMessage response = client.PostAsync("/api/CokeNet/OrdemVendaCancelamento", contentString).Result;
                DataTable dt = new DataTable();
                string retorno = response.ReasonPhrase;
                if (response.IsSuccessStatusCode)
                {
                    if (!response.Content.Equals("null"))
                    {
                        return response.Content.ToString();
                    }
                }
                return response.RequestMessage.Content.ToString();
            }
            catch (Exception ex)
            {
                return "Erro:" + ex.Message;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        [HttpGet]
		[ApiKey]
		public string Teste()
        {
            return "OK";
        }

       
    }
}