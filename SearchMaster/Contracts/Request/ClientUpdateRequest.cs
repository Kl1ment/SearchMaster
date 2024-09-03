namespace SearchMaster.Contracts.Request
{
    public record ClientUpdateRequest(
        string Email,
        string Name,
        string Surname);
}
