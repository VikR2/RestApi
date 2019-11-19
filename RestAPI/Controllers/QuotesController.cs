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
        public IActionResult Get()
        {
            //return Ok(_quotesDbContext.Quotes);
            return StatusCode(StatusCodes.Status200OK);

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
