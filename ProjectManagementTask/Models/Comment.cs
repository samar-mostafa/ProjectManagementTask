using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagement.Models
{
    public class Comment:BaseModel
    {
        public string Description { get; set; }
      
        [ForeignKey(nameof(Task))]
        public string TaskId { get; set; }
        public Task Task { get; set; }

    }
}
