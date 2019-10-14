using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChefsAndDishes.Models
{
    public class Chef
    {
        [Key]
        public int ChefId {get;set;}
        [Required]
        public string FirstName {get;set;}
        [Required]
        public string LastName {get;set;}
        [Required]
        [DataType(DataType.Date)]
        [PastDate]
        [MinimumAge(18, ErrorMessage = "Must be 18 years or older")]
        public DateTime Birthday {get;set;}
        [NotMapped]
        public int Age {get;set;}
        public DateTime Created_at {get;set;} = DateTime.Now;
        public DateTime Updated_at {get;set;} = DateTime.Now;
        public List<Dish> CreatedDishes {get;set;}

    }

}
public class PastDateAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if((DateTime)value > DateTime.Now)
            {
                return new ValidationResult("Date must be in the future");
            }
            return ValidationResult.Success;
        }
    }

public class MinimumAgeAttribute: ValidationAttribute
{
    int _minimumAge;

    public MinimumAgeAttribute(int minimumAge)
    {
      _minimumAge = minimumAge;
    }

    public override bool IsValid(object value)
    {
        DateTime date;
        if (DateTime.TryParse(value.ToString(),out date))
        {
            return date.AddYears(_minimumAge) < DateTime.Now;
        }

        return false;
    }
}

