using Spellthis.Models;
using System;
using Spellthis.Data;
using System.Linq;

namespace Spellthis.Repositories
{
    public interface IWordsRepository
    {

        /// <summary>
        /// Retrieves all the words
        /// </summary>
        /// <returns>All the words</returns>
        IQueryable<Word> GetAll();

        /// <summary>
        /// Adds a word to the repository
        /// </summary>
        /// <param name="word">Word to add to the repository</param>     
        void Add(Word word);
    }

    public class WordsRepository : IWordsRepository
    {
        
        private SpellThisContext _dbContext;

        /// <summary>
        /// Sets up classes dependencies
        /// </summary>
        public WordsRepository(SpellThisContext dbContext)
        {

            if (dbContext == null)
                throw new InvalidOperationException("dbContext cannot be null");

            _dbContext = dbContext;

        }

        /// <summary>
        /// Adds a word to the repository
        /// </summary>
        /// <param name="word">Word to add to the repository</param>
        public void Add(Word word)
        {

            _dbContext.Words.Add(word);
            _dbContext.SaveChanges();

        }

        /// <summary>
        /// Retrieves all the words
        /// </summary>
        /// <returns>All the words</returns>
        public IQueryable<Word> GetAll()
        {

            return _dbContext.Words;

        }
    }

}