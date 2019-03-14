using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ToDoApp.Models;
using ToDoApp.Services;

namespace ToDoApp.Controllers
{
    public class ToDoController : Controller
    {
        private readonly IToDoItemService _toDoItemService;
        public ToDoController(IToDoItemService toDoItemService)
        {
            _toDoItemService = toDoItemService;
        }
        public async Task<IActionResult> Index()
        {
            var items = await _toDoItemService.GetIncompleteItemsAsync();
            var model = new ToDoViewModel()
            {
                Items = items
            };
            return View(model);
        }
    }
}
