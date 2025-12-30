public interface ITaskRepository
{
    List<TaskItem> GetAll();
    TaskItem? GetById(int id);
    TaskItem Add(TaskItem task);
    bool Update(TaskItem task);
    bool Delete(TaskItem task);
}
