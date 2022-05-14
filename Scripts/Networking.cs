using System.IO;
using System.Net;
using System.Text.Json;

namespace SymmOSCFuncs
{
	class Networking
	{
		public static string APIRequestFromURL(string url)
		{
			// Make an HTTP web request
			HttpWebRequest httpRequest = (HttpWebRequest)WebRequest.Create(url);

			// Only accept "application/json" type header
			httpRequest.Accept = "application/json";

			// Typecast to a Web response
			HttpWebResponse httpResponse = (HttpWebResponse)httpRequest.GetResponse();
			string result = "";
			using (StreamReader streamReader = new StreamReader(httpResponse.GetResponseStream()))
			{
				result = streamReader.ReadToEnd();
			}
			return result;
		}

		public static WeatherAPIAstronomyJson.Root RequestAstroAPI(string url)
		{
			string urlResult = APIRequestFromURL(url);
			WeatherAPIAstronomyJson.Root jsonResult = JsonSerializer.Deserialize<WeatherAPIAstronomyJson.Root>(urlResult);
			return jsonResult;
		}
	}
}
