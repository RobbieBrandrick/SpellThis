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
                    Name = "Curmudgeon",
                    AddDate = DateTime.Now,
                    AudioFileLocation = @"F:\dev\SpellThis\src\Spellthis\wwwroot\Words\Curmudgeon.mp3",
                    AudioFileWebUri = "Words/Curmudgeon",
                },
                new Word
                {
                    Name = "Anaphylaxis",
                    AddDate = DateTime.Now,
                    AudioFileLocation = @"F:\dev\SpellThis\src\Spellthis\wwwroot\Words\Anaphylaxis.mp3",
                    AudioFileWebUri = "Words/Anaphylaxis",
                },
                new Word
                {
                    Name = "Meteorological",
                    AddDate = DateTime.Now,
                    AudioFileLocation = @"F:\dev\SpellThis\src\Spellthis\wwwroot\Words\Meteorological.mp3",
                    AudioFileWebUri = "Words/Meteorological",
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
