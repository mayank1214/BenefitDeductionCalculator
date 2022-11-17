using Paylocity.CodingExcercise.Core.Constants;
using Paylocity.CodingExcercise.Core.Interfaces;
using Paylocity.CodingExcercise.Data.Entities;
using Paylocity.CodingExcercise.Data.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paylocity.CodingExcercise.Services.Services
{
    public class RateService : IRateService
    {
        public decimal GetAnnualDeductionAmount(PersonType personType)
        {
            switch (personType)
            {
                case PersonType.Employee:
                    return DeductionConstant.EMPLOYEE_DEDUCTION_PER_YEAR;
                case PersonType.Spouse:
                case PersonType.Child:
                    return DeductionConstant.DEPENDENT_DEDUCTION_PER_YEAR;
                default: throw new ArgumentException();
            }
        }

        public decimal GetDiscountRate(Person person)
        {
            if (person?.Name?.ToUpper().StartsWith("A") ?? false)
                return DeductionConstant.NAME_STARTS_WITH_A_DEDUCTION_PER_YEAR;
            else
                return 0.0M;
        }
    }
}
