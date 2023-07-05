using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using System;
using System.Web.Http;
using Negocio;
using Entities;

namespace wsCokeNet.Controllers
{
	/// <summary>
	/// 
	/// </summary>
	[ApiController]
	[Route("api/CokeNet")]
	public class CokeNetController : ControllerBase
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="json"></param>
		/// <returns></returns>
		[System.Web.Http.HttpPost]
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
		[System.Web.Http.HttpPost]
		public string OrdemVendaCancelamento(OrdemVendaCancelamento json)
		{
			Negocio.SAP neg = new Negocio.SAP();
			return neg.OrdemVendaCancelamento(json);
		}
	}
}
