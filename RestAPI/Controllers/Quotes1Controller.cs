using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestAPI.Models;

namespace RestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Quotes1Controller : ControllerBase
    {
        static List<Quote> quotes = new List<Quote>()
        {
            new Quote(){ Id=1, Author="Satvik Ramineni",
                Description ="This is my name",
                Title ="My first rest api" },

               new Quote(){ Id=2, Author="Satvik Ramineni",
                Description ="This is my name",
                Title ="My first rest api2" }
        };

        [HttpGet]
        public IEnumerable<Quote> Get()
        {
            return quotes;
        }

        [HttpPost]
        public void Post([FromBody]Quote ass)
        {
            quotes.Add(ass);
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody]Quote quote)
        {
            quotes[id] = quote;
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            quotes.RemoveAt(id);
        }
    }
}