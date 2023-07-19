using System.Runtime.Serialization.Json;
using System.Text;

namespace wsCokeNetDMZ
{
	public class Util
	{
		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="obj"></param>
		/// <returns></returns>
		public static string Serializar<T>(T obj)
		{
			DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(T));
			MemoryStream ms = new MemoryStream();
			serializer.WriteObject(ms, obj);
			return Encoding.UTF8.GetString(ms.ToArray());
		}
	}
}