namespace Articles.Web.Models
{
    using Articles.Web.Infrastructure.Mapping;
    using Articles.Data.Models;
    using System;
    using System.Collections.Generic;

    public class ArticleViewModel : IMapFrom<Article>
    {
        public int Id { get; set; }
        
        public string Title { get; set; }
        
        public string Content { get; set; }

        public DateTime? CreatedOn { get; set; }

        public ICollection<File> Images { get; set; }
    }
}