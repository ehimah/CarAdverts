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
        IEnumerable<CarAdvert> GetAllItems();
        CarAdvert GetById(Guid id);
        IEnumerable<CarAdvert> GetByQuery(CarAdvertQueryModel queryModel);
        CarAdvert Add(CarAdvert carAdvert);
        void Update(CarAdvert carAdvert);
        void Remove(Guid id);
    }
}
