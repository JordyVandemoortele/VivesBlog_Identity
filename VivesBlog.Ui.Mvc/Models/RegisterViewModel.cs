using System.ComponentModel.DataAnnotations;

namespace VivesBlog.Ui.Mvc.Models
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        public required string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public required string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare(nameof(Password))]
        public required string ConfirmPassword { get; set; }
    }
}
