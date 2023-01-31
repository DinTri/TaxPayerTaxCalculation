using MediatR;
using TaxPayerTaxCalculation.Domain.Entities;

namespace TaxPayerTaxCalculation.Application.Commands
{
    public class AddTaxPayerCommand : IRequest<TaxPayer>
    {
        public TaxPayer? TaxPayer { get; set; }
    }
}
