namespace Articles.Web.Models
{
    using Infrastructure.Mapping;
    using System.ComponentModel.DataAnnotations;
    using System.Web;
    using Articles.Data.Models;

    public class CreateArticleViewModel : IMapFrom<Article>
    {
        public int Id { get; set; }

        [MaxLength(200)]
        public string Title { get; set; }

        [MaxLength(10000)]
        public string Content { get; set; }

        public HttpPostedFileBase ImageUpload { get; set; }
    }
}