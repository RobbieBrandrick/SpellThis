using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Spellthis.Repositories;
using Spellthis.Models;
using Spellthis.Services;

namespace Spellthis.Controllers
{
    public class SpellThisController : Controller
    {

        private ISpellThisService _spellThisService;

        public SpellThisController(ISpellThisService spellThisService)
        {

            if (spellThisService == null) throw new InvalidOperationException("spellThisService cannot be null");

            _spellThisService = spellThisService;

        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ViewWords()
        {

            IEnumerable<Word> words = _spellThisService.GetSpellingWords();

            return View(words);

        }

    }
}
