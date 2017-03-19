using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Spellthis.Models;
using Spellthis.Models.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Spellthis.Data
{
    public class SpellThisDbInitializer
    {

        public static void Initialize(SpellThisContext context)
        {

            context.Database.EnsureCreated();

            if (!context.Words.Any())
            {
                SeedWords(context);
            }

            if (!context.UserRoles.Any())
            {
                SeedRoles(context);
            }

            if (!context.Users.Any())
            {
                SeedUsers(context);
            }

        }

        private static void SeedUsers(SpellThisContext context)
        {
            var admin = new ApplicationUser
            {
                UserName = "admin@spellthis.com",
                NormalizedUserName = "admin@spellthis.com",
                Email = "admin@spellthis.com",
                NormalizedEmail = "admin@spellthis.com",
                EmailConfirmed = true,
                LockoutEnabled = false,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            var password = new PasswordHasher<ApplicationUser>();
            var hashed = password.HashPassword(admin, "password");
            admin.PasswordHash = hashed;
            var userStore = new UserStore<ApplicationUser>(context);
            userStore.CreateAsync(admin).Wait();
            userStore.AddToRoleAsync(admin, "admin").Wait();
        }

        private static void SeedRoles(SpellThisContext context)
        {

            var roleStore = new RoleStore<IdentityRole>(context);

            if (!context.Roles.Any(r => r.Name == "admin"))
            {

                roleStore.CreateAsync(new IdentityRole { Name = "admin", NormalizedName = "admin" }).Wait();

            }

        }

        private static void SeedWords(SpellThisContext context)
        {
            var words = new List<Word>()
            {
                new Word
                {
                    Name = "Curmudgeon",
                    AddDate = DateTime.Now,
                    AudioFileLocation = @"F:\dev\SpellThis\src\Spellthis\wwwroot\Words\Curmudgeon.mp3",
                    AudioFileWebUri = "Words/Curmudgeon.mp3",
                },
                new Word
                {
                    Name = "Anaphylaxis",
                    AddDate = DateTime.Now,
                    AudioFileLocation = @"F:\dev\SpellThis\src\Spellthis\wwwroot\Words\Anaphylaxis.mp3",
                    AudioFileWebUri = "Words/Anaphylaxis.mp3",
                },
                new Word
                {
                    Name = "Meteorological",
                    AddDate = DateTime.Now,
                    AudioFileLocation = @"F:\dev\SpellThis\src\Spellthis\wwwroot\Words\Meteorological.mp3",
                    AudioFileWebUri = "Words/Meteorological.mp3",
                },
            };

            foreach (var word in words)
            {
                context.Words.Add(word);
            }

            context.SaveChanges();
        }
    }
}
