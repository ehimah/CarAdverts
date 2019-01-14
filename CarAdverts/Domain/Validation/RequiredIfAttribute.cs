using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CarAdverts.Domain.Validation
{
    public class RequiredIfAttribute:ValidationAttribute
    {
        private string TargetPropertyName { get; set; }
        private object TargetPropertyValue { get; set; }

        public RequiredIfAttribute(string otherPropertyName, object targetPropertyValue)
        {
            TargetPropertyName = otherPropertyName;
            TargetPropertyValue = targetPropertyValue;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var instance = validationContext.ObjectInstance; 
            var instanceType = instance.GetType();

            //using reflection, the the target property current value for comparison
            var valueToValidate = instanceType.GetProperty(TargetPropertyName).GetValue(instance);

            if (valueToValidate == null || TargetPropertyValue == null) return ValidationResult.Success;

            if(valueToValidate.ToString() == TargetPropertyValue.ToString() && value == null)
            {
                return new ValidationResult(ErrorMessage);
            }
            
            return ValidationResult.Success;
        }
    }
}
