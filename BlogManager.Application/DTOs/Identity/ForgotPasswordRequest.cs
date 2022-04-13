using System.ComponentModel.DataAnnotations;

namespace BlogManager.Application.DTOs.Identity
{
    public class ForgotPasswordRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}