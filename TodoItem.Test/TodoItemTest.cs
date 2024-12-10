using Application.Handlers;
using Domain;
using Dto.Commands;
using Dto.Dto;
using FakeItEasy;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SimpleToDoList.API.Controllers;

namespace SimpleToDoList.Test
{
    public class TodoItemTest
    {
        private readonly ITodoItemHandler _handler;
        private readonly ITodoItemRepository _repository;
        private readonly TodoController _controller;
        public TodoItemTest()
        {
            _handler = A.Fake<ITodoItemHandler>();
            _repository = A.Fake<ITodoItemRepository>();
            _controller = A.Fake<TodoController>();
        }

        [Fact]
        public async Task TodoController_GetAll_ReturnOk()
        {
            //Arrange
            var todoItems = new List<TodoItemList>
            {
                new() { Id = 1, Title = "title", Description = "desc", CreatedDate = DateTimeOffset.Now }
            };
            A.CallTo(() => _repository.GetAll());
            A.CallTo(() => _handler.GetAll());
            
            //Act
            var result = await _controller.GetAll();
            var resultObject = result.Result as OkObjectResult;

            //Assert
            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status200OK, resultObject.StatusCode);
        }

        [Fact]
        public async Task TodoController_Create_ReturnCreated()
        {
            //Arrange
            var command = new CreateTodoItemCommand
            {
                Title = "title",
                Description = "desc",
            };
            var item = new TodoItem(command.Title, command.Description);
            A.CallTo(() => _repository.Add(item));
            A.CallTo(() => _handler.Create(command));

            //Act
            var result = await _controller.Create(command);
            var resultObject = result as OkObjectResult;

            //Assert
            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status200OK, resultObject.StatusCode);
        }

        [Fact]
        public async Task TodoController_Update_ReturnAccepted()
        {
            //Arrange
            var command = new UpdateTodoItemCommand
            {
                Title = "title",
                Description = "desc",
            };
            var item = new TodoItem(command.Title, command.Description);
            item.Update(command.Title, command.Description);
            A.CallTo(() => _repository.Update(item));
            A.CallTo(() => _handler.Update(command));

            //Act
            var result = await _controller.Update(command);
            var resultObject = result as AcceptedResult;

            //Assert
            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status202Accepted, resultObject.StatusCode);
        }

        [Fact]
        public async Task TodoController_Complete_ReturnAccepted()
        {
            //Arrange
            var item = new TodoItem("Title", "Description");
            item.Complete();
            A.CallTo(() => _repository.Update(item));
            A.CallTo(() => _handler.Complete(item.Id));

            //Act
            var result = await _controller.Complete(item.Id);
            var resultObject = result as AcceptedResult;

            //Assert
            Assert.NotNull(result);
            Assert.True(item.IsCompleted);
            Assert.Equal(StatusCodes.Status202Accepted, resultObject.StatusCode);
        }
    }
}