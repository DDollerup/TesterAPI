namespace TesterAPI
{
    public class Task
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public int CaseId { get; set; }
        public Case? Case { get; set; }
    }
}
