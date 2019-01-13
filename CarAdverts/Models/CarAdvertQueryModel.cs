using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarAdverts.Models
{
    public class CarAdvertQueryModel
    {
        public string Title { get; set; }
        public FuelType? Fuel { get; set; }
        public int? Price { get; set; }
        public bool? New { get; set; }
        public int? Mileage { get; set; }
        public DateTime FirstRegistration { get; set; }
    }
}
