using Paylocity.CodingExcercise.Core.Models;
using Paylocity.CodingExcercise.Data.Entities;

namespace Paylocity.CodingExcercise.Core.Interfaces
{
    public interface IDeductionCalculationService
    {
        /// <summary>
        /// Return benefit deduction amount based per paycheck
        /// </summary>
        /// <param name="persons"></param>
        /// <param name="numberOfPaychecksPerYear"></param>
        /// <returns></returns>
        decimal CalculateDeductionPerPaycheck(List<Person> persons, int numberOfPaychecksPerYear);

        /// <summary>
        ///  Return benefit deduction amount yearly
        /// </summary>
        /// <param name="persons"></param>
        /// <returns></returns>
        decimal CalculateDeductionPerAnnum(List<Person> persons);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        DeductionCalculationResult CalculateDeductions(List<Person> persons, int NumberOfPayChecksPerYear, int Salary);

    }
}
