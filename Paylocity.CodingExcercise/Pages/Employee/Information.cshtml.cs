using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Paylocity.CodingExcercise.Core.Constants;
using Paylocity.CodingExcercise.Core.Enums;
using Paylocity.CodingExcercise.Core.Interfaces;
using Paylocity.CodingExcercise.Core.Models;
using Paylocity.CodingExcercise.ViewModels;
using Paylocity.CodingExcercise.Web.Extensions;
using Paylocity.CodingExcercise.Web.Helpers;
using Paylocity.CodingExcercise.Web.ViewModels;

namespace Paylocity.CodingExcercise.Web.Pages.Employee
{
    public class InformationModel : PageModel
    {
        private readonly IDeductionCalculationService _deductionCalculationService;
        [BindProperty]
        public EmployeeViewModel EmployeeViewModel { get; set; }

        public DeductionCalculationResult DeductionCalculationResult { get; set; }

        public List<SelectListItem> DependentTypes { get; } = new List<SelectListItem>();

        public InformationModel(IDeductionCalculationService deductionCalculationService)
        {
            _deductionCalculationService = deductionCalculationService;
        }

        public IActionResult OnGet(int? dependents)
        {
            if (dependents == null)
            {
                return RedirectToPage(PagesPath.INDEX);
            }
            EmployeeViewModel = CreateEmployeeViewModel(dependents.Value);
            PoulateDependentTypes();
            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                PoulateDependentTypes();
                return Page();
            }

            var persons = EmployeeObjectConverter.ConvertEmployeeToPersonList(EmployeeViewModel);

            // service layer call
            var result = _deductionCalculationService.CalculateDeductions(persons, EmployeeViewModel.NumberOfPayChecksPerYear, EmployeeViewModel.Salary);

            // set result to temp data to display on next page
            //TempData.Set("DeductionCalculationResult", result);


            //return RedirectToPage(PagesPath.RESULT);

            DeductionCalculationResult = result;
            return Page();
        }

        private void PoulateDependentTypes()
        {
            foreach (var value in Enum.GetValues(typeof(DependentType)))
            {
                DependentTypes.Add(new SelectListItem(value.ToString(), value.ToString()));
            }
        }

        private EmployeeViewModel CreateEmployeeViewModel(int dependents)
        {
            Func<int, List<DependentViewModel>> addDependents = (dependentsCount) =>
            {
                var dependents = new List<DependentViewModel>();
                for (int i = 0; i < dependentsCount; i++)
                {
                    dependents.Add(new DependentViewModel() { DependentType = (i == 0 ? DependentType.Spouse : DependentType.Child) });
                }
                return dependents;
            };


            return new EmployeeViewModel
            {
                Salary = EmployeeSalaryInformationConstant.EMPLOYEE_PAY_PER_CHECK * EmployeeSalaryInformationConstant.NUMBER_OF_PAY_CHEKCS_IN_A_YEAR,
                NumberOfPayChecksPerYear = EmployeeSalaryInformationConstant.NUMBER_OF_PAY_CHEKCS_IN_A_YEAR,
                Dependents = addDependents(dependents)
            };
        }
    }
}
