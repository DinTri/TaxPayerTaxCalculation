using Microsoft.Extensions.Options;
using Moq;
using TaxPayerTaxCalculation.Application.Repositories;
using TaxPayerTaxCalculation.Application.Services;
using TaxPayerTaxCalculation.Application.Settings;
using TaxPayerTaxCalculation.Domain.Entities;

namespace TaxPayerTaxCalculation.Test
{
    public class TaxCalculatorTests
    {
        private readonly Mock<IOptions<CalculationSettings>> _mockAppSettings;
        private readonly Mock<ITaxPayerRepository> _mockTaxPayerRepository;
        private readonly TaxPayerService _taxPayerService;
        public TaxCalculatorTests()
        {
            _mockAppSettings = new Mock<IOptions<CalculationSettings>>();
            _mockTaxPayerRepository = new Mock<ITaxPayerRepository>();
            var calculationSettings = new CalculationSettings { IncomeTaxRate = 0.1m, SocialContributiomRate = 0.15m };
            _mockAppSettings.Setup(x => x.Value).Returns(calculationSettings);

            _taxPayerService = new TaxPayerService(_mockAppSettings.Object, _mockTaxPayerRepository.Object);
        }

        [Fact]
        public void CalculateTaxes_GrossIncomeLowerThan1000_ReturnsNetIncome()
        {
            // Arrange
            var mockTaxPayerRepository = new Mock<ITaxPayerRepository>();
            mockTaxPayerRepository.Setup(x => x.AddTaxesAsync(It.IsAny<string>(), It.IsAny<Taxes>()));

            var taxPayer = new TaxPayer
            {
                FullName = "George Smith",
                SSN = "123456789",
                GrossIncome = 980.0m,
                CharitySpent = 0.0m,
                DateOfBirth = DateTime.UtcNow,
            };
            var taxCalculator = _taxPayerService.CalculateTaxesAsync(taxPayer);

            // Act
            var result = taxCalculator.Result;

            // Assert
            Assert.Equal(0, result.IncomeTax);
            Assert.Equal(0, result.SocialTax);
            Assert.Equal(0, result.CharitySpent);
            Assert.Equal(980.0m, result.NetIncome);
        }

        [Fact]
        public void CalculateTaxes_GrossIncomeAbove1000_ReturnsNetIncomeAfterTaxes()
        {
            // Arrange
            var mockTaxPayerRepository = new Mock<ITaxPayerRepository>();
            mockTaxPayerRepository.Setup(x => x.AddTaxesAsync(It.IsAny<string>(), It.IsAny<Taxes>()));

            var taxPayer = new TaxPayer
            {
                FullName = "Irina Blonde",
                SSN = "523456789",
                GrossIncome = 3400,
                CharitySpent = 0,
                DateOfBirth = DateTime.UtcNow,
            };

            var taxCalculator = _taxPayerService.CalculateTaxesAsync(taxPayer);

            // Act
            var result = taxCalculator.Result;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(240.0m, result.IncomeTax);
            Assert.Equal(360.0m, result.SocialTax);
            Assert.Equal(2800.0m, result.NetIncome);
        }

        [Fact]
        public void CalculateTaxes_GrossIncomeAbove1000WithCharitySpent_ReturnsNetIncomeAfterTaxes()
        {
            // Arrange
            var mockTaxPayerRepository = new Mock<ITaxPayerRepository>();
            mockTaxPayerRepository.Setup(x => x.AddTaxesAsync(It.IsAny<string>(), It.IsAny<Taxes>()));

            var taxPayer = new TaxPayer
            {
                FullName = "Mick Brown",
                SSN = "323456789",
                GrossIncome = 2500,
                CharitySpent = 150,
                DateOfBirth = DateTime.UtcNow,
            };

            var taxCalculator = _taxPayerService.CalculateTaxesAsync(taxPayer);

            // Act
            var result = taxCalculator.Result;

            Assert.Equal(135.0m, result.IncomeTax);
            Assert.Equal(202.50m, result.SocialTax);
            Assert.Equal(2162.50m, result.NetIncome);
        }

        [Fact]
        public void CalculateTaxes_GrossIncomeAbove3000WithCharitySpent_ReturnsNetIncomeAfterTaxes()
        {
            // Arrange
            var mockTaxPayerRepository = new Mock<ITaxPayerRepository>();
            mockTaxPayerRepository.Setup(x => x.AddTaxesAsync(It.IsAny<string>(), It.IsAny<Taxes>()));

            var taxPayer = new TaxPayer
            {
                FullName = "Bill Clinton",
                SSN = "723456789",
                GrossIncome = 3600.0m,
                CharitySpent = 520,
                DateOfBirth = DateTime.UtcNow,
            };

            var taxCalculator = _taxPayerService.CalculateTaxesAsync(taxPayer);

            // Act
            var result = taxCalculator.Result;

            // Assert
            Assert.Equal(208, result.IncomeTax);
            Assert.Equal(312, result.SocialTax);
            Assert.Equal(3080.0m, result.NetIncome);
        }

    }
}