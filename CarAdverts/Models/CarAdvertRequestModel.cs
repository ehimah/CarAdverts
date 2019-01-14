using CarAdverts.Domain.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CarAdverts.Models
{
    public class CarAdvertRequestModel
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public FuelType? Fuel { get; set; }
        [Required]
        public int? Price { get; set; }
        [Required]
        public bool? New { get; set; }
        [RequiredIf("New", false, ErrorMessage = "You must specify the mileage for used cars")]
        public int? Mileage { get; set; }
        [RequiredIf("New", false, ErrorMessage = "You must specify the FirstRegistration date for used cars")]
        public DateTime? FirstRegistration { get; set; }
    }
}
