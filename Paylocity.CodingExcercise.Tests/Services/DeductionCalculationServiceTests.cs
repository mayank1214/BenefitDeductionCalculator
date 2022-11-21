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
        private readonly Mock<IRateService> _rateService;
        public DeductionCalculationServiceTests()
        {
            _rateService = new Mock<IRateService>();
        }

        [Fact]
        public void CalculateDeductionPerAnnum_With_OneDependent_And_Names_NotStarting_With_A()
        {
            var emplpoyee = new Person { Name = "John", Type = PersonType.Employee };
            var dependent = new Person { Name = "Jane", Type = PersonType.Spouse };
            _rateService.SetupGetAnnualDeductionRate(PersonType.Employee, 1000);
            _rateService.SetupGetAnnualDeductionRate(PersonType.Spouse, 500);
            _rateService.SetupGetDiscountedRate(emplpoyee,0);
            _rateService.SetupGetDiscountedRate(dependent,0);
            var persons = new List<Person>();
            persons.Add(emplpoyee);
            persons.Add(dependent);

            var deductionCalculationService = new DeductionCalculationService(_rateService.Object);

            var result = deductionCalculationService.CalculateDeductionPerAnnum(persons);
            Assert.Equal(1500, result);
        }

        [Fact]
        public void CalculateDeductionPerAnnum_WithTwoDependents_And_Names_NotStarting_With_A()
        {
            var emplpoyee = new Person { Name = "John", Type = PersonType.Employee };
            var dependent = new Person { Name = "Jane", Type = PersonType.Spouse };
            var dependent1 = new Person { Name = "Jack", Type = PersonType.Child };

            _rateService.SetupGetAnnualDeductionRate(PersonType.Employee, 1000);
            _rateService.SetupGetAnnualDeductionRate(PersonType.Spouse, 500);
            _rateService.SetupGetAnnualDeductionRate(PersonType.Child, 500);

            _rateService.SetupGetDiscountedRate(emplpoyee, 0);
            _rateService.SetupGetDiscountedRate(dependent, 0);
            _rateService.SetupGetDiscountedRate(dependent1, 0);

            var persons = new List<Person>();
            persons.Add(emplpoyee);
            persons.Add(dependent);
            persons.Add(dependent1);

            var deductionCalculationService = new DeductionCalculationService(_rateService.Object);

            var result = deductionCalculationService.CalculateDeductionPerAnnum(persons);
            Assert.Equal(2000, result);
        }

        [Fact]
        public void CalculateDeductionPerAnnum_WithTwoDependents_And_EmployeeName_Starting_With_A()
        {
            var emplpoyee = new Person { Name = "Alex", Type = PersonType.Employee };
            var dependent = new Person { Name = "Jane", Type = PersonType.Spouse };
            var dependent1 = new Person { Name = "Jack", Type = PersonType.Child };

            _rateService.SetupGetAnnualDeductionRate(PersonType.Employee, 1000);
            _rateService.SetupGetAnnualDeductionRate(PersonType.Spouse, 500);
            _rateService.SetupGetAnnualDeductionRate(PersonType.Child, 500);

            _rateService.SetupGetDiscountedRate(emplpoyee, 0.1M);
            _rateService.SetupGetDiscountedRate(dependent, 0);
            _rateService.SetupGetDiscountedRate(dependent1, 0);

            var persons = new List<Person>();
            persons.Add(emplpoyee);
            persons.Add(dependent);
            persons.Add(dependent1);

            var deductionCalculationService = new DeductionCalculationService(_rateService.Object);

            var result = deductionCalculationService.CalculateDeductionPerAnnum(persons);
            Assert.Equal(1900, result);
        }

        [Fact]
        public void CalculateDeductionPerAnnum_WithTwoDependents_All_Names_Starting_With_A()
        {
            var emplpoyee = new Person { Name = "Alex", Type = PersonType.Employee };
            var dependent = new Person { Name = "Alexa", Type = PersonType.Spouse };
            var dependent1 = new Person { Name = "Alicia", Type = PersonType.Child };

            _rateService.SetupGetAnnualDeductionRate(PersonType.Employee, 1000);
            _rateService.SetupGetAnnualDeductionRate(PersonType.Spouse, 500);
            _rateService.SetupGetAnnualDeductionRate(PersonType.Child, 500);

            _rateService.SetupGetDiscountedRate(emplpoyee, 0.1M);
            _rateService.SetupGetDiscountedRate(dependent, 0.1M);
            _rateService.SetupGetDiscountedRate(dependent1, 0.1M);

            var persons = new List<Person>();
            persons.Add(emplpoyee);
            persons.Add(dependent);
            persons.Add(dependent1);

            var deductionCalculationService = new DeductionCalculationService(_rateService.Object);

            var result = deductionCalculationService.CalculateDeductionPerAnnum(persons);
            Assert.Equal(1800, result); // (1000 * 0.9) + (500 * 0.9) + (500 * 0.9)
        }


        [Fact]
        public void CalculateDeductionPerPayCheck_With_OneDependent_And_Names_NotStarting_With_A()
        {
            var emplpoyee = new Person { Name = "John", Type = PersonType.Employee };
            var dependent = new Person { Name = "Jane", Type = PersonType.Spouse };
            _rateService.SetupGetAnnualDeductionRate(PersonType.Employee, 1000);
            _rateService.SetupGetAnnualDeductionRate(PersonType.Spouse, 500);
            _rateService.SetupGetDiscountedRate(emplpoyee, 0);
            _rateService.SetupGetDiscountedRate(dependent, 0);
            var persons = new List<Person>();
            persons.Add(emplpoyee);
            persons.Add(dependent);

            var deductionCalculationService = new DeductionCalculationService(_rateService.Object);

            var result = deductionCalculationService.CalculateDeductionPerPaycheck(persons, EmployeeSalaryInformationConstant.NUMBER_OF_PAY_CHEKCS_IN_A_YEAR);
            Assert.Equal(57.69M, Math.Round(result,2));
        }

        [Fact]
        public void CalculateDeductionPerPayCheck_With_TwoDependents_And_Names_NotStarting_With_A()
        {
            var emplpoyee = new Person { Name = "John", Type = PersonType.Employee };
            var dependent = new Person { Name = "Jane", Type = PersonType.Spouse };
            var dependent1 = new Person { Name = "Jack", Type = PersonType.Child };

            _rateService.SetupGetAnnualDeductionRate(PersonType.Employee, 1000);
            _rateService.SetupGetAnnualDeductionRate(PersonType.Spouse, 500);
            _rateService.SetupGetAnnualDeductionRate(PersonType.Child, 500);

            _rateService.SetupGetDiscountedRate(emplpoyee, 0);
            _rateService.SetupGetDiscountedRate(dependent, 0);
            _rateService.SetupGetDiscountedRate(dependent1, 0);

            var persons = new List<Person>();
            persons.Add(emplpoyee);
            persons.Add(dependent);
            persons.Add(dependent1);

            var deductionCalculationService = new DeductionCalculationService(_rateService.Object);

            var result = deductionCalculationService.CalculateDeductionPerPaycheck(persons, EmployeeSalaryInformationConstant.NUMBER_OF_PAY_CHEKCS_IN_A_YEAR);
            Assert.Equal(76.92M, Math.Round(result, 2));
        }

        [Fact]
        public void CalculateDeductionPerPayCheck_WithTwoDependents_And_EmployeeName_Starting_With_A()
        {
            var emplpoyee = new Person { Name = "Ant", Type = PersonType.Employee };
            var dependent = new Person { Name = "Jane", Type = PersonType.Spouse };
            var dependent1 = new Person { Name = "Jack", Type = PersonType.Child };

            _rateService.SetupGetAnnualDeductionRate(PersonType.Employee, 1000);
            _rateService.SetupGetAnnualDeductionRate(PersonType.Spouse, 500);
            _rateService.SetupGetAnnualDeductionRate(PersonType.Child, 500);

            _rateService.SetupGetDiscountedRate(emplpoyee, 0.1M);
            _rateService.SetupGetDiscountedRate(dependent, 0);
            _rateService.SetupGetDiscountedRate(dependent1, 0);

            var persons = new List<Person>();
            persons.Add(emplpoyee);
            persons.Add(dependent);
            persons.Add(dependent1);

            var deductionCalculationService = new DeductionCalculationService(_rateService.Object);

            var result = deductionCalculationService.CalculateDeductionPerPaycheck(persons, EmployeeSalaryInformationConstant.NUMBER_OF_PAY_CHEKCS_IN_A_YEAR);
            Assert.Equal(73.08M, Math.Round(result, 2));
        }

        [Fact]
        public void CalculateDeductionPerPayCheck_WithTwoDependents_All_Names_Starting_With_A()
        {
            var emplpoyee = new Person { Name = "Alex", Type = PersonType.Employee };
            var dependent = new Person { Name = "Alexa", Type = PersonType.Spouse };
            var dependent1 = new Person { Name = "Alicia", Type = PersonType.Child };

            _rateService.SetupGetAnnualDeductionRate(PersonType.Employee, 1000);
            _rateService.SetupGetAnnualDeductionRate(PersonType.Spouse, 500);
            _rateService.SetupGetAnnualDeductionRate(PersonType.Child, 500);

            _rateService.SetupGetDiscountedRate(emplpoyee, 0.1M);
            _rateService.SetupGetDiscountedRate(dependent, 0.1M);
            _rateService.SetupGetDiscountedRate(dependent1, 0.1M);

            var persons = new List<Person>();
            persons.Add(emplpoyee);
            persons.Add(dependent);
            persons.Add(dependent1);

            var deductionCalculationService = new DeductionCalculationService(_rateService.Object);

            var result = deductionCalculationService.CalculateDeductionPerPaycheck(persons, EmployeeSalaryInformationConstant.NUMBER_OF_PAY_CHEKCS_IN_A_YEAR);
            Assert.Equal(69.23M, Math.Round(result, 2));
        }
        
        [Fact]
        public void CalculateDeductions_With_Employee_And_Dependents_Name_Starting_With_A()
        {
            var emplpoyee = new Person { Name = "Alex", Type = PersonType.Employee };
            var dependent = new Person { Name = "Alexa", Type = PersonType.Spouse };
            var dependent1 = new Person { Name = "Alicia", Type = PersonType.Child };

            _rateService.SetupGetAnnualDeductionRate(PersonType.Employee, 1000);
            _rateService.SetupGetAnnualDeductionRate(PersonType.Spouse, 500);
            _rateService.SetupGetAnnualDeductionRate(PersonType.Child, 500);

            _rateService.SetupGetDiscountedRate(emplpoyee, 0.1M);
            _rateService.SetupGetDiscountedRate(dependent, 0.1M);
            _rateService.SetupGetDiscountedRate(dependent1, 0.1M);

            var persons = new List<Person>();
            persons.Add(emplpoyee);
            persons.Add(dependent);
            persons.Add(dependent1);

            var deductionCalculationService = new DeductionCalculationService(_rateService.Object);

            int numberOfPayCheckPerYear = EmployeeSalaryInformationConstant.NUMBER_OF_PAY_CHEKCS_IN_A_YEAR;
            int salary = EmployeeSalaryInformationConstant.EMPLOYEE_PAY_PER_CHECK * numberOfPayCheckPerYear;

            var result = deductionCalculationService.CalculateDeductions(persons, numberOfPayCheckPerYear, salary);

            Assert.NotNull(result);

            decimal employeeDeductionPerAnnum = (DeductionConstant.EMPLOYEE_DEDUCTION_PER_YEAR * (1 - DeductionConstant.NAME_STARTS_WITH_A_DEDUCTION_PER_YEAR));
            Assert.Equal(employeeDeductionPerAnnum, result.EmployeeDeductionPerYear);

            decimal employeeDeductionPerPayCheck = employeeDeductionPerAnnum / EmployeeSalaryInformationConstant.NUMBER_OF_PAY_CHEKCS_IN_A_YEAR;
            Assert.Equal(Math.Round(employeeDeductionPerPayCheck,2), Math.Round(result.EmployeeDeductionPerPayCheck,2));

            var dependents = persons.Count(p => p.Type != PersonType.Employee);
            decimal dependentsDeductionPerAnnum = ((DeductionConstant.DEPENDENT_DEDUCTION_PER_YEAR * (1 - DeductionConstant.NAME_STARTS_WITH_A_DEDUCTION_PER_YEAR)) * dependents);
            Assert.Equal(dependentsDeductionPerAnnum, result.DependentsDeductionPerYear);

            decimal dependentsDeductionPerPayCheck = (dependentsDeductionPerAnnum / EmployeeSalaryInformationConstant.NUMBER_OF_PAY_CHEKCS_IN_A_YEAR);
            Assert.Equal(Math.Round(dependentsDeductionPerPayCheck,2), Math.Round(result.DependentsDeductionPerPayCheck,2));

            Assert.Equal(Math.Round(employeeDeductionPerPayCheck + dependentsDeductionPerPayCheck,2), Math.Round(result.TotalDeductionPerPayCheck,2));
            Assert.Equal(Math.Round(employeeDeductionPerAnnum + dependentsDeductionPerAnnum, 2), result.TotalDeductionPerYear);
        }    
    }
}
