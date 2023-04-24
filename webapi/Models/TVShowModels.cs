using Newtonsoft.Json;

namespace webapi.Models
{
    public partial class Rating
    {
        [JsonProperty("average")]
        public double Average { get; set; }
    }

    public partial class Country
    {
        [JsonProperty("name")]
        public string? Name { get; set; }

        [JsonProperty("code")]
        public string? Code { get; set; }

        [JsonProperty("timezone")]
        public string? Timezone { get; set; }
    }

    public partial class Network
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("country")]
        public Country Country { get; set; }

        [JsonProperty("officialSite")]
        public Uri? OfficialSite { get; set; }
    }

    public partial class Image
    {
        [JsonProperty("medium")]
        public Uri? Medium { get; set; }

        [JsonProperty("original")]
        public Uri? Original { get; set; }
    }

    public class TVShow
    {
        [JsonProperty("name")]
        public string? Name { get; set; }

        [JsonProperty("summary")]
        public string Summary { get; set; }

        [JsonProperty("genres")]
        public string[]? Genres { get; set; }

        [JsonProperty("rating")]
        public Rating? Rating { get; set; }

        [JsonProperty("network")]
        public Network? Network { get; set; }

        [JsonProperty("image")]
        public Image? Image { get; set; }
        
    }
}
