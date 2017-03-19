using Spellthis.Models;
using System;
using Spellthis.Data;
using System.Linq;
using System.Threading.Tasks;

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
        Task Add(Word word);

        /// <summary>
        /// Associates a word to a user
        /// </summary>
        /// <param name="word">Word to associate to a user</param>
        Task AddToUser(int wordId, string userId);

        /// <summary>
        /// Removed a word from the repository
        /// </summary>
        /// <param name="word">Word to remove</param>
        /// <returns>Asynchronous Task of removing the word</returns>
        Task Remove(Word word);

        /// <summary>
        /// Deassociates a User Word within the repository
        /// </summary>
        /// <param name="wordId">Word to remove</param>
        /// <param name="userId">userId to remove</param>
        /// <returns>Asynchronous Task of removing the word</returns>
        Task RemoveUserWord(int wordId, string userId);

        /// <summary>
        /// Retrieves a users the words
        /// </summary>
        /// <returns>All the words for a user</returns>
        IQueryable<Word> GetAllForUser(string userId);
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
        public async Task Add(Word word)
        {

            _dbContext.Words.Add(word);

            await _dbContext.SaveChangesAsync();

        }

        /// <summary>
        /// Associates a word to a user
        /// </summary>
        /// <param name="word">Word to associate to a user</param>
        public async Task AddToUser(int wordId, string userId)
        {

            var word = _dbContext.Words.FirstOrDefault(w => w.Id == wordId);

            _dbContext.UserWords.Add(new UserWord() { WordId = wordId, UserId = userId });

            await _dbContext.SaveChangesAsync();

        }

        /// <summary>
        /// Retrieves all the words
        /// </summary>
        /// <returns>All the words</returns>
        public IQueryable<Word> GetAll()
        {

            return _dbContext.Words;

        }

        /// <summary>
        /// Retrieves a users the words
        /// </summary>
        /// <returns>All the words for a user</returns>
        public IQueryable<Word> GetAllForUser(string userId)
        {

            return _dbContext.Words
                .Where(w => w.UserWord.UserId.Equals(userId));

        }

        /// <summary>
        /// Removed a word from the repository
        /// </summary>
        /// <param name="word">Word to remove</param>
        /// <returns>Asynchronous Task of removing the word</returns>
        public async Task Remove(Word word)
        {

            _dbContext.Words.Remove(word);

            await _dbContext.SaveChangesAsync();

        }

        /// <summary>
        /// Deassociates a User Word within the repository
        /// </summary>
        /// <param name="wordId">Word to remove</param>
        /// <param name="userId">userId to remove</param>
        /// <returns>Asynchronous Task of removing the word</returns>
        public async Task RemoveUserWord(int wordId, string userId)
        {

            var userWord = _dbContext.UserWords
                .FirstOrDefault(uw => uw.WordId == wordId && uw.UserId == userId);

            _dbContext.UserWords.Remove(userWord);

            await _dbContext.SaveChangesAsync();

        }
    }

}