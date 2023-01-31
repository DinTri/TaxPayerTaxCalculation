namespace TaxPayerTaxCalculation.Domain.Entities
{
    public class Taxes
    {
        public string? SSN { get; set; }
        public decimal GrossIncome { get; set; }
        public decimal CharitySpent { get; set; }
        public decimal IncomeTax { get; set; }
        public decimal SocialTax { get; set; }
        public decimal TotalTax { get; set; }
        public decimal NetIncome { get; set; }

        //public Taxes(decimal grossIncome, decimal charitySpent, decimal incomeTax, decimal socialTax, decimal totalTax, decimal netIncome)
        //{
        //    GrossIncome = grossIncome;
        //    CharitySpent = charitySpent;
        //    IncomeTax = incomeTax;
        //    SocialTax = socialTax;
        //    TotalTax = totalTax;
        //    NetIncome = netIncome;
        //}

        //public Taxes()
        //{
        //}
    }
}
