public class TaskQueryParams
{
    public int page {get;set;} = 1;
    public int pageSize {get;set;} = 5;
    public bool? IsCompleted {get;set;}
    public string? Search {get;set;}
}