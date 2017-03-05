using Spellthis.Models;
using System.Collections.Generic;
using System;

namespace Spellthis.Repositories
{
    public interface IWordsRepository
    {

        /// <summary>
        /// Retrieves all the words
        /// </summary>
        /// <returns>All the words</returns>
        List<Word> GetAll();

        /// <summary>
        /// Adds a word to the repository
        /// </summary>
        /// <param name="word">Word to add to the repository</param>     
        void Add(Word word);
    }

    public class WordsRepository : IWordsRepository
    {

        private List<Word> _words;

        public WordsRepository()
        {
            _words = new List<Word>()
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

        /// <summary>
        /// Adds a word to the repository
        /// </summary>
        /// <param name="word">Word to add to the repository</param>
        public void Add(Word word)
        {
            _words.Add(word);
        }

        /// <summary>
        /// Retrieves all the words
        /// </summary>
        /// <returns>All the words</returns>
        public List<Word> GetAll()
        {
            return _words;
        }
    }

}