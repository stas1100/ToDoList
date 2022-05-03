namespace ToDoList.Models
{
    public interface IToDoTaskRepository
    {
        public List<ToDoTaskModel> ListItems(bool? isDone, int categoryId);
        public ToDoTaskModel GetTask(int id);   
        public bool Create(ToDoTaskModel task);
        public bool Delete(int id);
        public bool SetDoneStatus(int id, bool status);
        public bool Update(ToDoTaskModel task);

    }
}
