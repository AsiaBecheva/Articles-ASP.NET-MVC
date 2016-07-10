namespace Articles.Data.Data
{
    using Articles.Data.Models;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Migrations;
    using System.Data.Entity;

    public class ArticleDbContext : IdentityDbContext<User>
    {
        public ArticleDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ArticleDbContext, Configuration>());
        }

        public virtual IDbSet<Article> Articles { get; set; }

        public virtual IDbSet<File> Files { get; set; }

        public static ArticleDbContext Create()
        {
            return new ArticleDbContext();
        }
    }
}
