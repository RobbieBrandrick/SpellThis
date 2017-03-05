using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Spellthis.Models;
using Spellthis.Services;

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
        public IActionResult ViewWords()
        {

            IEnumerable<Word> words = _spellThisService.GetSpellingWords();

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

            return Redirect("ViewWords");

        }

    }
}
