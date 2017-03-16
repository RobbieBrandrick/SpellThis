using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Spellthis.Models;
using Spellthis.Models.Account;

namespace Spellthis.Data
{
    public class SpellThisContext : IdentityDbContext<ApplicationUser>
    {

        public SpellThisContext(DbContextOptions<SpellThisContext> options) : base(options)
        {

        }

        public DbSet<Word> Words { get; set; }

    }
}
