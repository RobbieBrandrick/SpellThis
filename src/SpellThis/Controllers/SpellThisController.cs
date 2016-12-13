using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using SpellThis.Repositories;
using SpellThis.Models;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace SpellThis.Controllers
{

    [Route("api/[controller]")]
    public class SpellThisController : Controller
    {

        IWordRepository _wordRepository;

        /// <summary>
        /// Set up controllers dependencies
        /// </summary>
        public SpellThisController(IWordRepository wordRepository)
        {

            _wordRepository = wordRepository;

        }
        
        /// <summary>
        /// Get all the words in the repository
        /// </summary>
        /// <returns>All the words in the repository</returns>
        [HttpGet]
        public IEnumerable<Word> GetAll()
        {

            return _wordRepository.GetAll();

        }

    }
}
