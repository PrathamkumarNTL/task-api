using Microsoft.Extensions.Caching.Memory;

public class TaskService : ITaskService
{
    private readonly ITaskRepository _repository;
    private readonly IMemoryCache _cache;
    private const string CacheKey = "task_list";
    private readonly ILogger<TaskService> _logger;

    public TaskService(ITaskRepository repository,IMemoryCache cache,ILogger<TaskService> logger)
    {
        _repository = repository;
        _cache = cache;
        _logger = logger;
    }
    
    public List<TaskItem> GetAll()
    {
        //const string cacheKey = "task_list";

        if (!_cache.TryGetValue(CacheKey, out List<TaskItem>? tasks))
        {
            _logger.LogInformation("Fetching from DB...");

            tasks = _repository.GetAll(); 

            var cacheOptions = new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(5));

            _cache.Set(CacheKey, tasks, cacheOptions);
        }
        else
        {
            _logger.LogInformation("Fetching from CACHE...");
        }

        return tasks!;
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

        //return _repository.Add(task);

        var result = _repository.Add(task);
        _cache.Remove(CacheKey);

        return result;
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

       var result = _repository.Delete(task);
       _cache.Remove(CacheKey);
       return result;
    }

    public TaskItem? Update(int id, UpdateTaskDto dto)
    {
        var task = _repository.GetById(id);

        if(task == null)
            return null;
        
        task.Title = dto.Title;
        task.IsCompleted = dto.IsCompleted;

        var isUpdated = _repository.Update(task);

        if(!isUpdated)
            return null;
        
        _cache.Remove(CacheKey);
        return task;
    }
}