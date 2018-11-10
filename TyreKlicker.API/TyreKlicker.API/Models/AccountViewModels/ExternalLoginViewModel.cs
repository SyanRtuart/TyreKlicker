using System.ComponentModel.DataAnnotations;

namespace TyreKlicker.API.Models.AccountViewModels
{
    public class ExternalLoginViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}