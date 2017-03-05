using Microsoft.EntityFrameworkCore;
using Spellthis.Models;
namespace Spellthis.Data
{
    public class SpellThisContext : DbContext
    {

        public SpellThisContext(DbContextOptions<SpellThisContext> options) : base(options)
        {

        }

        public DbSet<Word> Words { get; set; }

    }
}
