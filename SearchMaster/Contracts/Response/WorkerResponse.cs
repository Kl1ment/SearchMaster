namespace SearchMaster.Contracts.Response
{
    public record WorkerResponse(
        Guid Id,
        string Username,
        string Name,
        string Surname,
        string Profession,
        string About,
        float Rating,
        List<ReviewResponse> Reviews);
}
