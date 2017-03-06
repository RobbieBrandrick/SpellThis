using Microsoft.EntityFrameworkCore;
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

        /// <summary>
        /// Set up classes dependencies
        /// </summary>
        public SpellThisService(IWordsRepository wordsRepository,
            ITextToSpeechService ttsService)
        {

            if(wordsRepository == null)
                throw new InvalidOperationException("wordsRepository cannot be null");

            if (ttsService == null)
                throw new InvalidOperationException("ttsService cannot be null");


            _wordsRepository = wordsRepository;
            _ttsService = ttsService;

        }

        /// <summary>
        /// Get the user's spelling words
        /// </summary>
        /// <returns>User's spelling words</returns>
        public async Task<IEnumerable<Word>> GetSpellingWords()
        {

            List<Word> spellingWords = null;

            try
            {

                spellingWords = await _wordsRepository.GetAll().ToListAsync();

            }
            catch(Exception ex)
            {
                //TODO:Add Logging
            }

            return spellingWords;

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
                //TODO:Add logging
            }

            return null;

        }

    }
}
