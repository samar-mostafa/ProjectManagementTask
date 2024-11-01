using ProjectManagement.Helpers;
using System.ComponentModel.DataAnnotations;

namespace ProjectManagement.Dtos
{
    public class EditTaskDto:AddedTaskDto
    {
        [Required(ErrorMessage = Messages.Required)]
        public string Id { get; set; }
    }
}
