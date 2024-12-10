using Domain;
using Dto.Commands;
using Dto.Dto;

namespace Application.Handlers;

public class TodoItemHandler(ITodoItemRepository repository) : ITodoItemHandler
{
    private readonly ITodoItemRepository _todoItemRepository = repository;

    public async Task<TodoItemDetailsDto> Get(int id)
    {
        var entity = await _todoItemRepository.GetAsync(id);
        return new TodoItemDetailsDto
        {
            Id = entity.Id,
            Title = entity.Title,
            Description = entity.Description,
            IsCompleted = entity.IsCompleted,
        };
    }

    public async Task<List<TodoItemList>> GetAll()
    {
        var entities = await _todoItemRepository.GetAll();
        return entities.Select(c => new TodoItemList
        {
            Id = c.Id,
            Title = c.Title,
            Description = c.Description,
            IsCompleted = c.IsCompleted,
            CreatedDate = c.CreatedDate,
        })
        .ToList();
    }

    public IEnumerable<TodoItemList> GetPending()
    {
        var entities = _todoItemRepository.GetPending();
        return entities.Select(c => new TodoItemList
        {
            Id = c.Id,
            Title = c.Title,
            Description = c.Description,
            IsCompleted = c.IsCompleted,
            CreatedDate = c.CreatedDate,
        });
    }

    public async Task<int> Create(CreateTodoItemCommand command)
    {
        var entity = new TodoItem(command.Title, command.Description);
        _todoItemRepository.Add(entity);
        await _todoItemRepository.UnitOfWork.SaveChangesAsync();
        return entity.Id;
    }

    public async Task Delete(int id)
    {
        var entity = await _todoItemRepository.GetAsync(id);
        _todoItemRepository.Delete(entity);
        await _todoItemRepository.UnitOfWork.SaveChangesAsync();
    }

    public async Task Update(UpdateTodoItemCommand command)
    {
        var entity = await _todoItemRepository.GetAsync(command.Id);
        entity.Update(command.Title, command.Description);
        _todoItemRepository.Update(entity);
        await _todoItemRepository.UnitOfWork.SaveChangesAsync();
    }

    public async Task Complete(int id)
    {
        var entity = await _todoItemRepository.GetAsync(id);
        entity.Complete();
        _todoItemRepository.Update(entity);
        await _todoItemRepository.UnitOfWork.SaveChangesAsync();
    }

    public async Task UnComplete(int id)
    {
        var entity = await _todoItemRepository.GetAsync(id);
        entity.UnComplete();
        _todoItemRepository.Update(entity);
        await _todoItemRepository.UnitOfWork.SaveChangesAsync();
    }
}
