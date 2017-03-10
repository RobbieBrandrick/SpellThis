using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Spellthis.Models;
using Spellthis.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
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
        Task<Word> AddSpellingWord(string wordName);

        /// <summary>
        /// Removed a word from the repository
        /// </summary>
        /// <param name="id">Id of the word to remove</param>
        /// <returns>Asynchronous Task of removing the word</returns>
        Task RemoveWord(int id);
    }

    public class SpellThisService : ISpellThisService
    {

        private IWordsRepository _wordsRepository;
        private ITextToSpeechService _ttsService;
        private ILogger _logger;
        private IHostingEnvironment _environment;

        private const string WordsAudioFileLocation = "\\Words\\AudioFiles";

        /// <summary>
        /// Set up classes dependencies
        /// </summary>
        public SpellThisService(IWordsRepository wordsRepository,
            ITextToSpeechService ttsService,
            ILogger<SpellThisService> logger,
            IHostingEnvironment environment)
        {

            _wordsRepository = wordsRepository;
            _ttsService = ttsService;
            _logger = logger;
            _environment = environment;

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
            catch (Exception ex)
            {

                _logger.LogError($"An unknown exception has occurred: {ex}");
                throw ex;

            }

        }

        /// <summary>
        /// Adds the spelling word to the users list
        /// </summary>
        /// <param name="wordName">Word to add</param>
        public async Task<Word> AddSpellingWord(string wordName)
        {

            try
            {

                if (string.IsNullOrEmpty(wordName))
                    throw new InvalidOperationException("Word must contain a value");

                //If the word already exists within the repository then return it.
                Word word = await _wordsRepository
                    .GetAll()
                    .FirstOrDefaultAsync(w => w.Name.Equals(wordName, StringComparison.OrdinalIgnoreCase));

                if (word != null)
                {
                    return word;
                }
                
                var newWordAudioFile = Path.Combine(_environment.WebRootPath, "Words", wordName) + ".mp3";

                await _ttsService.CreateAudioFile(wordName, newWordAudioFile);

                var newWord = new Word()
                {
                    AddDate = DateTime.Now,
                    Name = wordName,
                    AudioFileLocation = newWordAudioFile,
                    AudioFileWebUri = "Words/" + wordName + ".mp3"                   
                };

                _wordsRepository.Add(newWord);

                return newWord;

            }
            catch (Exception ex)
            {

                _logger.LogError($"An unknown exception has occurred: {ex}");
                throw ex;

            }

        }

        /// <summary>
        /// Removed a word from the repository
        /// </summary>
        /// <param name="id">Id of the word to remove</param>
        /// <returns>Asynchronous Task of removing the word</returns>
        public async Task RemoveWord(int id)
        {

            try
            {

                Word word = await _wordsRepository
                    .GetAll()
                    .FirstOrDefaultAsync(w => w.Id == id);

                if (word == null)
                    throw new InvalidOperationException("Word does not exist within the repository.");

                await _wordsRepository.Remove(word);
                
            }
            catch (Exception ex)
            {

                _logger.LogError($"An unknown exception has occurred: {ex}");
                throw ex;

            }

        }
    }
}
