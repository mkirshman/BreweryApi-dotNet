using BreweryService.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Net;
using System.Threading.Tasks;
using System.Web;

namespace BreweryService.Services
{
    public class BreweryService
    {
        private readonly string _apiBaseUrl;
        private readonly HttpClient _httpClient;

        public BreweryService(string apiBaseUrl)
        {
            _apiBaseUrl = apiBaseUrl;
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<Brewery> GetBreweryByIdAsync(string obdbId)
        {
            try
            {
                var response = await _httpClient.GetAsync(BuildApiUri($"/v1/breweries/{obdbId}"));

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<Brewery>(content);
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions (e.g., network issues, API errors)
                Console.WriteLine(ex); // Replace with proper logging
            }

            return null;
        }

        public async Task<List<Brewery>> GetAllBreweriesAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync(BuildApiUri("/v1/breweries"));

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<Brewery>>(content);
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions (e.g., network issues, API errors)
                Console.WriteLine(ex); // Replace with proper logging
            }

            return new List<Brewery>();
        }

        public async Task<List<Brewery>> GetBreweriesByCityAsync(string city)
        {
            try
            {
                var response = await _httpClient.GetAsync(BuildApiUri($"/v1/breweries?by_city={city}"));

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<Brewery>>(content);
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions (e.g., network issues, API errors)
                Console.WriteLine(ex); // Replace with proper logging
            }

            return new List<Brewery>();
        }

        // Implement similar methods for other query types

        private string BuildApiUri(string path)
        {
            var uriBuilder = new UriBuilder(new Uri(_apiBaseUrl));
            uriBuilder.Path = path;

            return uriBuilder.ToString();
        }
    }
}