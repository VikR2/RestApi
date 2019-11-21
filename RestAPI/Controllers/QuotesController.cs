using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestAPI.Data;
using RestAPI.Models;


namespace RestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuotesController : ControllerBase
    {
        private QuotesDbContext _quotesDbContext;

        public QuotesController(QuotesDbContext quotesDbContext) {
            _quotesDbContext = quotesDbContext;
        }
        // GET: api/Quotes
        [HttpGet]
        [ResponseCache(Duration = 60, Location = ResponseCacheLocation.Client)]
        public IActionResult Get(string sort)
        {
            IQueryable<Quote> quotes;

            switch (sort)
            {
                case "desc" :
                    quotes = _quotesDbContext.Quotes.OrderByDescending(q => q.DateCreated);
                    break;
                case "asc":
                    quotes = _quotesDbContext.Quotes.OrderBy(q => q.DateCreated);
                    break;
                default:
                    quotes = _quotesDbContext.Quotes;
                    break;
            }
            return Ok(quotes);
            //return StatusCode(StatusCodes.Status200OK);

        }

        //pagination , skip records
        [HttpGet("[action]")]
        public IActionResult PagingQuote(int? pageNumber, int? pageSize)
        {
            var quotes = _quotesDbContext.Quotes;
            var currentpageNumber = pageNumber ?? 1;
            var currentpageSize = pageSize ?? 2;
            /*
             *pagenumber =2 , pagesize=10
             * (2-1) * 10 = 10 ; that means skip the first 10 records
             */
            return Ok(quotes.Skip((currentpageNumber - 1 )* currentpageSize).Take(currentpageSize));
        }

        // Search Functionality
        [HttpGet("[action]")]
        public IActionResult SearchQuote(string type)
        {
           var quotes = _quotesDbContext.Quotes.Where(x => x.Type.StartsWith(type));
            return Ok(quotes);
        }

        // GET: api/Quotes/5
        [HttpGet("{id}", Name = "Get")]
        public IActionResult Get(int id)
        {
            var quote =_quotesDbContext.Quotes.Find(id);
            if (quote == null)
            {
                return NotFound("No Records Found!");

            }
            else
            {
                return Ok(quote);
            }
            
        }

        // POST: api/Quotes
        [HttpPost]
        public IActionResult Post([FromBody] Quote quote)
        {
            _quotesDbContext.Quotes.Add(quote);
            _quotesDbContext.SaveChanges();
            return StatusCode(StatusCodes.Status201Created);
        }

        // PUT: api/Quotes/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Quote quote)
        {
           var entity = _quotesDbContext.Quotes.Find(id);
            if (entity == null)
            {
                return NotFound("no record found against this id");
            }
            else
            {
                entity.Title = quote.Title;
                entity.Author = quote.Author;
                entity.Description = quote.Description;
                entity.Type = quote.Type;
                entity.DateCreated = quote.DateCreated;
                _quotesDbContext.SaveChanges();
                return Ok("Record Updated ");
            }
           
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var quote =_quotesDbContext.Quotes.Find(id);
            if (quote==null)
            {
                return NotFound($"No Quote with {0}");
            }
            else
            {
                _quotesDbContext.Quotes.Remove(quote);
                _quotesDbContext.SaveChanges();

                return Ok("Quote Deleted");
            }
           
        }
    }
}
