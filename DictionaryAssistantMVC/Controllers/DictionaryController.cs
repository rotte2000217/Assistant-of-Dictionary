using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DictionaryAssistantMVC.Dictionary;
using DictionaryAssistantMVC.Services;


namespace DictionaryAssistantMVC.Controllers
{
    [ApiController]
    [Route("api/dictionary")]
    public class DictionaryController : Controller
    {
        private readonly IDictionaryService dictionaryService;

        public DictionaryController(IDictionaryService dictionaryService)
        {
            this.dictionaryService = dictionaryService;
        }

        [HttpGet("all-letters")]
        public async Task<IActionResult> GetAllLetters()
        {
            List<LetterStatistics> allLetters = await dictionaryService.GetAllLetters();

            if (allLetters == null || allLetters?.Count < 1)
            {
                return NotFound();
            }

            return Ok(allLetters);
        }

        [HttpGet("top-letters/{howMany:int}")]
        public async Task<IActionResult> GetTopLetters(int howMany)
        {
            if (howMany > Alphabet.AlphabetCount)
            {
                return BadRequest(howMany);
            }

            List<LetterStatistics> topLetters = await dictionaryService.GetTopLetters(howMany);

            if (topLetters == null || topLetters?.Count < 1)
            {
                return NotFound();
            }

            return Ok(topLetters);
        }
    }
}
