namespace ProjectManagement.Dtos
{
    public class TaskDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string ProjectName { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Status { get; set; }
        public int Priority { get; set; }
        public string AssignedTo { get; set; }
    }
}
