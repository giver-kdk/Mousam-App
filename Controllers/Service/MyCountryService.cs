using Mousam_App.Models;
using System.Text.Json;

namespace Mousam_App.Controllers.Service
{
	public class MyCountryService
	{
		private HttpClient _httpClient;
		public MyCountryService(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}

		public async Task<CountryModel?> GetAsyncCountry(string countryCode = "NP")
		{
			string endpoint = "/tools/api/";
			string parameters = $"?format=json&resource=convert-country-code-to-country-name&country_code={countryCode}";
			string requestURI = endpoint + parameters;
			try
			{
				// Get API resposne in string format. Default response is HTML with JSON
				var htmlResponse = await _httpClient.GetStringAsync(requestURI);
				// Filter JSON part of the API response
				var jsonResponse = htmlResponse.Split('\n').Last();
				// Map JSON data to variable with respective to model
				var result = JsonSerializer.Deserialize<CountryModel>(jsonResponse);
				Console.WriteLine(result.ToString());
				return result;
			}
			catch (Exception ex)
			{
				Console.WriteLine("ERROR!!!!!!!!!!!*****************" + ex.Message + "**************************");	
				return null;
			}
		}
	}
}
