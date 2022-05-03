using ToDoList.Models;
using ToDoList.ViewModels;

namespace ToDoList.ViewModels
{
    public class ToDoTaskViewModelPage
    {
        public List<ToDoTaskViewModel> CurrentTasks { get; set; }
        public List<ToDoTaskViewModel> CompletedTasks { get; set; }
        public List<CategoryViewModel> Categories { get; set; }
        public int CurrentCategory { get; set; }

    }
}
