
using ProjectManagement.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagement.Models
{
    public class Project :BaseModel
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public double Budget { get; set; }
        public string Owner { get; set; } = null!;
        public bool Status { get; set; }
        public bool IsDeleted { get; set; }

        public List<ProjectManagement.Models.Task> Tasks { get; set; }
    }
}
