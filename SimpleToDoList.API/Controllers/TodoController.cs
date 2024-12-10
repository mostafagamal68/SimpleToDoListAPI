using Application.Handlers;
using Dto.Commands;
using Dto.Dto;
using Microsoft.AspNetCore.Mvc;

namespace SimpleToDoList.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TodoController(ITodoItemHandler handler) : ControllerBase
{
    private readonly ITodoItemHandler _todoItemHandler = handler;

    /// <summary>
    ///     Create a new Todo item.
    /// </summary>
    /// <param name="command"></param>
    /// <returns>Created Id</returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     POST /Todo
    ///     {
    ///        "title": "Item #1",
    ///        "description": "Description"
    ///     }
    ///
    /// </remarks>
    /// <response code="200">Returns the newly created item id</response>
    /// <response code="400">If the item is null</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create(CreateTodoItemCommand command)
    {
        var id = await _todoItemHandler.Create(command);
        return Ok(id);
    }

    /// <summary>
    ///     Get Todo item.
    /// </summary>
    /// <param name="id"></param>
    /// <returns>TodoItemDto</returns>
    /// <response code="200">Returns todo item</response>
    /// <response code="400">If the item is null</response>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<TodoItemDetailsDto>> Get(int id)
    {
        var dto = await _todoItemHandler.Get(id);
        return Ok(dto);
    }

    /// <summary>
    ///     Get all Todo items.
    /// </summary>
    /// <returns>List of TodoItemDto</returns>
    /// <response code="200">Returns List of todo items</response>
    /// <response code="400">If the items is null</response>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<List<TodoItemList>>> GetAll()
    {
        var list = await _todoItemHandler.GetAll();
        return Ok(list);
    }

    /// <summary>
    ///     Get pending Todo items.
    /// </summary>
    /// <returns>List of TodoItemDto</returns>
    /// <response code="200">Returns List of pending todo items</response>
    /// <response code="400">If the items is null</response>
    [HttpGet("pending")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<IEnumerable<TodoItemList>> Pending()
    {
        var list = _todoItemHandler.GetPending();
        return Ok(list);
    }

    /// <summary>
    ///     Deletes a specific TodoItem.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <response code="204"></response>
    /// <response code="400">If the id is not found</response>
    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Delete(int id)
    {
        await _todoItemHandler.Delete(id);
        return NoContent();
    }

    /// <summary>
    ///     Updates a specific TodoItem.
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     PUT /Todo
    ///     {
    ///        "title": "Item #1",
    ///        "description": "Description"
    ///     }
    ///
    /// </remarks>
    /// <response code="204"></response>
    /// <response code="400">If the id is not found</response>
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Update(UpdateTodoItemCommand command)
    {
        await _todoItemHandler.Update(command);
        return Accepted();
    }

    /// <summary>
    ///     Completes a specific TodoItem.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <response code="204"></response>
    /// <response code="400">If the id is not found</response>
    [HttpPut("{id}/complete")]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Complete(int id)
    {
        await _todoItemHandler.Complete(id);
        return Accepted();
    }

    /// <summary>
    ///     Uncompletes a specific TodoItem.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <response code="204"></response>
    /// <response code="400">If the id is not found</response>
    [HttpPut("{id}/uncomplete")]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UnComplete(int id)
    {
        await _todoItemHandler.UnComplete(id);
        return Accepted();
    }
}
