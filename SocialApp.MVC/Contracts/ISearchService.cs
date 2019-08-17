using SocialApp.MVC.Models;

namespace SocialApp.MVC.Contracts
{
    public interface ISearchService
    {
        SearchViewModel Search(SearchModel searchModel);
        void GenerateSearch();
    }
}
