using System.ComponentModel.DataAnnotations;

namespace TaxPayerTaxCalculation.Domain.Entities
{
    public class TaxPayer
    {
        [Required]
        [RegularExpression(@"^[a-zA-Z]+(\s[a-zA-Z]+)?$", ErrorMessage = "FullName should contain only letters and spaces")]
        [MinLength(2, ErrorMessage = "FullName should be at least two words")]
        public string? FullName { get; set; }

        [Required]
        [RegularExpression(@"^\d{5,11}$", ErrorMessage = "SSN should be a valid 5 to 10 digits number")]
        public string SSN { get; set; }

        [Required]
        [RegularExpression(@"^\d+(\.\d{1,2})?$")]
        [Range(0, 99999.99, ErrorMessage = "GrossIncome should be a valid number")]
        public decimal GrossIncome { get; set; }

        [RegularExpression(@"^\d+(\.\d{1,2})?$")]
        [Range(0, 99999.99, ErrorMessage = "GrossIncome should be a valid number")]
        public decimal CharitySpent { get; set; }

        [DataType(DataType.Date, ErrorMessage = "DateOfBirth should be a valid date")]
        public DateTime? DateOfBirth { get; set; }

        public TaxPayer(string fullName, string ssn, decimal grossIncome, decimal charitySpent, DateTime? dateOfBirth)
        {
            FullName = fullName;
            SSN = ssn;
            GrossIncome = grossIncome;
            CharitySpent = charitySpent;
            DateOfBirth = dateOfBirth;
        }
        public TaxPayer()
        {

        }
    }
}
