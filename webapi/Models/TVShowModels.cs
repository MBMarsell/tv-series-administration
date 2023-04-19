namespace webapi.Models
{
    public class TVShowModel
    {
        public string ShowName { get; set; }
        public string Summary { get; set; }
        public double Rating { get; set; }
        public string Network { get; set; }
        public ICollection<string> Genres { get; set; }
        public int episodeCount { get; set; }
        public int episodesReleasedCount { get; set; }
    }

    public class TVShowNetwork
    {
        public double AvgRating { get; set; }
        public string NetworkName { get; set; }
        public string ShowName { get; set; }
        public string ShowCount { get; set; }
    }

    public class TVShowEpisode
    {
        public string ShowName { get; set; }
        public int Season { get; set; }
        public int Number { get; set; }
        public DateTime Airdate { get; set; }
        public string imageUrl { get; set; }
    }
}
