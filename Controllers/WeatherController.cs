using Microsoft.AspNetCore.Mvc;
using Mousam_App.Controllers.Service;
using Mousam_App.Models;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;


namespace Mousam_App.Controllers
{
    public class WeatherController : Controller
    {
		private MyJsonService _jsonService;
		private MyCountryService _myCountryService;

        //public WeatherController() { }
        // ************ DI for HTTP Client **************
        public WeatherController(MyJsonService jsonService, MyCountryService myCountryService)
        {
            _jsonService = jsonService;
            _myCountryService = myCountryService;
		}
        // GET: api/<WeatherController>
        [HttpGet("/")]
        public async Task<IActionResult> Index(string cityName = "Kathmandu", int listIndex = 0, int timeIndex = 0, bool doReuse = false)
        {
            ViewBag.SelectedList = listIndex;
            ViewBag.SelectedTime = timeIndex;

            ViewBag.ReuseData = doReuse;
            ViewBag.ApiDataError = false;

			var combinedModel = new CombinedModel();
			//if (!HttpContext.Session.TryGetValue("updatedWeather", out byte[] value))
			if (!ViewBag.ReuseData)
            {
                try{
			        // ****** Await for asynchronous operation ***********
			        var weatherResult = await _jsonService.GetAsyncWeather(cityName);
			        var countryResult = await _myCountryService.GetAsyncCountry();

				    combinedModel.JsonModelData = weatherResult;
				    combinedModel.CountryModelData = countryResult;

                    HttpContext.Session.SetString("updatedWeather", JsonSerializer.Serialize(weatherResult));
                    HttpContext.Session.SetString("updatedCountry", JsonSerializer.Serialize(countryResult));
                }
                catch(Exception ex)
                {
                    // Show previously loaded session data if city name is not valid or API fails to fetch data
                    if(HttpContext.Session.TryGetValue("updatedWeather", out byte[] value))
                    {
						combinedModel.JsonModelData = JsonSerializer.Deserialize<JsonModel>(HttpContext.Session.GetString("updatedWeather"));
						combinedModel.CountryModelData = JsonSerializer.Deserialize<CountryModel>(HttpContext.Session.GetString("updatedCountry"));

                        ViewBag.ApiDataError = true;
					}
                    // Display error message in absence of previous data
                    else{
                        return Content("Sorry, Unable to fetch Weather Data!\n\nError Message:\n" + ex.Message);
                    }
                }
			}
            else{
                //Console.WriteLine("***************Data***************" + HttpContext.Session.GetString("updatedWeather"));
				combinedModel.JsonModelData = JsonSerializer.Deserialize<JsonModel>(HttpContext.Session.GetString("updatedWeather"));
				combinedModel.CountryModelData = JsonSerializer.Deserialize<CountryModel>(HttpContext.Session.GetString("updatedCountry"));
			}
            return View(combinedModel);
        }
		[HttpPost("/")]
        public async Task<IActionResult> SetCity(IFormCollection collections)
        {
            var city = Convert.ToString(collections["city"]);
			return RedirectToAction("Index", "Weather", new {cityName = city});
        }
    }
}
