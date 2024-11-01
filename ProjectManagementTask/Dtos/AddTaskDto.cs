using System.ComponentModel.DataAnnotations;
using ProjectManagement.Enums;
using ProjectManagement.Helpers;

namespace ProjectManagement.Dtos
{
    public class AddedTaskDto
    {

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

        public string? AssignedToId { get; set; } 
        [Required(ErrorMessage = Messages.Required)]
        public string ProjectId { get; set; }
    }

    public class AddTaskDto:AddedTaskDto
    {
        [Required(ErrorMessage = Messages.Required)]
        public TaskStatusEnum Status { get; set; }
    }


}
