namespace Articles.Data.Data.Migrations
{
    using Common;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using System.Data.Entity.Migrations;
    using System.Linq;

    public sealed class Configuration : DbMigrationsConfiguration<ArticleDbContext>
    {
        private UserManager<User> userManager;
        private IRandomGenerator random;

        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;
            this.random = new RandomGenerator();
        }

        protected override void Seed(ArticleDbContext context)
        {
            this.userManager = new UserManager<User>(new UserStore<User>(context));
            this.SeedRoles(context);
            this.SeedUsers(context);
            this.SeedProperties(context);
        }

        private void SeedRoles(ArticleDbContext context)
        {
            context.Roles.AddOrUpdate(x => x.Name, new IdentityRole("Admin"));
            context.SaveChanges();
        }

        private void SeedUsers(ArticleDbContext context)
        {
            if (context.Users.Any())
            {
                return;
            }

            var adminUser = new User
            {
                Email = "admin@articles.com",
                UserName = "FirstAdmin"
            };

            var secondAdmin = new User
            {
                Email = "admin@articles1.com",
                UserName = "SecondAdmin"
            };

            this.userManager.Create(adminUser, "123456");
            this.userManager.Create(secondAdmin, "123456");

            this.userManager.AddToRole(adminUser.Id, "Admin");
            this.userManager.AddToRole(secondAdmin.Id, "Admin");
        }

        private void SeedProperties(ArticleDbContext context)
        {
            if (context.Articles.Any())
            {
                return;
            }
            
            var users = context.Users.ToList();
            for (int i = 0; i < 20; i++)
            {
                var article = new Article
                {
                    Title = this.random.RandomString(20, 50),
                    Content = this.random.RandomString(500, 1500),
                    Author = users[this.random.RandomNumber(0, users.Count - 1)]
                };

                context.Articles.Add(article);
                context.SaveChanges();
            }
        }
    }
}
