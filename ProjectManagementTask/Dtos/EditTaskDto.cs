using ProjectManagement.Helpers;
using System.ComponentModel.DataAnnotations;

namespace ProjectManagement.Dtos
{
    public class EditTaskDto
    {
        [Required(ErrorMessage = Messages.Required)]
        public string Id { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = Messages.Required)]
        public string Name { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = Messages.Required)]
        public string Description { get; set; }
        [Required(ErrorMessage = Messages.Required)]
        public DateTime StartDate { get; set; }
        [Required(ErrorMessage = Messages.Required)]
        public DateTime EndDate { get; set; }

        [Required(ErrorMessage = Messages.Required)]
        public int Priority { get; set; }
    }
}
