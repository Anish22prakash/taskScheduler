using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TaskSchedulerAPI.DtoModels
{
    public class RegisterUserDto
    {
      
        [Required(ErrorMessage = "User name is required")]
        [Column(TypeName = "nvarchar(255)")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "User email is required")]
        [EmailAddress]
        public string UserEmail { get; set; }

        [Required(ErrorMessage = "User email is required")]
        [DataType(DataType.Password)]
        public string password { get; set; }
    }
}
