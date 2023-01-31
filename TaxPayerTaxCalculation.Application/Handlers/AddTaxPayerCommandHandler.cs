using MediatR;
using TaxPayerTaxCalculation.Application.Commands;
using TaxPayerTaxCalculation.Application.Repositories;
using TaxPayerTaxCalculation.Domain.Entities;

namespace TaxPayerTaxCalculation.Application.Handlers
{
    public class AddTaxPayerCommandHandler : IRequestHandler<AddTaxPayerCommand, TaxPayer>
    {
        private readonly ITaxPayerRepository _taxPayerRepository;

        public AddTaxPayerCommandHandler(ITaxPayerRepository taxPayerRepository)
        {
            _taxPayerRepository = taxPayerRepository;
        }

        public async Task<TaxPayer> Handle(AddTaxPayerCommand request, CancellationToken cancellationToken)
        {
            await _taxPayerRepository.AddTaxPayerAsync(request.TaxPayer);
            return request.TaxPayer;
        }
    }
}
