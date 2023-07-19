using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTeste
{
	internal class Program
	{
		static void Main(string[] args)
		{
			Negocio.SAP neg = new Negocio.SAP();
			OrdemVenda ov = new OrdemVenda();
			ov.I_JSON = "teste";
			neg.OrdemVenda(ov);
		}
	}
}
