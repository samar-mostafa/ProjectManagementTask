
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagement.Models
{
    [Table("Tasks")]
    public class Task :BaseModel
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Status { get; set; }
        public int Priority { get; set; }
        public bool IsDeleted { get; set; }

        [ForeignKey(nameof(CreatedBy))]
        public string? CreatedById { get; set; }
        [ForeignKey(nameof(LastModifiedBy))]
        public string? LastModifiedById { get; set; }
        [ForeignKey(nameof(AssignedTo))]
        public string? AssignedToId { get; set; }

        [ForeignKey(nameof(Project))]
        public string ProjectId { get; set; }

        public User? CreatedBy { get; set; }
        public User? LastModifiedBy { get; set; }
        public User? AssignedTo { get; set; }
        public Project Project { get; set; }
        public List<Comment> Comments { get; set; }
    }
}
