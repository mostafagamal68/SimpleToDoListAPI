using Domain;
using Microsoft.EntityFrameworkCore;

namespace Infrastructrue.Repositories;

public class TodoItemRepository(AppDbContext appContext) : ITodoItemRepository
{
    private readonly AppDbContext _appContext = appContext;
    public IUnitOfWork UnitOfWork => _appContext;

    public void Add(TodoItem todoItem)
    {
        _appContext.TodoItems.Add(todoItem);
    }

    public void Delete(TodoItem todoItem)
    {
        _appContext.TodoItems.Remove(todoItem);
    }

    public async Task<List<TodoItem>> GetAll()
    {
        return await _appContext.TodoItems.ToListAsync();
    }

    public IEnumerable<TodoItem> GetPending()
    {
        return _appContext.TodoItems.Where(c => !c.IsCompleted).AsEnumerable();
    }

    public async Task<TodoItem> GetAsync(int id)
    {
        return await _appContext.TodoItems.FirstOrDefaultAsync(x => x.Id == id)
            ?? throw new KeyNotFoundException();
    }

    public void Update(TodoItem todoItem)
    {
        _appContext.TodoItems.Update(todoItem);
    }
}
