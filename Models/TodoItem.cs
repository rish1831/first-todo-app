using System.ComponentModel.DataAnnotations;

namespace first_api_project.Models
{
    public class TodoItem
    {
        public Guid Id { get; set; }
        
        [Required]
        public string Title { get; set; }

        public bool IsCompleted { get; set; }
         public DateTime TodoTimeAdded { get; set; }
         public DateTime TodoModifiedTime { get; set; }
    }
}
