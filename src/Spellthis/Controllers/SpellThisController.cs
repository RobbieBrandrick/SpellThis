using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Spellthis.Models;
using Spellthis.Services;
using System.Threading.Tasks;

namespace Spellthis.Controllers
{
    public class SpellThisController : Controller
    {

        private ISpellThisService _spellThisService;

        /// <summary>
        /// Set up the controllers dependencies
        /// </summary>
        public SpellThisController(ISpellThisService spellThisService)
        {

            if (spellThisService == null) throw new InvalidOperationException("spellThisService cannot be null");

            _spellThisService = spellThisService;

        }

        /// <summary>
        /// View the users words
        /// </summary>
        /// <returns>Users words</returns>
        public async Task<IActionResult> ViewWords()
        {

            IEnumerable<Word> words = await _spellThisService.GetSpellingWords();

            return View(words);

        }

        /// <summary>
        /// View AddWord UI
        /// </summary>
        public IActionResult AddWord()
        {

            return View();

        }

        /// <summary>
        /// Add a word to the service
        /// </summary>
        /// <returns>Redirects to view words</returns>
        [HttpPost]
        public IActionResult AddWord(string word)
        {

           _spellThisService.AddSpellingWord(word);

            return RedirectToAction("ViewWords");

        }

        /// <summary>
        /// Removed a word from the repository
        /// </summary>
        public async Task<IActionResult> RemoveWord(int id)
        {

            await _spellThisService.RemoveWord(id);

            return RedirectToAction("ViewWords");

        }

    }
}
