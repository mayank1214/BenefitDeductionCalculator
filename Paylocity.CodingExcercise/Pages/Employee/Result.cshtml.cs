using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Paylocity.CodingExcercise.Core.Constants;
using Paylocity.CodingExcercise.Core.Models;
using Paylocity.CodingExcercise.Web.Extensions;

namespace Paylocity.CodingExcercise.Web.Pages.Employee
{
    public class ResultModel : PageModel
    {
        public DeductionCalculationResult DeductionCalcuationResult { get; set; }


        public IActionResult OnGet()
        {
            DeductionCalcuationResult = TempData.Get<DeductionCalculationResult>("DeductionCalculationResult");

            if (DeductionCalcuationResult == null)
            {
                return RedirectToPage(PagesPath.INDEX);
            }

            TempData.Set("DeductionCalculationResult", DeductionCalcuationResult);

            return Page();
        }
    }
}
