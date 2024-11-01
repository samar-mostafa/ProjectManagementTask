using System.ComponentModel.DataAnnotations;
using ProjectManagement.Helpers;
namespace ProjectManagement.Dtos
{
    public class EditProjectDto
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = Messages.Required)]
        public string Id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = Messages.Required)]
        public string Name { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = Messages.Required)]
        public string Description { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = Messages.Required)]
        public DateTime StartDate { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = Messages.Required)]
        public DateTime EndDate { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = Messages.Required)]
        public double Budget { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = Messages.Required)]
        public string Owner { get; set; }
    }
}
