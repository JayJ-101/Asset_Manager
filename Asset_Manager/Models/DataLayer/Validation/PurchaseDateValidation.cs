using System.ComponentModel.DataAnnotations;

namespace Asset_Manager.Models  
{
    public class PurchaseDateValidation : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is DateTime purchaseDate)
            {
                if (purchaseDate > DateTime.Now.Date)
                {
                    return new ValidationResult("Purchase Date cannot be in the future.");
                }
            }
            return ValidationResult.Success;
        }
    }
}
