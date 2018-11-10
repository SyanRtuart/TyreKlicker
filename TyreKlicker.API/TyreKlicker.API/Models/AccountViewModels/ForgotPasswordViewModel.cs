using System.ComponentModel.DataAnnotations;

namespace TyreKlicker.API.Models.AccountViewModels
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}