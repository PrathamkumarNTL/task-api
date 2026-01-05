public class TaskRepository : ITaskRepository
{
    private readonly AppDbContext _context;

    public TaskRepository(AppDbContext context)
    {
        _context = context;
    }

    public List<TaskItem> GetAll()
    {
        return _context.Tasks.ToList();
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
}