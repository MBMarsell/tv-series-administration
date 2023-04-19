using webapi.Models;

namespace webapi.Services
{
    public class TVShowServices
    {
        private readonly List<TVShowModel> _tvShows;
        private readonly List<TVShowEpisode> _episodes;
        static HttpClient client = new HttpClient();

        public TVShowServices()
        {
            _tvShows = new List<TVShowModel>();
            _episodes = new List<TVShowEpisode>();

            _tvShows.Add(new TVShowModel
            {
                ShowName = "The Office",
                Summary = "A mockumentary on a group of typical office workers, where the workday consists of ego clashes, inappropriate behavior, and tedium.",
                Rating = 8.9,
                Network = "NBC",
                Genres = new List<string> { "Comedy" },
                episodeCount = 201,
                episodesReleasedCount = 201
            });

            _episodes.Add(new TVShowEpisode
            {
ShowName = "The Office",
                Season = 1,
                Number = 1,
                Airdate = new DateTime(2005, 3, 24),
                imageUrl = "https://static.tvmaze.com/uploads/images/medium_landscape/1/4388.jpg"
            });
        }

        public List<TVShowModel> GetAllTVShows()
        {
            return _tvShows;
        }

        public TVShowModel GetTVShowByName(string showName)
        {
            return _tvShows.FirstOrDefault(t => t.ShowName == showName);
        }

        public List<TVShowEpisode> GetEpisodesForTVShow(string showName)
        {
            return _episodes.Where(e => e.ShowName == showName).ToList();
        }

        public TVShowNetwork GetNetworkDetails(string networkName)
        {
            var showsOnNetwork = _tvShows.Where(t => t.Network == networkName).ToList();
            var avgRating = showsOnNetwork.Average(t => t.Rating);
            var showCount = showsOnNetwork.Count;

            return new TVShowNetwork
            {
                NetworkName = networkName,
                AvgRating = avgRating,
                ShowCount = showCount.ToString()
            };
        }





    }
}
