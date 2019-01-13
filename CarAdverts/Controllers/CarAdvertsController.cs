﻿using System;
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
        public ActionResult Get()
        {
            var carAdverts = carAdvertService.GetAllItems();
            return Ok(carAdverts);
        }

        // GET: api/CarAdverts/5
        [HttpGet("{id}", Name = "Get")]
        public async Task<ActionResult> Get(Guid id, [FromQuery] CarAdvertQueryModel carAdveryQuery = null)
        {
            return Ok();
        }
        
        // POST: api/CarAdverts
        [HttpPost]
        public async Task<ActionResult> Post([FromBody]CarAdvertRequestModel value)
        {
            return CreatedAtAction("Get",value);
        }
        
        // PUT: api/CarAdverts/5
        [HttpPut("{id}")]
        public ActionResult Put(Guid id, CarAdvertRequestModel value)
        {
            return NoContent();
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public ActionResult Delete(Guid id)
        {
            return Ok();
        }
    }
}
