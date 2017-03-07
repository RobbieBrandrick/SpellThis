using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Spellthis.Models;
using Spellthis.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Spellthis.Services
{

    public interface ISpellThisService
    {
        /// <summary>
        /// Get the user's spelling words
        /// </summary>
        /// <returns>User's spelling words</returns>
        Task<IEnumerable<Word>> GetSpellingWords();

        /// <summary>
        /// Adds the spelling word to the users list
        /// </summary>
        /// <param name="word">Word to add</param>
        Word AddSpellingWord(string word);
    }

    public class SpellThisService : ISpellThisService
    {

        private IWordsRepository _wordsRepository;
        private ITextToSpeechService _ttsService;
        private ILogger _logger;

        /// <summary>
        /// Set up classes dependencies
        /// </summary>
        public SpellThisService(IWordsRepository wordsRepository,
            ITextToSpeechService ttsService,
            ILogger<SpellThisService> logger)
        {

            if(wordsRepository == null)
                throw new InvalidOperationException("wordsRepository cannot be null");

            if (ttsService == null)
                throw new InvalidOperationException("ttsService cannot be null");

            if (logger == null)
                throw new InvalidOperationException("logger cannot be null");


            _wordsRepository = wordsRepository;
            _ttsService = ttsService;
            _logger = logger;

        }

        /// <summary>
        /// Get the user's spelling words
        /// </summary>
        /// <returns>User's spelling words</returns>
        public async Task<IEnumerable<Word>> GetSpellingWords()
        {
            
            try
            {

                List<Word> spellingWords = await _wordsRepository.GetAll().ToListAsync();

                return spellingWords;

            }
            catch(Exception ex)
            {

                _logger.LogError($"An unknown exception has occurred: {ex}");
                throw ex;

            }

        }

        /// <summary>
        /// Adds the spelling word to the users list
        /// </summary>
        /// <param name="word">Word to add</param>
        public Word AddSpellingWord(string word)
        {

            try
            {

                if (string.IsNullOrEmpty(word))
                    throw new InvalidOperationException("Word must contain a value");

                var newWord = new Word()
                {
                    AddDate = DateTime.Now,
                    Name = word
                };

                _wordsRepository.Add(newWord);

                return newWord;

            }
            catch(Exception ex)
            {

                _logger.LogError($"An unknown exception has occurred: {ex}");
                throw ex;

            }

        }

    }
}
