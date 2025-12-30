public class TaskService : ITaskService
{
    public static readonly List<TaskItem> _tasks = new();
    private static int _id=1;
    
    public List<TaskItem> GetAll()
    {
        return _tasks;
    }

    public TaskItem? GetById(int id)
    {
        return _tasks.FirstOrDefault(t =>t.id == id);
    }

    public TaskItem Create(CreateTaskDto dto)
    {
        var task = new TaskItem
        {
            id = _id++,
            Title = dto.Title,
            IsCompleted = false
        };

        _tasks.Add(task);
        return task;
    }

    public bool MarkCompleted(int id)
    {
        var task = GetById(id);
        if(task == null)
            return false;

        task.IsCompleted = true;
        return true;
    }


    public bool Delete(int id)
    {
        var task = GetById(id);
        if(task == null)
            return false;
        _tasks.Remove(task);
        return true;
    }
}