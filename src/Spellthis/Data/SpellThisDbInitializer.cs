using Spellthis.Models;
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

            if (context.Words.Any())
            {
                return;
            }
            
            var words = new List<Word>()
            {
                new Word
                {
                    Id = 1,
                    Name = "Curmudgeon",
                    AddDate = DateTime.Now
                },
                new Word
                {
                    Id = 2,
                    Name = "Anaphylaxis",
                    AddDate = DateTime.Now
                },
                new Word
                {
                    Id = 3,
                    Name = "Meteorological",
                    AddDate = DateTime.Now
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
