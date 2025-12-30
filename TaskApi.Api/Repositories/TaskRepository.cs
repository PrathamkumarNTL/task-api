public class TaskRepository : ITaskRepository
{
    private static readonly List<TaskItem> _tasks = new();
    private static int _id=1;

    public List<TaskItem> GetAll()
    {
        return _tasks;
    }

    public TaskItem? GetById(int id)
    {
        return _tasks.FirstOrDefault(t=> t.id == id);
    }

    public TaskItem Add(TaskItem task)
    {
        task.id = _id++;
        _tasks.Add(task);
        return task;
    }

    public bool Update(TaskItem task)
    {
        return true;
    }
    

    public bool Delete(TaskItem task)
    {
        return _tasks.Remove(task);
    }
}