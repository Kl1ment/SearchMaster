namespace SearchMaster.Contracts.Request
{
    public record OrderRequest(
        string Title,
        string Description,
        decimal Price);
}
