namespace TaskManagement.Api.Entities
{
    public class MyTask
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty;
        public ApplicationUser User { get; set; } = default!;
        public ICollection<DurationOfDay> DurationOfDays { get; set; } = [];

    }
}
