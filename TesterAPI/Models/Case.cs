namespace TesterAPI
{
    public class Case
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public DateTime StartDate { get; set; } /* = DateTime.Now;*/
        public DateTime EndDate { get; set; }

        public List<Task>? Tasks { get; set; }
    }
}
