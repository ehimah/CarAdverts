using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CarAdverts.Domain.Entity;
using CarAdverts.Domain.Service;
using CarAdverts.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace CarAdverts.Controllers
{
    [Produces("application/json")]
    [Route("api/CarAdverts")]
    public class CarAdvertsController : Controller
    {
        private readonly ICarAdvertService carAdvertService;
        private readonly IMapper mapper;

        public CarAdvertsController(ICarAdvertService carAdvertService, IMapper mapper)
        {
            this.carAdvertService = carAdvertService;
            this.mapper = mapper;
        }
        
        // GET: api/CarAdverts
        [HttpGet]
        public ActionResult Get([FromQuery] CarAdvertQueryModel carAdveryQuery = null)
        {
            var carAdverts = carAdvertService.GetByQuery(carAdveryQuery).ToList();
            var response = mapper.Map<List<CarAdvertResponseModel>>(carAdverts);
            return Ok(response);
        }

        // GET: api/CarAdverts/5
        [HttpGet("{id}", Name = "Get")]
        public ActionResult Get(Guid id)
        {
            var carAdvert = carAdvertService.GetById(id);
            if (carAdvert == null)
                return NotFound();
            var model = mapper.Map<CarAdvertResponseModel>(carAdvert);
            return Ok(model);
        }
        
        // POST: api/CarAdverts
        [HttpPost]
        public ActionResult Post([FromBody]CarAdvertRequestModel requestModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var carAdvert = mapper.Map<CarAdvert>(requestModel);
            carAdvertService.Add(carAdvert);

            return CreatedAtAction("Get",requestModel);
        }
        
        // PUT: api/CarAdverts/5
        [HttpPut("{id}")]
        public ActionResult Put(Guid id, [FromBody] CarAdvertRequestModel requestModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var carAdvert = carAdvertService.GetById(id);
            if (carAdvert == null)
                return NotFound();

            mapper.Map(requestModel, carAdvert);
            carAdvertService.Update(carAdvert);
            return NoContent();
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public ActionResult Delete(Guid id)
        {
            var carAdvert = carAdvertService.GetById(id);
            if (carAdvert == null)
                return NotFound();

            return Ok();
        }

        [HttpPatch]
        public ActionResult Patch(Guid id, 
            [FromBody] JsonPatchDocument<CarAdvertRequestModel> patchDocument)
        {
            if (patchDocument == null)
            {
                return BadRequest();
            }

            var existingCarAdvert = carAdvertService.GetById(id);
            var carAdvertToPatch = mapper.Map<CarAdvertRequestModel>(existingCarAdvert);

            patchDocument.ApplyTo(carAdvertToPatch, ModelState);

            TryValidateModel(carAdvertToPatch);

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            mapper.Map(carAdvertToPatch, existingCarAdvert);
            carAdvertService.Update(existingCarAdvert);

            return NoContent();
        }
    }
}
