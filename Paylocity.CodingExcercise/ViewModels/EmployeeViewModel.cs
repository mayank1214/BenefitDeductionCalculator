using Paylocity.CodingExcercise.Web.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace Paylocity.CodingExcercise.ViewModels
{
    public class EmployeeViewModel : PersonViewModel
    {
        public int Salary { get; set; }

        public int NumberOfPayChecksPerYear { get; set; }

        public List<DependentViewModel> Dependents { get; set; }
    }
}
