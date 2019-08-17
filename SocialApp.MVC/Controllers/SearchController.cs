using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SocialApp.MVC.Contracts;
using SocialApp.MVC.Models;

namespace SocialApp.MVC.Controllers
{
    [Authorize]
    public class SearchController : Controller
    {
        private readonly ISearchService searchService;
        private readonly IConfiguration configuration;

        public SearchController(ISearchService searchService, IConfiguration configuration)
        {
            this.searchService = searchService;
            this.configuration = configuration;
        }

        [HttpGet("search")]
        public ActionResult Index(SearchViewModel viewModel)
        {
            return View(viewModel);
        }

        [HttpPost("search")]
        public ActionResult Index([FromForm] SearchModel searchModel)
        {
            return View("Index", searchService.Search(searchModel));
        }

        [HttpGet("search/generate")]
        public ActionResult GenerateSearch()
        {
            searchService.GenerateSearch();
            return Ok("Your search service is already to use.");
        }
    }
}
