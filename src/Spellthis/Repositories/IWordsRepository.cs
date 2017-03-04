using Spellthis.Models;
using System.Collections.Generic;
using System;

namespace Spellthis.Repositories
{
    public interface IWordsRepository
    {

        List<Word> GetAll();

    }

    public class WordsRepository : IWordsRepository
    {
        public List<Word> GetAll()
        {
            return new List<Word>()
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
        }
    }
}