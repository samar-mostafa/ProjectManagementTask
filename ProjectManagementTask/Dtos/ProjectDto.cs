namespace ProjectManagementTask.Dtos
{
    public class ProjectDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public double Budget { get; set; }
        public string Owner { get; set; }
        public int Status { get; set; }
       
    }
}
