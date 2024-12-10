using Dto.Commands;
using Dto.Dto;

namespace Application.Handlers;

public interface ITodoItemHandler
{
    Task<TodoItemDetailsDto> Get(int id);
    Task<List<TodoItemList>> GetAll();
    IEnumerable<TodoItemList> GetPending();
    Task<int> Create(CreateTodoItemCommand command);
    Task Delete(int id);
    Task Update(UpdateTodoItemCommand command);
    Task Complete(int id);
    Task UnComplete(int id);
}
