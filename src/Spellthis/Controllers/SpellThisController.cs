using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Spellthis.Models;
using Spellthis.Services;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Spellthis.Models.Account;

namespace Spellthis.Controllers
{
    [Authorize]
    public class SpellThisController : Controller
    {

        private ISpellThisService _spellThisService;
        private UserManager<ApplicationUser> _userManager;

        /// <summary>
        /// Set up the controllers dependencies
        /// </summary>
        public SpellThisController(
            ISpellThisService spellThisService,
            UserManager<ApplicationUser> userManager)
        {

            _spellThisService = spellThisService;
            _userManager = userManager;

        }

        [AllowAnonymous]
        public IActionResult Home()
        {
            return View();
        }

        /// <summary>
        /// View the users words
        /// </summary>
        /// <returns>Users words</returns>
        public async Task<IActionResult> ViewWords()
        {

            var user = await _userManager.GetUserAsync(HttpContext.User);

            IEnumerable<Word> words = await _spellThisService.GetSpellingWords(user.Id);

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
        [AutoValidateAntiforgeryToken]
        [HttpPost]
        public async Task<IActionResult> AddWord([Bind("Name")] Word word)
        {

            if (ModelState.IsValid)
            {

                var user = await _userManager.GetUserAsync(HttpContext.User);

                await _spellThisService.AddSpellingWord(word.Name, user.Id);

                return RedirectToAction("ViewWords");

            }
            else
            {

                return View(word);

            }
            

        }

        /// <summary>
        /// Removed a word from the repository
        /// </summary>
        public async Task<IActionResult> RemoveWord(int id)
        {

            var user = await _userManager.GetUserAsync(HttpContext.User);

            await _spellThisService.RemoveWord(id, user.Id);

            return RedirectToAction("ViewWords");

        }

    }
}
