using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Authorize]
[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
public class TaskController : ControllerBase
{
    private readonly ITaskService _taskService;

    public TaskController(ITaskService taskService)
    {
        _taskService = taskService;
    }

    //[HttpGet]
    // public IActionResult GetAll()
    // {
    //     //return Ok(_taskService.GetAll());

    //     var tasks = _taskService.GetAll();

    //     return Ok(new ApiResponse<List<TaskItem>>
    //     {
    //         Success = true,
    //         Data = tasks,
    //         Message = "Tasks fetched successfully"
    //     });
    // }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] TaskQueryParams query)
    {
        var result = await _taskService.GetAll(query);
        return Ok(new ApiResponse<List<TaskItem>>
    {
        Success = true,
        Data = result,
        Message = "Tasks fetched successfully"
    });
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var task = _taskService.GetById(id);
        if(task == null)
            return NotFound(new ApiResponse<string>
            {
                Success = false,
                Data = null,
                Message = "Task not found"
            });

        //return Ok(task);

        return Ok(new ApiResponse<TaskItem>
        {
            Success = true,
            Data = task,
            Message = "Id fetched successfully"
        });
    }

    [HttpPost]
    public IActionResult Create(CreateTaskDto dto)
    {
        var task = _taskService.Create(dto);
        //return CreatedAtAction(nameof(GetById),new {id = task.Id},task);

        return Ok(new ApiResponse<TaskItem>
        {
            Success = true,
            Data = task,
            Message = "Task created successfully"
        });
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id,UpdateTaskDto dto)
    {
        var updateTask = _taskService.Update(id,dto);
        if(updateTask == null)
        {
            return NotFound(new ApiResponse<string>
            {
               Success = false,
               Message = "Task not found" 
            });
        }

        return Ok(new ApiResponse<TaskItem>
        {
           Success = true,
           Data = updateTask,
           Message = "Task updated successfully" 
        });
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