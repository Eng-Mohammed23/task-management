using System.ComponentModel.DataAnnotations;

namespace TaskManagement.Api.Entities
{
    public class DurationOfDay
    {
        public int Id {  get; set; }
        public decimal Value { get; set; }
        public DateTime Time { get; set; }
        public string? Rating {  get; set; }
        public int TaskId { get; set; }
        public MyTask MyTask { get; set; } = default!;
    }
}
