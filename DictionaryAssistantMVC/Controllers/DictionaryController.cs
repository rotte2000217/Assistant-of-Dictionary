using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using DictionaryAssistantMVC.Services;
using DictionaryAssistantMVC.Dictionary;


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
        public async Task<IActionResult> GetAllLettersByCount()
        {
            List<LetterStatistics> allLetters = await dictionaryService.GetAllLettersByCount();

            if (allLetters == null || allLetters?.Count < 1)
            {
                return NotFound();
            }

            return Ok(allLetters);
        }

        [HttpGet("top-letters/{howMany:int}")]
        public async Task<IActionResult> GetTopLettersByCount(int howMany)
        {
            if (howMany > Alphabet.AlphabetCount)
            {
                return BadRequest(howMany);
            }

            List<LetterStatistics> topLetters = await dictionaryService.GetTopLettersByCount(howMany);

            if (topLetters == null || topLetters?.Count < 1)
            {
                return NotFound();
            }

            return Ok(topLetters);
        }

        [HttpGet("all-statistics")]
        public async Task<IActionResult> GetAllLetters()
        {
            List<LetterStatistics> allLetters = await dictionaryService.GetAllLetters();

            if (allLetters == null || allLetters?.Count < 1)
            {
                return NotFound();
            }

            return Ok(allLetters);
        }

        [HttpGet("longest-words/{howMany:int}")]
        [Produces("application/json")]
        public async Task<IActionResult> GetLongestWords(int howMany)
        {
            Dictionary<char, List<string>> longestWords = await dictionaryService.GetLongestWords(howMany);

            if (longestWords == null || longestWords?.Keys.Count < 1)
            {
                return NotFound();
            }

            // .NET Builtin JSON doesn't support dictionaries?? We'll use Newtonsoft. Okay...  :|
            // Aaand, ASP.NET-Core will escape the quotes in the JSON if an Ok() is used.  >:(
            return Content(JsonConvert.SerializeObject(longestWords), "application/json");
        }

        [HttpGet("shortest-words/{howMany:int}")]
        [Produces("application/json")]
        public async Task<IActionResult> GetShortestWords(int howMany)
        {
            Dictionary<char, List<string>> shortestWords = await dictionaryService.GetShortestWords(howMany);

            if (shortestWords == null || shortestWords?.Keys.Count < 1)
            {
                return NotFound();
            }

            return Content(JsonConvert.SerializeObject(shortestWords), "application/json");
        }

        [HttpPost("add-words")]
        [Produces("text/plain")]
        public async Task<IActionResult> AddWords(IFormFile wordListFile)
        {
            int numberAdded = 0;

            using (var stream = wordListFile.OpenReadStream())
            {
                numberAdded = await dictionaryService.AddWordsToDictionary(stream);
            }

            return Content(numberAdded.ToString(), "text/plain");
        }
    }
}
