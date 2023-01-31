using Microsoft.Extensions.Options;
using TaxPayerTaxCalculation.Application.Repositories;
using TaxPayerTaxCalculation.Application.Settings;
using TaxPayerTaxCalculation.Domain.Entities;

namespace TaxPayerTaxCalculation.Application.Services
{
    public class TaxPayerService : ITaxPayerService
    {
        private readonly ITaxPayerRepository _taxPayerRepository;
        CalculationSettings _appSettings;

        public TaxPayerService(IOptions<CalculationSettings> appSettings, ITaxPayerRepository taxPayerRepository)
        {
            _taxPayerRepository = taxPayerRepository;
            _appSettings = appSettings.Value;
        }

        public async Task<Taxes> CalculateTaxesAsync(TaxPayer taxPayer)
        {

            decimal grossIncome = taxPayer.GrossIncome;
            decimal charitySpent = taxPayer.CharitySpent;

            decimal taxableIncome = (grossIncome > 1000) ? grossIncome - 1000 - charitySpent : 0;
            decimal incomeTax = (taxableIncome > 0) ? taxableIncome * _appSettings.IncomeTaxRate : 0;
            decimal socialContribution = (taxableIncome > 0 && taxableIncome <= 3000) ? taxableIncome * _appSettings.SocialContributiomRate : (taxableIncome > 3000) ? 3000 * _appSettings.SocialContributiomRate : 0;
            decimal netIncome = grossIncome - incomeTax - socialContribution;

            // Return calculated Taxes
            var taxes = new Taxes
            {
                SSN = taxPayer.SSN,
                GrossIncome = grossIncome,
                NetIncome = netIncome,
                IncomeTax = incomeTax,
                SocialTax = socialContribution,
                CharitySpent = charitySpent
            };

            await _taxPayerRepository.AddTaxesAsync(taxPayer.SSN, taxes);
            return taxes;
        }
    }
}
