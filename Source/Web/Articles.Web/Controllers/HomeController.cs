namespace Articles.Web.Controllers
{
    using Articles.Data.Models;
    using Data.Common.Repositories;
    using System.Web.Mvc;
    using System.Linq;
    using Infrastructure.Mapping;
    using Models;

    public class HomeController : Controller
    {
        private IRepository<Article> articles;

        public HomeController(IRepository<Article> articles)
        {
            this.articles = articles;
        }

        public ActionResult Index()
        {
            var allArticles = this.articles
                .All()
                .OrderByDescending(a => a.CreatedOn)
                .Take(20)
                .To<ArticleViewModel>()
                .ToList();

            return View(allArticles);
        }
    }
}