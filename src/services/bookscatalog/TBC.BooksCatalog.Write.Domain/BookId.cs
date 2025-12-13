namespace TBC.BooksCatalog.Write.Domain;

public sealed record BookId
{
    public Guid Value { get; private set; } = Guid.CreateVersion7();
}
