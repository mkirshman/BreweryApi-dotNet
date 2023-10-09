using BreweryService.Models;
using Microsoft.AspNetCore.Mvc;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BreweryService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BreweryController : ControllerBase
    {
        private static readonly string API_URL = "https://api.openbrewerydb.org/v1/breweries";
        private readonly IRestClient _restClient;

        public BreweryController()
        {
            _restClient = new RestClient();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Brewery>>> GetBreweries(
            [FromQuery(Name = "state")] string state,
            [FromQuery(Name = "zipcode")] string zip,
            [FromQuery(Name = "type")] string type)
        {
            string apiUrl = API_URL;

            if (!string.IsNullOrEmpty(state))
            {
                apiUrl = AppendParameter(apiUrl, "by_state=" + state);
            }
            if (!string.IsNullOrEmpty(zip))
            {
                apiUrl = AppendParameter(apiUrl, "by_postal=" + zip);
            }
            if (!string.IsNullOrEmpty(type))
            {
                apiUrl = AppendParameter(apiUrl, "by_type=" + type);
            }

            var request = new RestRequest(apiUrl, Method.Get);

            var response = await _restClient.ExecuteAsync<List<Brewery>>(request);

            if (response.IsSuccessful)
            {
                var breweries = response.Data;
                return Ok(breweries);
            }
            else
            {
                return StatusCode((int)response.StatusCode, response.ErrorMessage);
            }
        }

        private string AppendParameter(string apiUrl, string parameter)
        {
            if (apiUrl.Contains("?"))
            {
                return apiUrl + "&" + parameter;
            }
            else
            {
                return apiUrl + "?" + parameter;
            }
        }
    }
}
