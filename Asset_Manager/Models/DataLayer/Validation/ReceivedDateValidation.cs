using System.ComponentModel.DataAnnotations;

namespace Asset_Manager.Models  
{
    public class ReceivedDateValidation : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is DateTime ReceivedDate)
            {
                if (ReceivedDate > DateTime.Now.Date)
                {
                    return new ValidationResult("Recieved date can't be greater than today.");
                }
            }
            return ValidationResult.Success;
        }
    }
    
}
