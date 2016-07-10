namespace Articles.Web.Models
{
    using Articles.Data.Models;
    using Articles.Web.Infrastructure.Mapping;
    using System;

    public class AdministrationViewModel : IMapFrom<Article>
    {
        public int Id { get; set; }
        
        public string Title { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public User Author { get; set; }
    }
}