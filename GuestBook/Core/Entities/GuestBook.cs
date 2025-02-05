namespace GuestBook.Core.Entities;

public class GuestBook
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Message { get; set; }
    public string? PhotoPath { get; set; }
    public DateTime DatePosted { get; set; } = DateTime.UtcNow;
}