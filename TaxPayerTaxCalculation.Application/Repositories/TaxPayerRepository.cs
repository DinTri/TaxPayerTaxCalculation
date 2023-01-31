using Microsoft.Extensions.Caching.Memory;
using TaxPayerTaxCalculation.Domain.Entities;

namespace TaxPayerTaxCalculation.Application.Repositories
{
    public class TaxPayerRepository : ITaxPayerRepository
    {
        private readonly IMemoryCache _memoryCache;
        private readonly List<TaxPayer> _taxPayers;
        private readonly List<Taxes> _taxes;
        public TaxPayerRepository(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
            _taxPayers = new List<TaxPayer>();
            _taxes = new List<Taxes>();
        }

        public async Task<List<TaxPayer>> GetTaxPayersAsync()
        {
            return await Task.FromResult(_taxPayers);
        }

        public Task<bool> TaxPayerExistsAsync(string ssn)
        {
            return Task.FromResult(_taxPayers.Any(t => t.SSN == ssn));
        }

        public async Task<bool> AddTaxPayerAsync(TaxPayer taxPayer)
        {
            _taxPayers.Add(taxPayer);
            _memoryCache.Set(taxPayer.SSN, taxPayer);
            return await Task.FromResult(true);
        }

        public Task<bool> UpdateTaxPayerAsync(TaxPayer taxPayer)
        {
            var existingTaxPayer = _taxPayers.FirstOrDefault(t => t.SSN == taxPayer.SSN);
            if (existingTaxPayer == null)
            {
                return Task.FromResult(false);
            }

            existingTaxPayer.FullName = taxPayer.FullName;
            existingTaxPayer.GrossIncome = taxPayer.GrossIncome;
            existingTaxPayer.CharitySpent = taxPayer.CharitySpent;
            _memoryCache.Set(existingTaxPayer.SSN, existingTaxPayer);
            return Task.FromResult(true);
        }

        public async Task<bool> DeleteTaxPayerAsync(string ssn)
        {
            var existingTaxPayer = _taxPayers.FirstOrDefault(x => ssn != null && x.SSN.ToString() == ssn);
            if (existingTaxPayer == null) return await Task.FromResult(false);
            _taxPayers.Remove(existingTaxPayer);
            _memoryCache.Remove(existingTaxPayer.SSN);
            return await Task.FromResult(true);
        }

        public async Task<TaxPayer> GetTaxPayerAsync(string ssn)
        {
            if (_memoryCache.TryGetValue(ssn, out TaxPayer? taxPayer))
            {
                return await Task.FromResult(taxPayer);
            }
            taxPayer = _taxPayers.FirstOrDefault(t => t.SSN.ToString() == ssn);
            if (taxPayer != null)
            {
                _memoryCache.Set(ssn, taxPayer);
            }
            return await Task.FromResult(taxPayer);
        }


        public async Task<bool> AddTaxesAsync(string ssn, Taxes taxes)
        {
            _taxes.Add(taxes);
            _memoryCache.Set(ssn, taxes);

            return await Task.FromResult(true);

        }

        public async Task<Taxes> GetTaxesAsync(string ssn)
        {
            return await Task.FromResult(_memoryCache.Get<Taxes>(ssn));
        }

    }
}
