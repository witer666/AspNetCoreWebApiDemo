using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreWebApiDemo.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class TodoController : Controller
    {
        private static int _Id = 0;
        public IRepositoryWrapper RepositoryWrapper;
        public TodoController(IRepositoryWrapper repository)
        {
            RepositoryWrapper = repository;
        }

        [HttpGet(Name = "Index")]
        public async Task<ActionResult<IEnumerable<Todo>>> Index()
        {
            return (await RepositoryWrapper.Todo.GetListAsync()).OrderBy(t => t.Id).ToList();
        }

        [HttpGet(Name = "Add")]
        public IActionResult Add()
        {
            RepositoryWrapper.Todo.Add(new Todo() { Id = ++_Id, Name = "a", IsComplete = true});
            return Ok();
        }

        [HttpGet(Name = "GetInfo")]
        public async Task<ActionResult<Todo>> GetInfo(int id)
        {
            return await RepositoryWrapper.Todo.GetInfo(id);
        }
    }
}
