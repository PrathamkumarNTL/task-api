using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class TaskController : ControllerBase
{
    private readonly ITaskService _taskService;

    public TaskController(ITaskService taskService)
    {
        _taskService = taskService;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(_taskService.GetAll());
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var task = _taskService.GetById(id);
        if(task == null)
            return NotFound();

        return Ok(task);
    }

    [HttpPost]
    public IActionResult Create(CreateTaskDto dto)
    {
        var task = _taskService.Create(dto);
        return CreatedAtAction(nameof(GetById),new {id = task.Id},task);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id)
    {
        var result = _taskService.MarkCompleted(id);
        if(!result)
            return NotFound();
        return NoContent();
    }

    [Authorize(Roles ="Admin")]
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var result = _taskService.Delete(id);
        if(!result)
            return NotFound();
        return NoContent();
    }

}