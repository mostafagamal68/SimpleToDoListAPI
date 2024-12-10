namespace Domain;

public class TodoItem
{
    private TodoItem() { }

    public TodoItem(string title, string description)
    {
        Title = title;
        Description = description;
        CreatedDate = DateTimeOffset.Now;
    }
    public int Id { get; private set; }
    public string Title { get; private set; }
    public string Description { get; private set; }
    public bool IsCompleted { get; private set; }
    public DateTimeOffset CreatedDate { get; private set; }

    public void Update(string title, string description)
    {
        Title = title;
        Description = description;
    }

    public void Complete() => IsCompleted = true;
    public void UnComplete() => IsCompleted = false;

}
