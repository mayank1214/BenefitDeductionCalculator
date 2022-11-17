using Moq;
using Paylocity.CodingExcercise.Core.Constants;
using Paylocity.CodingExcercise.Core.Interfaces;
using Paylocity.CodingExcercise.Data.Entities;
using Paylocity.CodingExcercise.Data.Enum;
using Paylocity.CodingExcercise.Services.Services;
using Paylocity.CodingExcercise.Tests.Extensions;
using Xunit;

namespace Paylocity.CodingExcercise.Tests.Services
{
    public class DeductionCalculationServiceTests
    {        
        [Fact]
        public void CalculateAnnualDiscount_With_OneDependent_And_Names_NotStarting_With_A()
        {
            var rateService = new Mock<IRateService>();
            var emplpoyee = new Person { Name = "John", Type = PersonType.Employee };
            var dependent = new Person { Name = "Jane", Type = PersonType.Spouse };
            rateService.SetupGetAnnualDeductionRate(PersonType.Employee, 1000);
            rateService.SetupGetAnnualDeductionRate(PersonType.Spouse, 500);
            rateService.SetupGetDiscountedRate(emplpoyee,0);
            rateService.SetupGetDiscountedRate(dependent,0);
            var persons = new List<Person>();
            persons.Add(emplpoyee);
            persons.Add(dependent);

            var deductionCalculationService = new DeductionCalculationService(rateService.Object);

            var result = deductionCalculationService.CalculateDeductionPerAnnum(persons);
            Assert.Equal(1500, result);
        }

        [Fact]
        public void CalculateAnnualDiscount_WithTwoDependents_And_Names_NotStarting_With_A()
        {
            var rateService = new Mock<IRateService>();
            var emplpoyee = new Person { Name = "John", Type = PersonType.Employee };
            var dependent = new Person { Name = "Jane", Type = PersonType.Spouse };
            var dependent1 = new Person { Name = "Jack", Type = PersonType.Child };

            rateService.SetupGetAnnualDeductionRate(PersonType.Employee, 1000);
            rateService.SetupGetAnnualDeductionRate(PersonType.Spouse, 500);
            rateService.SetupGetAnnualDeductionRate(PersonType.Child, 500);

            rateService.SetupGetDiscountedRate(emplpoyee, 0);
            rateService.SetupGetDiscountedRate(dependent, 0);
            rateService.SetupGetDiscountedRate(dependent1, 0);

            var persons = new List<Person>();
            persons.Add(emplpoyee);
            persons.Add(dependent);
            persons.Add(dependent1);

            var deductionCalculationService = new DeductionCalculationService(rateService.Object);

            var result = deductionCalculationService.CalculateDeductionPerAnnum(persons);
            Assert.Equal(2000, result);
        }

        [Fact]
        public void CalculateAnnualDiscount_WithTwoDependents_And_EmployeeName_Starting_With_A()
        {
            var rateService = new Mock<IRateService>();
            var emplpoyee = new Person { Name = "Alex", Type = PersonType.Employee };
            var dependent = new Person { Name = "Jane", Type = PersonType.Spouse };
            var dependent1 = new Person { Name = "Jack", Type = PersonType.Child };

            rateService.SetupGetAnnualDeductionRate(PersonType.Employee, 1000);
            rateService.SetupGetAnnualDeductionRate(PersonType.Spouse, 500);
            rateService.SetupGetAnnualDeductionRate(PersonType.Child, 500);

            rateService.SetupGetDiscountedRate(emplpoyee, 0.1M);
            rateService.SetupGetDiscountedRate(dependent, 0);
            rateService.SetupGetDiscountedRate(dependent1, 0);

            var persons = new List<Person>();
            persons.Add(emplpoyee);
            persons.Add(dependent);
            persons.Add(dependent1);

            var deductionCalculationService = new DeductionCalculationService(rateService.Object);

            var result = deductionCalculationService.CalculateDeductionPerAnnum(persons);
            Assert.Equal(1900, result); // 1000 * .9 + 500 + 500
        }

        [Fact]
        public void CalculateAnnualDiscount_WithTwoDependents_All_Names_Starting_With_A()
        {
            var rateService = new Mock<IRateService>();
            var emplpoyee = new Person { Name = "Alex", Type = PersonType.Employee };
            var dependent = new Person { Name = "Alexa", Type = PersonType.Spouse };
            var dependent1 = new Person { Name = "Alicia", Type = PersonType.Child };

            rateService.SetupGetAnnualDeductionRate(PersonType.Employee, 1000);
            rateService.SetupGetAnnualDeductionRate(PersonType.Spouse, 500);
            rateService.SetupGetAnnualDeductionRate(PersonType.Child, 500);

            rateService.SetupGetDiscountedRate(emplpoyee, 0.1M);
            rateService.SetupGetDiscountedRate(dependent, 0.1M);
            rateService.SetupGetDiscountedRate(dependent1, 0.1M);

            var persons = new List<Person>();
            persons.Add(emplpoyee);
            persons.Add(dependent);
            persons.Add(dependent1);

            var deductionCalculationService = new DeductionCalculationService(rateService.Object);

            var result = deductionCalculationService.CalculateDeductionPerAnnum(persons);
            Assert.Equal(1800, result); // (1000 * 0.9) + (500 * 0.9) + (500 * 0.9)
        }


        [Fact]
        public void CalculateDeductionPerPayCheck_With_OneDependent_And_Names_NotStarting_With_A()
        {
            var rateService = new Mock<IRateService>();
            var emplpoyee = new Person { Name = "John", Type = PersonType.Employee };
            var dependent = new Person { Name = "Jane", Type = PersonType.Spouse };
            rateService.SetupGetAnnualDeductionRate(PersonType.Employee, 1000);
            rateService.SetupGetAnnualDeductionRate(PersonType.Spouse, 500);
            rateService.SetupGetDiscountedRate(emplpoyee, 0);
            rateService.SetupGetDiscountedRate(dependent, 0);
            var persons = new List<Person>();
            persons.Add(emplpoyee);
            persons.Add(dependent);

            var deductionCalculationService = new DeductionCalculationService(rateService.Object);

            var result = deductionCalculationService.CalculateDeductionPerPaycheck(persons, EmployeeSalaryInformationConstant.NUMBER_OF_PAY_CHEKCS_IN_A_YEAR);
            Assert.Equal(57.69M, Math.Round(result,2));
        }

        [Fact]
        public void CalculateDeductionPerPayCheck_With_TwoDependents_And_Names_NotStarting_With_A()
        {
            var rateService = new Mock<IRateService>();
            var emplpoyee = new Person { Name = "John", Type = PersonType.Employee };
            var dependent = new Person { Name = "Jane", Type = PersonType.Spouse };
            var dependent1 = new Person { Name = "Jack", Type = PersonType.Child };

            rateService.SetupGetAnnualDeductionRate(PersonType.Employee, 1000);
            rateService.SetupGetAnnualDeductionRate(PersonType.Spouse, 500);
            rateService.SetupGetAnnualDeductionRate(PersonType.Child, 500);

            rateService.SetupGetDiscountedRate(emplpoyee, 0);
            rateService.SetupGetDiscountedRate(dependent, 0);
            rateService.SetupGetDiscountedRate(dependent1, 0);

            var persons = new List<Person>();
            persons.Add(emplpoyee);
            persons.Add(dependent);
            persons.Add(dependent1);

            var deductionCalculationService = new DeductionCalculationService(rateService.Object);

            var result = deductionCalculationService.CalculateDeductionPerPaycheck(persons, EmployeeSalaryInformationConstant.NUMBER_OF_PAY_CHEKCS_IN_A_YEAR);
            Assert.Equal(76.92M, Math.Round(result, 2));
        }


    }
}
