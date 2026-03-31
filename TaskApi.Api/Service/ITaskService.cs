public interface ITaskService
{
    List<TaskItem> GetAll();
    TaskItem? GetById(int id);
    TaskItem Create(CreateTaskDto dto);
    TaskItem? Update(int id,UpdateTaskDto dto);
    bool MarkCompleted(int id);
    bool Delete(int id);
}