using Paylocity.CodingExcercise.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace Paylocity.CodingExcercise.Web.ViewModels
{
    public class DependentViewModel
    {
        [Required]
        public string Name { get; set; }
        public DependentType DependentType { get; set; }
    }
}
