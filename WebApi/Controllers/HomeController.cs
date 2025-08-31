using BusinessLogicalLayer.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Shared.DTOS;
using Shared.Responses;

namespace MvcPresentationLayer.Controllers.Api
{
    [ApiController]
    [Route("api/Home")]
    public class HomeController : ControllerBase
    {
        private readonly IHomeService _homeService;

        public HomeController(IHomeService homeService)
        {
            _homeService = homeService;
        }

        [HttpGet("HomePage")]
        public async Task<ActionResult<SingleResponse<HomePageData>>> GetTopAnimeManga([FromQuery] int skip = 0, [FromQuery] int take = 7)
        {
            var response = await _homeService.GetTopAnimeManga(skip, take);
            return Ok(response);
        }
    }
}
