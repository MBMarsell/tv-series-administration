using Microsoft.AspNetCore.Mvc;
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
        public ActionResult GetAllTVShows()
        {
            return Ok(_tvShowServices.GetAllTVShows());
        }


    }
}
