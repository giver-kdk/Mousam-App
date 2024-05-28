using Microsoft.AspNetCore.Mvc;
using Mousam_App.Controllers.Service;
using Mousam_App.Models;
using System;


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
        public async Task<IActionResult> Index()
        {
			// ****** Await for asynchronous operation ***********
			var weatherResult = await _jsonService.GetAsyncWeather();
			var countryResult = await _myCountryService.GetAsyncCountry();
            CombinedModel combinedModel = new CombinedModel();
            combinedModel.JsonModelData = weatherResult;
            combinedModel.CountryModelData = countryResult;
            return View(combinedModel);
        }
        [HttpPost("/")]
        public async Task<IActionResult> SetCity(IFormCollection collections)
        {
            var cityName = Convert.ToString(collections["city"]);
            var weatherResult = await _jsonService.GetAsyncWeather(cityName);
			var countryResult = await _myCountryService.GetAsyncCountry(weatherResult?.city?.country ?? "NP");
			CombinedModel combinedModel = new CombinedModel();
			combinedModel.JsonModelData = weatherResult;
			combinedModel.CountryModelData = countryResult;
			return View("Index", combinedModel);
        }

    }
}
