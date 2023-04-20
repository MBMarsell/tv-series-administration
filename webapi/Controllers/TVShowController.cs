using Microsoft.AspNetCore.Mvc;
using webapi.Models;
using webapi.Services;

namespace webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TVShowsController : ControllerBase
    {
        private readonly TVShowServices _tvShowServices;

        public TVShowsController(TVShowServices tvShowServices)
        {
            _tvShowServices = tvShowServices;
        }

        // api/TVShows
        [HttpGet]
        public ActionResult<List<TVShowModel>> GetAllTVShows()
        {
            return Ok(_tvShowServices.GetAllTVShows());
            
        }

        //api/TVShows/Episode
       [HttpGet("Episodes")]
        public ActionResult<List<TVShowEpisode>> GetEpisodes()
        {
            return _tvShowServices.GetEpisodes();
        }

        //api/TVShows/Show
        [HttpGet("Show")]
        public ActionResult<TVShow> GetTVShow(string showTitle)
        {
            if(string.IsNullOrEmpty(showTitle))
            {
                return BadRequest("Show title cannot be null or empty");
            }

            var tvShow = _tvShowServices.GetTVShow(showTitle);

            if(tvShow == null)
            {
                return NotFound("Tv show not found");
            }

            return Ok(tvShow);
        }


    }
}
