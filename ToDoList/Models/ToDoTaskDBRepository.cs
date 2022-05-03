using System.Data.SqlClient;
using Dapper;

namespace ToDoList.Models
{
    public class ToDoTaskDBRepository : IToDoTaskRepository
	{
		private string _connectionString;

		public ToDoTaskDBRepository(IConfiguration configuration)
		{
			_connectionString = configuration["Data:ToDoItems:ConnectionString"];
		}

        public List<ToDoTaskModel> ListItems(bool? isDone = null, int categoryId = 0 )
		{
			using (var conn = new SqlConnection(_connectionString))
			{
				var parameters = new { Id = categoryId, IsDone = isDone };
				string sqlWhere = "";
				if (categoryId != 0){
					sqlWhere += $"WHERE Tasks.CategoryId = @Id ";
				}
                if (isDone.HasValue) {
					if(sqlWhere != "")
                    {
						sqlWhere += $"AND Tasks.IsDone = @IsDone ";
                    }
                    else
                    {
						sqlWhere += $"WHERE Tasks.IsDone = @IsDone ";
					}
                    if (isDone == true)
                    {
						sqlWhere += " ORDER BY Tasks.DoneDate DESC";
                    }
                    else
                    {
						sqlWhere += " ORDER BY COALESCE(Tasks.DeadlineDate,'2079-01-01') ASC";
					}
				}

				string sqlQuery = "SELECT Tasks.Id, Tasks.Title, Tasks.CategoryId, Categories.Name AS Category, " +
					"Tasks.CreatedDate, Tasks.DeadlineDate, Tasks.IsDone, " +
					"Tasks.DoneDate, Tasks.Description FROM Tasks " +
					"INNER JOIN Categories ON Tasks.CategoryId=Categories.Id " + sqlWhere;
				var tasksList = conn.Query<ToDoTaskModel>(sqlQuery, parameters).ToList();
				return tasksList;
			}
		}

		public bool Create(ToDoTaskModel task)
		{
			int affectedRows = 0;
			using (var conn = new SqlConnection(_connectionString))
			{
				var parameters = new { Title = task.Title, Description = task.Description,
					CategoryId = task.CategoryId, DeadlineDate = task.DeadlineDate };
				string sqlQuery = $"INSERT INTO Tasks (Title, Description, CategoryId, CreatedDate, DeadlineDate) VALUES(@Title, @Description, @CategoryId,  '{DateTime.Now}', @DeadlineDate)";
				affectedRows = conn.Execute(sqlQuery, parameters);
			}
			return affectedRows > 0;
		}

        public bool Delete(int id)
        {
			var affectedRows = 0;
			using (var conn = new SqlConnection(_connectionString))
			{
				var parameters = new { Id = id };
				string sqlQuery = $"DELETE FROM Tasks WHERE Id = @Id";
				affectedRows = conn.Execute(sqlQuery, parameters);
			}
			return affectedRows > 0;
		}

        public bool SetDoneStatus(int id, bool status)
        {
			var affectedRows = 0;
			using (var conn = new SqlConnection(_connectionString))
			{
				var parameters = new { Id = id, Status = status };
				string sqlQuery = $"UPDATE Tasks SET IsDone = @Status, DoneDate = '{DateTime.Now}' WHERE Id = @Id";
				affectedRows = conn.Execute(sqlQuery, parameters);
			}
			return affectedRows > 0;
		}

        public ToDoTaskModel GetTask(int id)
        {
			using (var conn = new SqlConnection(_connectionString))
			{
				var parameters = new { Id = id };
				string sqlQuery = "SELECT * FROM Tasks WHERE Id = @Id";
				ToDoTaskModel res = conn.QueryFirst<ToDoTaskModel>(sqlQuery, parameters);
				return res;
			}
		}
        public bool Update(ToDoTaskModel task)
        {
			var affectedRows = 0;
			using (var conn = new SqlConnection(_connectionString))
			{
				var parameters = new { Id = task.Id, Title = task.Title, Description = task.Description,
					DeadlineDate = task.DeadlineDate, CategoryId = task.CategoryId };
				string sqlQuery = "UPDATE Tasks SET Title = @Title, Description = @Description, CategoryId = @CategoryId," +
                    " DeadlineDate = @DeadlineDate WHERE Id = @Id";
				affectedRows = conn.Execute(sqlQuery, parameters);
			}
			return affectedRows > 0;
		}
    }
}
