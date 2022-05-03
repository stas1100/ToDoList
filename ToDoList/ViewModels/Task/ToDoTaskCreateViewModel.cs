namespace ToDoList.ViewModels
{
    public class ToDoTaskCreateViewModel
    {
        public int CategoryId { get; set; }
        public DateTime? DeadlineDate { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
    }
}
