using CarAdverts.Domain.Entity;
using CarAdverts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarAdverts.Domain.Service
{
    public interface ICarAdvertService
    {
        Task<IEnumerable<CarAdvert>> GetAllItemsAsync();
        Task<CarAdvert> GetById(Guid id);
        Task<IEnumerable<CarAdvert>> GetByQuery(CarAdvertQueryModel queryModel);
        Task<CarAdvert> Add(CarAdvert carAdvert);
        Task Update(Guid id, CarAdvert carAdvert);
        Task Remove(Guid id);
    }
}
