public class TaskService : ITaskService
{
    private readonly ITaskRepository _repository;

    public TaskService(ITaskRepository repository)
    {
        _repository = repository;
    }
    
    public List<TaskItem> GetAll()
    {
        return _repository.GetAll();
    }

    public TaskItem? GetById(int id)
    {
        return _repository.GetById(id);
    }

    public TaskItem Create(CreateTaskDto dto)
    {
        var task = new TaskItem
        {
            Title = dto.Title,
            IsCompleted = false
        };

        return _repository.Add(task);
    }

    public bool MarkCompleted(int id)
    {
        var task = _repository.GetById(id);
        if(task == null)
            return false;

        task.IsCompleted = true;
        return _repository.Update(task);
    }


    public bool Delete(int id)
    {
       var task = _repository.GetById(id);
       if(task == null) return false;

       return _repository.Delete(task);
    }
}