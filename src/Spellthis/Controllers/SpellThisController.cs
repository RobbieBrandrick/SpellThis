using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Spellthis.Repositories;
using Spellthis.Models;

namespace Spellthis.Controllers
{
    public class SpellThisController : Controller
    {

        IWordsRepository _wordsRepository;

        public SpellThisController(IWordsRepository wordsRepository)
        {

            if (wordsRepository == null) throw new InvalidOperationException("wordsRepository cannot be null");

            _wordsRepository = wordsRepository;

        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ViewWords()
        {

            List<Word> words = _wordsRepository.GetAll();

            return View(words);

        }

    }
}
