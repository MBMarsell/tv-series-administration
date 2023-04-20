using Newtonsoft.Json;

namespace webapi.Models
{
    public class TVShowModel
    {

        public string? ShowName { get; set; }
        public string? Summary { get; set; }
        public double Rating { get; set; }
        public string? Network { get; set; }
        public ICollection<string>? Genres { get; set; }
        public int EpisodeCount { get; set; }
        public int EpisodesReleasedCount { get; set; }
        public string? ImageUrl { get; set; }
    }

    public class TVShowNetwork
    {
        public double AvgRating { get; set; }
        public string? NetworkName { get; set; }
        public string? ShowName { get; set; }
        public string? ShowCount { get; set; }
    }

    public class TVShowEpisode
    {
        public string? ShowName { get; set; }
        public int Season { get; set; }
        public int Number { get; set; }
        public DateTime Airdate { get; set; }
        public string? ImageUrl { get; set; }
    }


    public partial class Rating
    {
        [JsonProperty("average")]
        public double Average { get; set; }
    }

    public partial class Country
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("timezone")]
        public string Timezone { get; set; }
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
        public Uri OfficialSite { get; set; }
    }

    public partial class Image
    {
        [JsonProperty("medium")]
        public Uri Medium { get; set; }

        [JsonProperty("original")]
        public Uri Original { get; set; }
    }

    public class TVShow
    {
        [JsonProperty("name")]
        public string? Name { get; set; }

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
