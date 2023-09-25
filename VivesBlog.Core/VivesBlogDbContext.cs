using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using VivesBlog.Model;

namespace VivesBlog.Core
{
    public class VivesBlogDbContext: IdentityDbContext
    {
        public VivesBlogDbContext(DbContextOptions<VivesBlogDbContext> options): base(options)
        {
            
        }

        public DbSet<Article> Articles => Set<Article>();
        public DbSet<Person> People => Set<Person>();
        
        public void Seed()
        {
            AddDefaultRoles();
            AddDefaultUser();

            var bavoPerson = new Person
            {
                FirstName = "Bavo",
                LastName = "Ketels",
                Email = "bavo.ketels@vives.be"
            };
            People.AddRange(new List<Person>
            {
                bavoPerson,
                new Person{FirstName = "Isabelle", LastName = "Vandoorne", Email = "isabelle.vandoorne@vives.be" },
                new Person
                {
                    FirstName = "Wim",
                    LastName = "Engelen",
                    Email = "wim.engelen@vives.be"
                },
                new Person{FirstName = "Ebe", LastName = "Deketelaere", Email = "ebe.deketelaere@vives.be" }
            });

            for (int i = 1; i <= 10; i++)
            {
                Articles.Add(new Article
                {
                    Id = i,
                    Title = $"Article title {i}",
                    Description = $"This is about article {i}",
                    Content = $"The full content of article {i}",
                    CreatedDate = DateTime.UtcNow.AddHours(-i),
                    Author = bavoPerson
                });
            }

            

            SaveChanges();
        }
        private void AddDefaultUser()
        {
            string email = "jordy.vandemoortele@outlook.com";
            string email2 = "jordy.normal@outlook.com";
            var ManagerRole = Roles.SingleOrDefault(r => r.Name == "Manager");

            IdentityUser defaultUser = new IdentityUser
            {
                AccessFailedCount = 0,
                EmailConfirmed = false,
                LockoutEnabled = false,
                PhoneNumberConfirmed = false,
                TwoFactorEnabled = false,
                UserName = email,
                Email = email,
                NormalizedEmail = email.ToUpper(),
                SecurityStamp = Guid.NewGuid().ToString(),
                NormalizedUserName = email.ToUpper(),
                PasswordHash = "AQAAAAIAAYagAAAAEHP2gmzTGx5N1QXzEsWy6MWuazVfSAjqP31a5gczgjHY27MzhGzGI5WLs9TclbBx3g=="
            };
            IdentityUser normalUser = new IdentityUser
            {
                AccessFailedCount = 0,
                EmailConfirmed = false,
                LockoutEnabled = false,
                PhoneNumberConfirmed = false,
                TwoFactorEnabled = false,
                UserName = email2,
                Email = email2,
                NormalizedEmail = email2.ToUpper(),
                SecurityStamp = Guid.NewGuid().ToString(),
                NormalizedUserName = email2.ToUpper(),
                PasswordHash = "AQAAAAIAAYagAAAAEHP2gmzTGx5N1QXzEsWy6MWuazVfSAjqP31a5gczgjHY27MzhGzGI5WLs9TclbBx3g=="
            };

            Users.Add(defaultUser);
            Users.Add(normalUser);
            SaveChanges();
            UserRoles.Add(new IdentityUserRole<string>()
            {
                RoleId = ManagerRole.Id,
                UserId = defaultUser.Id
            });
            SaveChanges();
        }
        private void AddDefaultRoles()
        {
            Roles.Add(new IdentityRole("Administrator"));
            Roles.Add(new IdentityRole("Manager"));

            SaveChanges();
        }
    }
}
