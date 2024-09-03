namespace SearchMaster.Contracts.Response
{
    public record ClientResponse(
        Guid Id,
        string Name,
        string Surname,
        float Rating,
        List<ReviewResponse> Reviews,
        List<OrderResponse> Orders)
        : PersonResponse(
            Id,
            Name,
            Surname);
}
