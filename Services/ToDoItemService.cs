using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using ToDoApp.Data;
using ToDoApp.Models;

namespace ToDoApp.Services
{
    public class ToDoItemService : IToDoItemService
    {
        private readonly ApplicationDbContext _context;
        public ToDoItemService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> AddItemAsync(ToDoItem newItem)
        {
            newItem.Id = Guid.NewGuid();
            newItem.IsDone = false;
            _context.Items.Add(newItem);
            var saveResult = await _context.SaveChangesAsync();
            return saveResult == 1;

        }

        public async Task<ToDoItem[]> GetIncompleteItemsAsync()
        {
            return await _context.Items
            .Where(x => x.IsDone == false)
            .ToArrayAsync();
        }

        public async Task<bool> MarkDoneAsync(Guid id)
        {
            var item = await _context.Items
            .Where(x => x.Id == id)
            .SingleOrDefaultAsync();
            if (item == null) return false;
            item.IsDone = true;
            var saveResult = await _context.SaveChangesAsync();
            return saveResult == 1; // One entity should have been updated
        }
    }
}
