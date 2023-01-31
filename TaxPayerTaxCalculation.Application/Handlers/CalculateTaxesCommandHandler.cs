using MediatR;
using TaxPayerTaxCalculation.Application.Commands;
using TaxPayerTaxCalculation.Application.Repositories;
using TaxPayerTaxCalculation.Application.Services;
using TaxPayerTaxCalculation.Domain.Entities;

namespace TaxPayerTaxCalculation.Application.Handlers
{
    public class CalculateTaxesCommandHandler : IRequestHandler<CalculateTaxCommand, Taxes>
    {
        private readonly ITaxPayerService _taxPayerService;

        private readonly ITaxPayerRepository _taxPayerRepository;

        public CalculateTaxesCommandHandler(ITaxPayerService taxPayerService, ITaxPayerRepository taxPayerRepository)
        {
            _taxPayerService = taxPayerService;
            _taxPayerRepository = taxPayerRepository;
        }

        public async Task<Taxes> Handle(CalculateTaxCommand request, CancellationToken cancellationToken)
        {
            var taxes = await _taxPayerService.CalculateTaxesAsync(request.TaxPayer);
            return taxes;
        }
    }
}
