using Spellthis.Models;
using System.Collections.Generic;
using System;
using Microsoft.EntityFrameworkCore;
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

        public WordsRepository(SpellThisContext dbContext)
        {

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