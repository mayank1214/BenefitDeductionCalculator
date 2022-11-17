using Paylocity.CodingExcercise.Data.Entities;
using Paylocity.CodingExcercise.Data.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paylocity.CodingExcercise.Core.Interfaces
{
    public interface IRateService
    {
        decimal GetDiscountRate(Person person);

        decimal GetAnnualDeductionAmount(PersonType personType);
    }
}
