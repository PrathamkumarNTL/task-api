using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class TaskController : ControllerBase
{
    private static readonly List<TaskItem> Tasks = new();
    private static int _id = 1;

    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(Tasks);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var task = Tasks.FirstOrDefault(x => x.id == id);
        if(task == null)
            return NotFound();

        return Ok(task);
    }

    [HttpPost]
    public IActionResult Create(CreateTaskDto dto)
    {
        var task = new TaskItem
        {
            id = _id++,
            Title = dto.Title,
            IsCompleted = false
        };

        Tasks.Add(task);
        return CreatedAtAction(nameof(GetById),new {Id = task.id},task);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id)
    {
        var task = Tasks.FirstOrDefault(x =>x.id == id);
        if(task == null)
            return  NotFound();
        
        task.IsCompleted = true;
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var task = Tasks.FirstOrDefault(x => x.id == id);
        if (task == null)
            return NotFound();
        
        Tasks.Remove(task);
        return NoContent();
    }

}