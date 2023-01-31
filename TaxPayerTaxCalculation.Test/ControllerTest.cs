using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TaxPayerTaxCalculation.Api.Controllers;
using TaxPayerTaxCalculation.Application.Commands;
using TaxPayerTaxCalculation.Domain.Entities;

namespace TaxPayerTaxCalculation.Test
{
    public class ControllerTest
    {
        [Fact]
        public async Task Calculate_ReturnsCorrectTaxes_WhenValidTaxPayerIsProvided()
        {
            // Arrange
            var taxPayer = new TaxPayer
            {
                FullName = "George Smith",
                SSN = "123456789",
                GrossIncome = 2500.0m,
                CharitySpent = 150.0m,
                DateOfBirth = DateTime.UtcNow
            };
            var taxes = new Taxes { SSN = "123456789", GrossIncome = 2500, IncomeTax = 130, SocialTax = 202.20m, NetIncome = 2162.50m };
            var mediatorMock = new Mock<IMediator>();
            mediatorMock.Setup(x => x.Send(It.IsAny<CalculateTaxCommand>(), default)).ReturnsAsync(taxes);
            var controller = new CalculatorController(mediatorMock.Object);

            // Act
            var result = await controller.Calculate(taxPayer);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedTaxes = Assert.IsType<Taxes>(okResult.Value);
            Assert.Equal(taxes, returnedTaxes);
        }

        [Fact]
        public async Task Calculate_ReturnsCorrectSSN()
        {
            // Arrange
            var taxPayer = new TaxPayer { SSN = "123456789", GrossIncome = 2500 };
            var taxes = new Taxes { SSN = "123456789", GrossIncome = 2500, IncomeTax = 130, SocialTax = 202.20m, NetIncome = 2162.50m };
            var expectedSSN = taxPayer.SSN;
            var mediator = new Mock<IMediator>();
            mediator.Setup(x => x.Send(It.IsAny<CalculateTaxCommand>(), default)).ReturnsAsync(taxes);

            var controller = new CalculatorController(mediator.Object);

            // Act
            var result = await controller.Calculate(taxPayer);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedTaxes = Assert.IsAssignableFrom<Taxes>(okResult.Value);
            Assert.Equal(expectedSSN, returnedTaxes.SSN);
        }
    }
}
