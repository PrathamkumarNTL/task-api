public interface ITaskService
{
    List<TaskItem> GetAll();
    TaskItem? GetById(int id);
    TaskItem Create(CreateTaskDto dto);
    bool MarkCompleted(int id);
    bool Delete(int id);
}