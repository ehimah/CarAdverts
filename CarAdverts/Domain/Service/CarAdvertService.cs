using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarAdverts.Domain.Data;
using CarAdverts.Domain.Entity;
using CarAdverts.Models;
using Microsoft.EntityFrameworkCore;

namespace CarAdverts.Domain.Service
{
    public class CarAdvertService : ICarAdvertService
    {
        private readonly ApplicationContext context;

        public Task<CarAdvert> Add(CarAdvert carAdvert)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<CarAdvert>> GetAllItemsAsync()
        {
            return await context.CarAdverts.ToListAsync();
        }

        public async Task<CarAdvert> GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<CarAdvert>> GetByQuery(CarAdvertQueryModel queryModel)
        {
            throw new NotImplementedException();
        }

        public Task Remove(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task Update(Guid id, CarAdvert carAdvert)
        {
            throw new NotImplementedException();
        }
    }
}
