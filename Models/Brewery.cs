using Newtonsoft.Json;

namespace BreweryService.Models
{
    public class Brewery
    {
        public string Name { get; set; }

        [JsonProperty("address_1")]
        public string Address1 { get; set; }

        public string City { get; set; }
        public string State { get; set; }
        public string Phone { get; set; }

        [JsonProperty("brewery_type")]
        public string BreweryType { get; set; }

        [JsonProperty("postal_code")]
        public string PostalCode { get; set; }

        [JsonProperty("website_url")]
        public string WebsiteUrl { get; set; }
    }
}