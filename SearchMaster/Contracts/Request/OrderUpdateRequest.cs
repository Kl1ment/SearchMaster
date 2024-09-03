namespace SearchMaster.Contracts.Request
{
    public record OrderUpdateRequest(
        Guid Id,
        Guid ClientId,
        string Title,
        string Description,
        decimal Price);
}
