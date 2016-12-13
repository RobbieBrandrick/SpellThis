using SpellThis.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SpellThis.Repositories
{
    public interface IWordRepository
    {

        /// <summary>
        /// Get all the words
        /// </summary>
        /// <returns>All the words</returns>
        IEnumerable<Word> GetAll();

    }

    public class WordRepository : IWordRepository
    {

        /// <summary>
        /// Get all the words
        /// </summary>
        /// <returns>All the words</returns>
        public IEnumerable<Word> GetAll()
        {

            return new List<Word>()
            {
                new Word
                {
                    Name = "Hepatitis",
                    LastAttempted = null,
                    NextAttempt = null
                },
                new Word
                {
                    Name = "Superfluous",
                    LastAttempted = new DateTime(2016, 01, 01),
                    NextAttempt = DateTime.Now
                },
                new Word
                {
                    Name = "Superficial",
                    LastAttempted = new DateTime(2016, 01, 01),
                    NextAttempt = DateTime.Now.AddDays(1)
                },
            }.AsEnumerable();

        }

    }
}
