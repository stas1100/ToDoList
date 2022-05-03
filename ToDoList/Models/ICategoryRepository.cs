namespace ToDoList.Models
{
    public interface ICategoryRepository 
    {
        public List<CategoryModel> GetAllCategories();
        public CategoryModel GetCategory(int id);
        public bool Delete(CategoryModel category);
        public bool Create(CategoryModel category);
        public bool Update(CategoryModel category);
    }
}
