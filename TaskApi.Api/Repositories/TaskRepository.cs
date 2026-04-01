using Microsoft.EntityFrameworkCore;

public class TaskRepository : ITaskRepository
{
    private readonly AppDbContext _context;

    public TaskRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<TaskItem>> GetAll()
    {
        return await _context.Tasks.ToListAsync();
    }

    public TaskItem? GetById(int id)
    {
        return _context.Tasks.FirstOrDefault(t=> t.Id == id);
    }

    public TaskItem Add(TaskItem task)
    {
        _context.Tasks.Add(task);
        _context.SaveChanges();
        return task;
    }

    public bool Update(TaskItem task)
    {
        _context.Tasks.Update(task);
        _context.SaveChanges();
        return true;
    }
    

    public bool Delete(TaskItem task)
    {
        _context.Tasks.Remove(task);
        _context.SaveChanges();
        return true;
    }

    public async Task<List<TaskItem>> GetAll(TaskQueryParams query)
    {
        var tasks = _context.Tasks.AsQueryable();

        //filtering
        if (query.IsCompleted.HasValue)
        {
            tasks =  tasks.Where(t => t.IsCompleted == query.IsCompleted);
        }

        //search
        if (!string.IsNullOrEmpty(query.Search))
        {
            tasks = tasks.Where(t => t.Title.Contains(query.Search));
        }

        //pagination
        tasks = tasks.Skip((query.page - 1) * query.pageSize).Take(query.pageSize);

        return await tasks.AsNoTracking().ToListAsync();
    }
}