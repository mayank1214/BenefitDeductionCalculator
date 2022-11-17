using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paylocity.CodingExcercise.Core.Models
{
    public class DeductionCalculationResult
    {
        [DisplayFormat(DataFormatString = "{0:C2}")]
        public decimal TotalDeductionPerPayCheck { get; set; }
        [DisplayFormat(DataFormatString = "{0:C2}")]
        public decimal DependentsDeductionPerPayCheck { get; set; }
        [DisplayFormat(DataFormatString = "{0:C2}")]
        public decimal EmployeeDeductionPerPayCheck { get; set; }
        [DisplayFormat(DataFormatString = "{0:C2}")]
        public decimal EmployeeDeductionPerYear { get; set; }
        [DisplayFormat(DataFormatString = "{0:C2}")]
        public decimal DependentsDeductionPerYear { get; set; }
        [DisplayFormat(DataFormatString = "{0:C2}")]
        public decimal TotalDeductionPerYear { get; set; }
        [DisplayFormat(DataFormatString = "{0:C2}")]
        public decimal EmployeePaycheckAfterDeductions { get; set; }
        [DisplayFormat(DataFormatString = "{0:C2}")]
        public decimal EmployeeYearlyPayAfterDeductions { get; set; }
    }
}
