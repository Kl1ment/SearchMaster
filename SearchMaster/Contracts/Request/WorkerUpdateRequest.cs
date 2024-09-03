namespace SearchMaster.Contracts.Request
{
    public record WorkerUpdateRequest(
        string Email,
        string Name,
        string Surname,
        string Profession,
        string About);
}
