namespace Articles.Web.Controllers
{
    using Data.Common.Repositories;
    using System.Web.Mvc;
    using Articles.Data.Models;
    using System.Web;
    using Models;
    using Microsoft.AspNet.Identity;
    using System.Collections.Generic;
    using System.Linq;
    using Infrastructure.Mapping;
    using System;

    
    public class AdminController : Controller
    {
        private IRepository<Article> articles;

        public AdminController(IRepository<Article> articles)
        {
            this.articles = articles;
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            var allArticles = this.articles
                .All()
                .OrderByDescending(x => x.CreatedOn)
                .To<AdministrationViewModel>()
                .ToList();

            return View(allArticles);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            var article = new CreateArticleViewModel
            {
            };

            return View(article);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Create(CreateArticleViewModel model, HttpPostedFileBase upload)
        {
            if (model != null && this.ModelState.IsValid)
            {
                var currentUser = this.User.Identity.GetUserId();
                var article = new Article
                {
                    AuthorId = currentUser,
                    Title = model.Title,
                    Content = model.Content
                };

                if (upload != null && upload.ContentLength > 0)
                {
                    var avatar = new File
                    {
                        FileName = System.IO.Path.GetFileName(upload.FileName),
                        FileType = FileType.Avatar,
                        ContentType = upload.ContentType
                    };
                    using (var reader = new System.IO.BinaryReader(upload.InputStream))
                    {
                        avatar.Content = reader.ReadBytes(upload.ContentLength);
                    }
                    article.Images = new List<File> { avatar };
                }

                articles.Add(article);
                articles.SaveChanges();

                return RedirectToAction("Index", "Home");

            }

            return View(model);
        }


        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id)
        {
            var currentArticle = this.articles.GetById(id);

            if (currentArticle == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(currentArticle);
            }
        }


        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(Article model, HttpPostedFileBase upload)
        {
            if (model != null && ModelState.IsValid)
            {
                var newArticle = new Article()
                {
                    AuthorId = this.User.Identity.GetUserId(),
                    Content = model.Content,
                    Title = model.Title,
                    CreatedOn = model.CreatedOn
                };
                
                if (upload != null && upload.ContentLength > 0)
                {
                    var avatar = new File
                    {
                        FileName = System.IO.Path.GetFileName(upload.FileName),
                        FileType = FileType.Avatar,
                        ContentType = upload.ContentType
                    };
                    using (var reader = new System.IO.BinaryReader(upload.InputStream))
                    {
                        avatar.Content = reader.ReadBytes(upload.ContentLength);
                    }
                    newArticle.Images = new List<File> { avatar };
                }
                
                this.articles.Add(newArticle);
                var articleForDelete = this.articles.All().Where(x => x.Id == model.Id).FirstOrDefault();
                this.articles.Delete(articleForDelete);
                this.articles.SaveChanges();

                return RedirectToAction("Index", "Admin");
            }

            return View(model);
        }

        public ActionResult Details(int id)
        {
            var article = articles
               .All()
               .Where(ar => ar.Id == id)
               .To<DetailsViewModel>()
               .FirstOrDefault();

            return View(article);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            var article = this.articles.GetById(id);

            if (article == null)
            {
                return this.HttpNotFound("There is no article with such ID!");
            }
            else
            {
                articles.Delete(article);
                articles.SaveChanges();

                return RedirectToAction("Index", "Admin");
            }
        }
    }
}