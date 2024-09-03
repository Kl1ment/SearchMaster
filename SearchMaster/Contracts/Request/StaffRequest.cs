namespace SearchMaster.Contracts.Request
{
    public record StaffRequest(
        string Email,
        string Name,
        string Surname,
        string Role);
}
