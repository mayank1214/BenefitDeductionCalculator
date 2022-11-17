using Moq;
using Paylocity.CodingExcercise.Core.Interfaces;
using Paylocity.CodingExcercise.Data.Entities;
using Paylocity.CodingExcercise.Data.Enum;

namespace Paylocity.CodingExcercise.Tests.Extensions
{
    internal static class IRateServiceMockupExtensions
    {
        internal static Mock<IRateService> SetupGetAnnualDeductionRate(this Mock<IRateService> mock,
            PersonType personType, decimal result)
        {
            mock.Setup(x => x.GetAnnualDeductionAmount(personType)).Returns(result);
            return mock;
        }

        internal static Mock<IRateService> SetupGetDiscountedRate(this Mock<IRateService> mock, Person person, decimal result)
        {
            mock.Setup(x => x.GetDiscountRate(person)).Returns(result);
            return mock;
        }
    }
}
