using TaxPayerTaxCalculation.Domain.Entities;

namespace TaxPayerTaxCalculation.Application.Repositories
{
    public interface ITaxPayerRepository
    {
        Task<bool> UpdateTaxPayerAsync(TaxPayer taxPayer);
        Task<bool> DeleteTaxPayerAsync(string ssn);
        Task<bool> AddTaxesAsync(string ssn, Taxes taxes);
        Task<Taxes> GetTaxesAsync(string ssn);
        Task<List<TaxPayer>> GetTaxPayersAsync();
        Task<bool> TaxPayerExistsAsync(string ssn);
        Task<bool> AddTaxPayerAsync(TaxPayer taxPayer);
        Task<TaxPayer> GetTaxPayerAsync(string ssn);
    }
}
