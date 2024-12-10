namespace Domain;

public interface ITodoItemRepository
{
    Task<List<TodoItem>> GetAll();
    IEnumerable<TodoItem> GetPending();
    Task<TodoItem> GetAsync(int id);
    void Add(TodoItem todoItem);
    void Update(TodoItem todoItem);
    void Delete(TodoItem todoItem);
    IUnitOfWork UnitOfWork { get; }
}
