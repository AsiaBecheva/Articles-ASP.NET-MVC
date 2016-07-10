namespace Articles.Web.Models
{
    using Articles.Data.Models;
    using Infrastructure.Mapping;
    using System.Collections.Generic;

    public class DetailsViewModel :IMapFrom<Article>
    {
        public string Title { get; set; }

        public string Content { get; set; }

        public ICollection<File> Images { get; set; }
    }
}