using BusinessLogicalLayer.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Shared.Models;
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

        [HttpGet("GetByName")]
        public async Task<ActionResult<SingleResponse<SearchData>>> GetByName([FromQuery] string name)
        {
            var response = await _homeService.GetByName(name);
            return Ok(response);
        }

        [HttpGet("HomePage")]
        public async Task<ActionResult<SingleResponse<HomePageData>>> GetTopAnimeManga([FromQuery] int skip = 0, [FromQuery] int take = 7)
        {
            var response = await _homeService.GetTopAnimeManga(skip, take);
            return Ok(response);
        }

        [HttpGet("GetHomeMedia")]
        public async Task<ActionResult<SingleResponse<ItemPageData>>> GetHomeMedia( [FromQuery] int skip = 0,[FromQuery] int take = 7,[FromQuery] string mediaType = "anime")
        {
            // Normaliza o parâmetro
            string tableName;
            if (mediaType.Equals("anime", StringComparison.OrdinalIgnoreCase))
                tableName = "Anime";
            else if (mediaType.Equals("manga", StringComparison.OrdinalIgnoreCase))
                tableName = "Mangas";
            else
                return BadRequest("Media type inválido. Use 'anime' ou 'manga'.");

            var response = await _homeService.GetHomeMedia(skip, take, tableName);
            return Ok(response);
        }

    }
}
