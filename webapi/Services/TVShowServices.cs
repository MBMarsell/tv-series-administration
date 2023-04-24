using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
using webapi.Models;

namespace webapi.Services
{
    public class TVShowServices
    {
        static readonly HttpClient client = new HttpClient();

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
