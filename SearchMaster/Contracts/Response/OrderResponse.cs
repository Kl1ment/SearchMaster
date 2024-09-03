namespace SearchMaster.Contracts.Response
{
    public record OrderResponse(
        Guid Id,
        ClientResponse? Client,
        string Title,
        string Description,
        decimal Price,
        DateTime CreatedDate);
}