using System.Text.Json.Serialization;

namespace TestApp.Models
{
    public class PublicHolidayV3Dto
    {
        [JsonPropertyName("date")]
        public DateTime Date { get; set; }

        [JsonPropertyName("localName")]
        public string LocalName { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("countryCode")]
        public string CountryCode { get; set; }

        [JsonPropertyName("fixed")]
        public bool Fixed { get; set; }

        [JsonPropertyName("global")]
        public bool Global { get; set; }

        [JsonPropertyName("counties")]
        public List<string> Counties { get; set; }

        [JsonPropertyName("launchYear")]
        public int? LaunchYear { get; set; }

        [JsonPropertyName("type")]
        public List<PublicHolidayType> Type { get; set; }
    }
}