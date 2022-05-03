namespace ToDoList.ViewModels
{
    public class ToDoTaskViewModel
    {
        public int Id { get; set; }
        public string Category { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? DeadlineDate { get; set; }
        public bool IsDone { get; set; }
        public DateTime? DoneDate { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
    }
}
