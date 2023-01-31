using MediatR;
using TaxPayerTaxCalculation.Domain.Entities;

namespace TaxPayerTaxCalculation.Application.Commands
{
    public class CalculateTaxCommand : IRequest<Taxes>
    {
        public TaxPayer? TaxPayer { get; set; }

        public CalculateTaxCommand(TaxPayer? taxPayer)
        {
            TaxPayer = taxPayer;
        }
    }
}
