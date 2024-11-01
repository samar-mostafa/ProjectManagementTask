using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ProjectManagement.Models
{
    public class BaseModel
    {
        public string Id { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PublicId { get; set; }

        [ScaffoldColumn(false)]
        public bool IsDeleted { get; set; }

        [ScaffoldColumn(false)]
        [Display(Name = "Created At")]
        public DateTime CreatedAt { get; set; }

        [ScaffoldColumn(false)]
        [Display(Name = "Updated At")]
        public DateTime UpdatedAt { get; set; }
    }
}
