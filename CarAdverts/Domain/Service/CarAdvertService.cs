using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarAdverts.Domain.Data;
using CarAdverts.Domain.Entity;
using CarAdverts.Models;
using CarAdverts.Utlils;
using Microsoft.EntityFrameworkCore;

namespace CarAdverts.Domain.Service
{
    public class CarAdvertService : ICarAdvertService
    {
        private readonly ApplicationContext context;

        public CarAdvertService(ApplicationContext context)
        {
            this.context = context;
        }

        public CarAdvert Add(CarAdvert carAdvert)
        {
            if (carAdvert == null)
                throw new ArgumentNullException(nameof(carAdvert));
            var result = context.CarAdverts.Add(carAdvert);
             context.SaveChanges();
            return result.Entity;
        }

        public IEnumerable<CarAdvert> GetAllItems()
        {
            return context.CarAdverts.ToList();
        }

        public CarAdvert GetById(Guid id)
        {
            return context.CarAdverts.Find(id);
        }

        public IEnumerable<CarAdvert> GetByQuery(CarAdvertQueryModel queryModel)
        {
            var items = context.CarAdverts
                .WhereIf(!string.IsNullOrWhiteSpace(queryModel.Title), ca => ca.Title.Trim().ToLowerInvariant().Contains(queryModel.Title.Trim().ToLowerInvariant()))
                .WhereIf(queryModel.Fuel.HasValue, ca => ca.Fuel == queryModel.Fuel.Value)
                .WhereIf(queryModel.Price.HasValue, ca => ca.Price == queryModel.Price.Value)
                .WhereIf(queryModel.New.HasValue, ca => ca.New == queryModel.New.Value)
                .WhereIf(queryModel.Mileage.HasValue, ca => ca.Mileage == queryModel.Mileage.Value);
            return items;
        }

        public  void Remove(Guid id)
        {
            var carAdvertToDelete = context.CarAdverts.Find(id);
            context.CarAdverts.Remove(carAdvertToDelete);
            context.SaveChanges();
        }

        public void Update(CarAdvert carAdvert)
        {
            if (carAdvert == null)
                throw new ArgumentNullException(nameof(carAdvert));
            
            context.Entry(carAdvert).State = EntityState.Modified;
            context.SaveChanges();
        }
    }
}
