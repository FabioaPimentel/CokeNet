using Entities;
using System.Text;

namespace wsCokeNetDMZ.Services
{
	/// <summary>
	/// 
	/// </summary>
	public class Seguranca
	{

		static HttpClient client;
		/// <summary>
		/// 

		public string ambiente;

		/// <summary>
		/// 
		/// </summary>
		public Seguranca(string _ambiente)
		{
			ambiente = _ambiente;
		}

		/// <summary>
		/// Autenticação no AD
		/// </summary>
		/// <remarks>api/AD/IsAuthenticated Ex.:api/AD/IsAuthenticated</remarks>
		/// <param name="usuario">usuário</param>
		/// <param name="senha">senha</param>
		/// <param name="mensagem"></param>
		/// <returns></returns>
		public bool IsAuthenticated(string usuario, string senha, out string mensagem)
		{
			mensagem = "";
			try
			{
				var client = new HttpClient();
				if (ambiente.ToUpper().Equals("DEV"))											  
					client.BaseAddress = new Uri("https://wsacessoaddev.solarbr.com.br/");
				else client.BaseAddress = new Uri("https://wsacessoad.solarbr.com.br/");

				client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

				var entrada = "{\"usuario\": \"" + usuario + "\",\"senha\": \"" + senha + "\",\"grupo\": \"G_API_WSCOKENET\"}";
				//chamando a api pela url

				var contentString = new StringContent(entrada, Encoding.UTF8, "application/json");
				System.Net.Http.HttpResponseMessage response = client.PostAsync("IsAuthenticated", contentString).Result;

				MensagemRetorno retorno = response.Content.ReadFromJsonAsync<MensagemRetorno>().Result;
				mensagem = retorno.Mensagem;
				return retorno.Sucesso;
			}
			catch (Exception ex)
			{
				mensagem = "Erro:" + ex.Message;
				return false;
			}

		}

	}
}