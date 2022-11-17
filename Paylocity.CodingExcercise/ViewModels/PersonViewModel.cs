using System.ComponentModel.DataAnnotations;

namespace Paylocity.CodingExcercise.Web.ViewModels
{
    public class PersonViewModel
    {
        [Required]
        public string Name { get; set; }
    }
}
