using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
using webapi.Models;

namespace webapi.Services
{
    public class TVShowServices
    {
        private readonly List<TVShowModel> _tvShows;
        private readonly List<TVShowEpisode> _episodes;
        static readonly HttpClient client = new HttpClient();

        public TVShowServices()
        {
            _tvShows = new List<TVShowModel>();
            _episodes = new List<TVShowEpisode>();

        }

        public List<TVShowModel> Get()
        {
            return _tvShows.ToList();
        }

        public List<TVShowModel> GetAllTVShows()
        {
            var sortedRatingList = _tvShows.OrderByDescending(show => show.Rating).ToList();

            var startDate = new DateTime(2023, 04, 20);

            List<TVShowModel> rankedTVShowsList = new List<TVShowModel>();
            foreach (var item in sortedRatingList)
            {
                TVShowModel tvshow = new TVShowModel
                {
                    ShowName = item.ShowName,
                    Summary = item.Summary,
                    Rating = item.Rating,
                    Network = item.Network,
                    Genres = item.Genres,
                    EpisodeCount = item.EpisodeCount,
                    EpisodesReleasedCount = item.EpisodesReleasedCount,
                    ImageUrl = item.ImageUrl,
                };

                var releasedEpisodeCount = _episodes.Where(e => e.ShowName == item.ShowName && e.Airdate <= startDate).ToList().Count;

                tvshow.EpisodesReleasedCount = releasedEpisodeCount;

                rankedTVShowsList.Add(tvshow);
            }
            return rankedTVShowsList;
        }

        public List<TVShowEpisode> GetEpisodes()
        {
            return _episodes.ToList();
        }

        public TVShow GetTVShow(string showTitle)
        {
            string apiUrl = $"https://api.tvmaze.com/singlesearch/shows?q={showTitle}";
            try
            {
                var response = client.GetAsync(apiUrl).Result;

                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    var tvShow = JsonConvert.DeserializeObject<TVShow>(result);
                    return tvShow;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            

            

            

        }
        


    }
}
