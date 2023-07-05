using Entities;
using SAP.Middleware.Connector;
using System;
using System.Configuration;
using System.Data;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;

namespace Negocio
{
	public class SAP
	{
		public static string ambiente = "";

		public SAP()
		{
			ambiente = ConfigurationManager.AppSettings.GetValues("AppAmbiente").GetValue(0).ToString();
		}

		#region Métodos CokeNet

		public string OrdemVenda(OrdemVenda json)
		{
			IRfcFunction fReadTable = null;
			RfcDestination dest = null;
			try
			{
				dest = GetRepository("COKENET");
				RfcSessionManager.BeginContext(dest);
				fReadTable = dest.Repository.CreateFunction("ZSD_COKE_ONLINE_OV");
				fReadTable.SetValue("I_JSON", json.I_JSON);				
				fReadTable.Invoke(dest);
				return fReadTable.GetString("RESPONSE");
			}
			catch (Exception ex)
			{
				Log("OrdemVenda - Erro:" + ex.Message + " - Trace:" + ex.StackTrace);
				throw ex;
			}
		}

		public string OrdemVendaCancelamento(OrdemVendaCancelamento json)
		{
			IRfcFunction fReadTable = null;
			RfcDestination dest = null;
			try
			{
				dest = GetRepository("COKENET");
				RfcSessionManager.BeginContext(dest);
				fReadTable = dest.Repository.CreateFunction("ZSD_COKE_ONLINE_OV_CANCEL");
				fReadTable.SetValue("I_JSON", json.I_JSON);
				fReadTable.Invoke(dest);
				return fReadTable.GetString("RESPONSE");
			}
			catch (Exception ex)
			{
				Log("OrdemVendaCancelamento - Erro:" + ex.Message + " - Trace:" + ex.StackTrace);
				throw ex;
			}
		}

		#endregion

		#region Métodos de conexão com o SAP

		public string ObterUsuarioSAP(string sistema)
		{
			if (sistema.ToUpper().Equals("COKENET"))
				return ("SRVWSLOGISTI|Snova@*0911");
			return "";

		}

		public RfcDestination GetRepository(string sistema)
		{
			string usuario = ObterUsuarioSAP(sistema);
			string[] dadosLogon = usuario.Split('|');
			try
			{
				string serverSAP = ambiente.ToUpper().Equals("PRD") ? "SOLARPRD" : "SOLARQA1";
				RfcConfigParameters parameters = new RfcConfigParameters();
				if (serverSAP.ToUpper() == "SOLARQA1")
				{
					parameters[RfcConfigParameters.Name] = "QA1";
					parameters[RfcConfigParameters.SystemID] = "QA1";
					parameters[RfcConfigParameters.PeakConnectionsLimit] = "20";
					parameters[RfcConfigParameters.PoolSize] = "10";
					parameters[RfcConfigParameters.ConnectionIdleTimeout] = "1"; // we keep connections for 1 minutes
					parameters[RfcConfigParameters.User] = dadosLogon[0];
					parameters[RfcConfigParameters.Password] = dadosLogon[1];
					parameters[RfcConfigParameters.Client] = "120";
					parameters[RfcConfigParameters.Language] = "PT";
					parameters[RfcConfigParameters.AppServerHost] = "FORSRP240.solarbr.com.br";
					parameters[RfcConfigParameters.SystemNumber] = "00";
					parameters[RfcConfigParameters.RegistrationCount] = "5";
				}
				else if (serverSAP.ToUpper() == "SOLARPRD")
				{
					parameters[RfcConfigParameters.Name] = "PRD";
					parameters[RfcConfigParameters.SystemID] = "PR1";
					parameters[RfcConfigParameters.PeakConnectionsLimit] = "20";
					parameters[RfcConfigParameters.PoolSize] = "10";
					parameters[RfcConfigParameters.ConnectionIdleTimeout] = "1"; // we keep connections for 1 minutes
					parameters[RfcConfigParameters.User] = dadosLogon[0];
					parameters[RfcConfigParameters.Password] = dadosLogon[1];
					parameters[RfcConfigParameters.Client] = "120";
					parameters[RfcConfigParameters.Language] = "PT";
					parameters[RfcConfigParameters.AppServerHost] = "FORSRP232.solarbr.com.br";
					parameters[RfcConfigParameters.SystemNumber] = "00";
					parameters[RfcConfigParameters.RegistrationCount] = "5";
				}
				return RfcDestinationManager.GetDestination(parameters);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		#endregion

		public void Log(string ex)
		{
			FileInfo arq = new FileInfo("c:\\\\Temp\\CokeNet.txt");
			if (arq.Exists && arq.Length > 10000000)
			{
				File.Move("c:\\\\Temp\\LogPedidos.txt", "c:\\\\Temp\\CokeNet" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".txt");
			}
			StreamWriter erro = new StreamWriter("c:\\\\Temp\\CokeNet.txt", true);
			erro.WriteLine("_____________________________________________________________________________________________________________");
			erro.WriteLine("Data: " + DateTime.Now.ToString("yyyy-MM-dd HH:mmTongue Tieds.fff"));
			erro.WriteLine(ex);
			erro.WriteLine("-------------------------------------------------------------------------------------------------------------");
			erro.Close();
		}

		public string Serializar<T>(T obj)
		{
			DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(T));
			MemoryStream ms = new MemoryStream();
			serializer.WriteObject(ms, obj);
			return Encoding.UTF8.GetString(ms.ToArray());
		}

		public DataTable getDataTable(IRfcTable tblRetorno)
		{
			DataTable data = new DataTable();

			for (int x = 0; x < tblRetorno.ElementCount; x++)
			{
				data.Columns.Add(tblRetorno.GetElementMetadata(x).Name.ToString().ToUpper());
			}
			for (int x = 0; x < tblRetorno.RowCount; x++)
			{
				var linha = data.NewRow();

				for (int y = 0; y < tblRetorno.ElementCount; y++)
				{
					linha[y] = tblRetorno[x][y].GetValue().ToString();
				}
				data.Rows.Add(linha);
			}
			return data;
		}
	}
}
