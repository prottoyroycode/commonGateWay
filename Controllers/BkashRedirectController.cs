using Bkash_Service_API.Data;
using Bkash_Service_API.Models.Entities.BkashRecurringModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Bkash_Service_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BkashRedirectController : ControllerBase
    {
        public readonly DataContext _context;
        public BkashRedirectController(DataContext context)
        {
            _context = context;
        }
        // GET: api/<BkashRedirectController>
        [HttpGet]
        public async Task<IActionResult> Get(string reference ,string status)
        {
            var backData = new CreateSubscriptionCalBack()
            {
                status= status,
                reference=reference,
            };
            var data = await _context.CreateSubscriptionCalBacks.AddAsync(backData);
            await _context.SaveChangesAsync();
            var url = await _context.CreateSubscriptionResponses.Where(s => s.reference == reference).Select(p => p.client_RedirectURL).FirstOrDefaultAsync();
            if(url !=null)
            return Redirect(url + '/' + reference + '/' + status + '/');
            return Redirect("https://partnersintregation.techapi24.com" + '/' + reference + '/' + status + '/');
            //return Ok(status);
        }

        // GET api/<BkashRedirectController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<BkashRedirectController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<BkashRedirectController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<BkashRedirectController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
