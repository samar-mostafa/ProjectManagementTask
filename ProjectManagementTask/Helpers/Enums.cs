

using System.ComponentModel.DataAnnotations;

namespace ProjectManagement.Enums
{
    
    public enum TaskStatusEnum
    {
        NotStarted,
        InProgress,
        Completed
    }

    public enum ManagementRoles
    {
        [Display(Name = "Employee")]
        Employee,
        [Display(Name = "Manager")]
        Manager
       
    }



}
