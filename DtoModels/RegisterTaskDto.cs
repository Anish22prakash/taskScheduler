using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TaskSchedulerAPI.DtoModels
{
    public class RegisterTaskDto
    {
        
        [Required(ErrorMessage = "Task title is required")]
        [Column(TypeName = "varchar(255)")]
        public string TaskTitle { get; set; }


        public string? TaskDesciption { get; set; }

        [Required(ErrorMessage = "User status is required")]
        [Range(1, 3, ErrorMessage = "Task status must be between 1 and 3")]
        public int TaskStatus { get; set; }

        [Required(ErrorMessage = "Task priority is required")]
        [Range(1, 3, ErrorMessage = "Task priority must be between 1 and 3")]
        public int TaskPriority { get; set; }

        public DateTime? DueUpdatedDate { get; set; }

        [Required(ErrorMessage = "UserId is required")]
        public int UserId { get; set; }
    }
}
