using Paylocity.CodingExcercise.Core.Models;
using Paylocity.CodingExcercise.Core.Interfaces;
using Paylocity.CodingExcercise.Data.Entities;
using Paylocity.CodingExcercise.Data.Enum;
using Paylocity.CodingExcercise.Core.Constants;

namespace Paylocity.CodingExcercise.Services.Services
{
    public class DeductionCalculationService : IDeductionCalculationService
    {
        private readonly IRateService _rateService;
        public DeductionCalculationService(IRateService rateService)
        {
            _rateService = rateService;
        }

        public decimal CalculateDeductionPerAnnum(List<Person> persons)
        {
            return persons.Sum(person => CalculateDeductionWithDiscount(person));
        }

        public decimal CalculateDeductionPerPaycheck(List<Person> persons, int numberOfPaychecksPerYear)
        {
            return CalculateDeductionPerAnnum(persons) / numberOfPaychecksPerYear;
        }

        private decimal CalculateDeductionWithDiscount(Person person)
        {
            var annualDeductionAmount = _rateService.GetAnnualDeductionAmount(person.Type);
            var discountedRate = _rateService.GetDiscountRate(person);
            return annualDeductionAmount * (1 - discountedRate);
        }

        public DeductionCalculationResult CalculateDeductions(List<Person> persons, int NumberOfPayChecksPerYear, int Salary)
        {
            decimal employeeDedudctionPerPayCheck = CalculateDeductionPerPaycheck(persons.Where(p => p.Type == PersonType.Employee).ToList(), NumberOfPayChecksPerYear);
            decimal dependentsDeductionPerPayCheck = CalculateDeductionPerPaycheck(persons.Where(p => p.Type != PersonType.Employee).ToList(), NumberOfPayChecksPerYear);
            decimal totalDeductionPerPayCheck = CalculateDeductionPerPaycheck(persons, NumberOfPayChecksPerYear);
            decimal employeeDeductionPerYear = CalculateDeductionPerAnnum(persons.Where(p => p.Type == PersonType.Employee).ToList());
            decimal dependentDeductionPerYear = CalculateDeductionPerAnnum(persons.Where(p => p.Type != PersonType.Employee).ToList());
            decimal totalDeductionPerYear = CalculateDeductionPerAnnum(persons);
            decimal employeePaycheckAfterDeductions = (Salary / (decimal)NumberOfPayChecksPerYear) - totalDeductionPerPayCheck;
            decimal employeeYearlyPayAfterDeductions = Salary - totalDeductionPerYear;

            return new DeductionCalculationResult()
            {
                EmployeeDeductionPerPayCheck = employeeDedudctionPerPayCheck,
                DependentsDeductionPerPayCheck = dependentsDeductionPerPayCheck,
                TotalDeductionPerPayCheck = totalDeductionPerPayCheck,
                EmployeeDeductionPerYear = employeeDeductionPerYear,
                DependentsDeductionPerYear = dependentDeductionPerYear,
                TotalDeductionPerYear = totalDeductionPerYear,
                EmployeePaycheckAfterDeductions = employeePaycheckAfterDeductions,
                EmployeeYearlyPayAfterDeductions = employeeYearlyPayAfterDeductions
            };
        }
    }
}
