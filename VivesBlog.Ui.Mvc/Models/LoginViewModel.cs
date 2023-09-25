using System.ComponentModel.DataAnnotations;

namespace VivesBlog.Ui.Mvc.Models
{
    public class LoginViewModel
    {
        [Required]
        [EmailAddress]
        public required string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public required string Password { get; set; }
    }
}
