namespace Dto.Dto
{
    public class TodoItemList
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsCompleted { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
    }
}
