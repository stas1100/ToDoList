namespace ToDoList.ViewModels
{
    public class ToDoTaskEditViewModel
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public bool IsDone { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? DeadlineDate { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
    }
}
