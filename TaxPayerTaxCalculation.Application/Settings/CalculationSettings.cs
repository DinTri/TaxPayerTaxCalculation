namespace TaxPayerTaxCalculation.Application.Settings
{
    public class CalculationSettings
    {
        public const string CalculationVariables = "CalculationVariables";
        public decimal IncomeTaxRate { get; set; }
        public decimal SocialContributiomRate { get; set; }
    }
}
