using Entities;
using Negocio;
using System.Web.Http;

namespace wsCokeNet.Controllers
{
	
	/// <summary>
	/// 
	/// </summary>
	[RoutePrefix("api/CokeNet")]
	public class CokeNetController : ApiController
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="json"></param>
		/// <returns></returns>
		[HttpPost]
		public string OrdemVenda(OrdemVenda json)
		{
			Negocio.SAP neg = new Negocio.SAP();
			return neg.OrdemVenda(json);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="json"></param>
		/// <returns></returns>
		[HttpPost]
		public string OrdemVendaCancelamento(OrdemVendaCancelamento json)
		{
			Negocio.SAP neg = new Negocio.SAP();
			return  neg.OrdemVendaCancelamento(json);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="json"></param>
		/// <returns></returns>
		[HttpGet]
		public string Teste()
		{
			return "OK";
		}
	}
}
