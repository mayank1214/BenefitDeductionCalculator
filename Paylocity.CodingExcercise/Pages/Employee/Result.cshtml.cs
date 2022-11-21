using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Paylocity.CodingExcercise.Core.Constants;
using Paylocity.CodingExcercise.Core.Models;
using Paylocity.CodingExcercise.Web.Extensions;

namespace Paylocity.CodingExcercise.Web.Pages.Employee
{
    public class ResultModel : PageModel
    {
        public DeductionCalculationResult DeductionCalculationResult { get; set; }


        public IActionResult OnGet()
        {
            DeductionCalculationResult = TempData.Get<DeductionCalculationResult>("DeductionCalculationResult");

            if (DeductionCalculationResult == null)
            {
                return RedirectToPage(PagesPath.INDEX);
            }

            TempData.Set("DeductionCalculationResult", DeductionCalculationResult);

            return Page();
        }
    }
}
