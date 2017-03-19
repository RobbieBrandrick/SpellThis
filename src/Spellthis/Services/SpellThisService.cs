using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Spellthis.Models;
using Spellthis.Models.Account;
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
        Task<IEnumerable<Word>> GetSpellingWords(string userId);

        /// <summary>
        /// Adds the spelling word to the users list
        /// </summary>
        /// <param name="word">Word to add</param>
        Task<Word> AddSpellingWord(string wordName, string userId);

        /// <summary>
        /// Removed a word from the repository
        /// </summary>
        /// <param name="id">Id of the word to remove</param>
        /// <returns>Asynchronous Task of removing the word</returns>
        Task RemoveWord(int id, string userId);
    }

    public class SpellThisService : ISpellThisService
    {

        private IWordsRepository _wordsRepository;
        private ITextToSpeechService _ttsService;
        private UserManager<ApplicationUser> _userManager;
        private SignInManager<ApplicationUser> _signInManager;
        private ILogger _logger;
        private IHostingEnvironment _environment;

        private const string WordsAudioFileLocation = "\\Words\\AudioFiles";

        /// <summary>
        /// Set up classes dependencies
        /// </summary>
        public SpellThisService(IWordsRepository wordsRepository,
            ITextToSpeechService ttsService,
            UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
            ILogger<SpellThisService> logger,
            IHostingEnvironment environment)
        {

            _wordsRepository = wordsRepository;
            _ttsService = ttsService;
            _signInManager = signInManager;
            _userManager = userManager;
            _logger = logger;
            _environment = environment;

        }

        /// <summary>
        /// Get the user's spelling words
        /// </summary>
        /// <returns>User's spelling words</returns>
        public async Task<IEnumerable<Word>> GetSpellingWords(string userId)
        {

            try
            {
                List<Word> spellingWords = await _wordsRepository
                    .GetAllForUser(userId)
                    .ToListAsync();

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
        public async Task<Word> AddSpellingWord(string wordName, string userId)
        {

            try
            {

                await ValidateUser(userId);
                
                if (string.IsNullOrEmpty(wordName))
                    throw new InvalidOperationException("Word must contain a value");
                
                //If the word doesn't exist then create it, and associate it to the user
                Word word = await _wordsRepository
                    .GetAll()
                    .FirstOrDefaultAsync(w => w.Name.Equals(wordName, StringComparison.OrdinalIgnoreCase));

                if (word == null)
                {
                    word = await CreateNewWord(wordName);
                }

                await _wordsRepository.AddToUser(word.Id, userId);

                return word;

            }
            catch (Exception ex)
            {

                _logger.LogError($"An unknown exception has occurred: {ex}");
                throw ex;

            }

        }

        /// <summary>
        /// Removed a user word association from the repository
        /// </summary>
        /// <param name="wordId">Id of the word to remove</param>
        /// <param name="userId">userId of the word to remove</param>
        /// <returns>Asynchronous Task of removing the word</returns>
        public async Task RemoveWord(int wordId, string userId)
        {

            try
            {

                await ValidateUser(userId);
                var word = await ValidateWord(wordId);

                await _wordsRepository.RemoveUserWord(word.Id, userId);

            }
            catch (Exception ex)
            {

                _logger.LogError($"An unknown exception has occurred: {ex}");
                throw ex;

            }

        }


        /// <summary>
        /// Create a new word within the repository
        /// </summary>
        /// <param name="wordName">Name of the word to create</param>
        /// <returns>The new word</returns>
        private async Task<Word> CreateNewWord(string wordName)
        {
            var newWordAudioFile = Path.Combine(_environment.WebRootPath, "Words", wordName) + ".mp3";

            await _ttsService.CreateAudioFile(wordName, newWordAudioFile);

            var newWord = new Word()
            {
                AddDate = DateTime.Now,
                Name = wordName,
                AudioFileLocation = newWordAudioFile,
                AudioFileWebUri = "Words/" + wordName + ".mp3"
            };

            await _wordsRepository.Add(newWord);

            return newWord;
        }

        /// <summary>
        /// Validates that the user exists within the repository
        /// </summary>
        /// <param name="userId">Id to look up</param>
        private async Task<ApplicationUser> ValidateUser(string userId)
        {

            if (string.IsNullOrEmpty(userId))
                throw new InvalidOperationException("UserId cannot be null");

            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
                throw new InvalidOperationException("User does not exist within the repository");

            return user;

        }

        /// <summary>
        /// Validates that the word exists within the repository
        /// </summary>
        /// <param name="wordId">wordid to lookup</param>
        /// <returns>Word, if found</returns>
        private async Task<Word> ValidateWord(int wordId)
        {
            
            Word word = await _wordsRepository
                .GetAll()
                .FirstOrDefaultAsync(w => w.Id == wordId);

            if (word == null)
                throw new InvalidOperationException("Word does not exist within the repository.");

            return word;

        }

    }

}
