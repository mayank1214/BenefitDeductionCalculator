using Moq;
using Paylocity.CodingExcercise.Core.Constants;
using Paylocity.CodingExcercise.Core.Interfaces;
using Paylocity.CodingExcercise.Data.Entities;
using Paylocity.CodingExcercise.Data.Enum;
using Paylocity.CodingExcercise.Tests.Extensions;
using Xunit;

namespace Paylocity.CodingExcercise.Tests.Services
{
    public class RateServiceTests
    {
        [Fact]
        public void ValidateEmployeeDeducationRate_ReturnOneThousand()
        {
            var rateService = new Mock<IRateService>();
            rateService.SetupGetAnnualDeductionRate(PersonType.Employee, DeductionConstant.EMPLOYEE_DEDUCTION_PER_YEAR);
            Assert.Equal(1000, rateService.Object.GetAnnualDeductionAmount(PersonType.Employee));
        }

        [Theory]
        [InlineData(PersonType.Spouse)]
        [InlineData(PersonType.Child)]
        public void ValidateDependentDeducationRate_ReturnFiveHundred(PersonType personType)
        {
            var rateService = new Mock<IRateService>();
            rateService.SetupGetAnnualDeductionRate(personType, DeductionConstant.DEPENDENT_DEDUCTION_PER_YEAR);
            Assert.Equal(500, rateService.Object.GetAnnualDeductionAmount(personType));
        }

        [Theory]
        [InlineData(PersonType.Employee)]
        [InlineData(PersonType.Spouse)]
        [InlineData(PersonType.Child)]
        public void ValidateDiscountedRate_When_Name_Starts_With_a(PersonType personType)
        {
            var rateService = new Mock<IRateService>();
            var person = new Person
            {
                Name = "aaa",
                Type = personType
            };
            rateService.SetupGetDiscountedRate(person, DeductionConstant.NAME_STARTS_WITH_A_DEDUCTION_PER_YEAR);
            Assert.Equal(0.10M, rateService.Object.GetDiscountRate(person));
        }


        [Fact]
        public void ValidateDiscountedRate_When_Name_Not_Starts_With_a()
        {
            var rateService = new Mock<IRateService>();
            var person = new Person
            {
                Name = "bcd",
                Type = PersonType.Employee
            };
            rateService.SetupGetDiscountedRate(person, 0.0M);
            Assert.Equal(0.0M, rateService.Object.GetDiscountRate(person));
        }
    }


}
