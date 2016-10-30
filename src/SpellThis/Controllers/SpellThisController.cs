using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public SpellThisController(IWordRepository wordRepository)
        {

            _wordRepository = wordRepository;

        }
        
        [HttpGet]
        public IEnumerable<Word> GetAll()
        {

            return _wordRepository.GetAll();

        }

    }
}
