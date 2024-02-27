using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskSchedulerAPI.Model
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required(ErrorMessage ="User name is required")]
        [Column(TypeName = "nvarchar(255)")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "User role is required")]
        public int UserRole {  get; set; }

        [Required(ErrorMessage = "User email is required")]
        [EmailAddress]
        public string UserEmail { get; set; }

        //[Required(ErrorMessage = "User password is required")]
        //[DataType(DataType.Password)]
        //public string password { get; set; }
        
        public string? UserPhone { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public string? Token { get; set; }

        [Required]
        public string PasswordSalt { get; set; }
        [Required]
        public string PasswordHash { get; set; }

    }
}
