using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Paylocity.CodingExcercise.Core.Constants;
using System.ComponentModel.DataAnnotations;

namespace Paylocity.CodingExcercise.Web.Pages.Employee
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        [Required]
        public int NumberOfDependents { get; set; }

        public List<SelectListItem> Dependents { get; } = new List<SelectListItem>();

        public IndexModel()
        {
            PopulateDependentListDropdown();
        }

        public void OnGet()
        {

        }

        /// <summary>
        /// On Dependent list count submit
        /// </summary>
        /// <returns></returns>
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            return RedirectToPage(PagesPath.INFORMATION, new { dependents = NumberOfDependents });
        }

        private void PopulateDependentListDropdown()
        {
            for (int i = 0; i <= 5; i++)
            {
                if (i == 0)
                {
                    Dependents.Add(new SelectListItem("-- Select --", i.ToString()));
                }
                else
                {
                    Dependents.Add(new SelectListItem(i.ToString(), i.ToString()));
                }

            }

        }
    }
}
