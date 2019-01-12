using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CarAdverts.Domain.Entity
{
    public class CarAdvert
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public FuelType Fuel { get; set; }
        [Required]
        public int Price { get; set; }
        [Required]
        public bool New { get; set; }
        public int Mileage { get; set; }
        [Column(TypeName ="date")]
        public DateTime FirstRegistration { get; set; }
    }
}
