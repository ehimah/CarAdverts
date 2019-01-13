using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarAdverts.Domain.Service;
using CarAdverts.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarAdverts.Controllers
{
    [Produces("application/json")]
    [Route("api/CarAdverts")]
    public class CarAdvertsController : Controller
    {
        private readonly ICarAdvertService carAdvertService;

        public CarAdvertsController(ICarAdvertService carAdvertService)
        {
            this.carAdvertService = carAdvertService;
        }


        // GET: api/CarAdverts
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var carAdverts = await carAdvertService.GetAllItemsAsync();
            return Ok(carAdverts);
        }

        // GET: api/CarAdverts/5
        [HttpGet("{id}", Name = "Get")]
        public ActionResult Get(int id, [FromQuery] CarAdvertQueryModel carAdveryQuery = null)
        {
            return Ok();
        }
        
        // POST: api/CarAdverts
        [HttpPost]
        public ActionResult Post([FromBody]CarAdvertRequestModel value)
        {
            return CreatedAtAction("Get",value);
        }
        
        // PUT: api/CarAdverts/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, CarAdvertRequestModel value)
        {
            return NoContent();
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            return Ok();
        }
    }
}
