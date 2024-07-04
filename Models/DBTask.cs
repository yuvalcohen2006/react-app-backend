using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace react_practice_backend.Models
{
    [Table("TaskList")]
    public class DBTask
    {
        [Key]
        public int Id { get; set; }

        [Column("Task")]
        public string Task { get; set; }

        [Column("Completed")] 
        public bool Completed { get; set; }
    }
}
