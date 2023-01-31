using MediatR;
using Microsoft.AspNetCore.Mvc;
using TaxPayerTaxCalculation.Application.Commands;
using TaxPayerTaxCalculation.Domain.Entities;

namespace TaxPayerTaxCalculation.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalculatorController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CalculatorController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("calculate")]
        public async Task<ActionResult<Taxes>> Calculate(TaxPayer? taxPayer)
        {
            var calculateTaxesCommand = new CalculateTaxCommand(taxPayer);
            var taxes = await _mediator.Send(calculateTaxesCommand);
            return Ok(taxes);
        }
    }
}
