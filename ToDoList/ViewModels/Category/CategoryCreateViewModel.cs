using System.ComponentModel.DataAnnotations;

namespace ToDoList.ViewModels
{
    public class CategoryCreateViewModel
    {
        [Required(ErrorMessage = "Please enter category name")]
        public string Name { get; set; }
    }
}
