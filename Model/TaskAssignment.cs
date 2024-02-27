using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskSchedulerAPI.Model
{
    public class TaskAssignment
    {
        [Key]
        public int TaskId { get; set; }

        [Required(ErrorMessage = "Task title is required")]
        [Column(TypeName = "varchar(255)")]
        public string TaskTitle { get; set; }

        
        public string? TaskDesciption { get; set; }

        [Required(ErrorMessage = "User status is required")]
        public int TaskStatus { get; set; }

        [Required(ErrorMessage = "Task priority is required")]
        [Range(1, 3, ErrorMessage = "Task priority must be between 1 and 3")]
        public int TaskPriority { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public DateTime? DueDate { get; set;}

        [Required(ErrorMessage = "UserId is required")]
        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public User? User { get; set; }
    }
}
