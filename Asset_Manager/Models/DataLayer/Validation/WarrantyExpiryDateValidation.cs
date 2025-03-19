using System.ComponentModel.DataAnnotations;

namespace Asset_Manager.Models  
{
    public class WarrantyExpiryDateValidation : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is DateTime warrantyExpiryDate)
            {
                if (warrantyExpiryDate < DateTime.Now.Date)
                {
                    return new ValidationResult("Warranty Expiry Date must be in the future.");
                }
            }
            return ValidationResult.Success;
        }
    }
    
}
