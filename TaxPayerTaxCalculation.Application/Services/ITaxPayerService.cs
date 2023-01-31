using TaxPayerTaxCalculation.Domain.Entities;

namespace TaxPayerTaxCalculation.Application.Services;

public interface ITaxPayerService
{
    Task<Taxes> CalculateTaxesAsync(TaxPayer taxPayer);
}