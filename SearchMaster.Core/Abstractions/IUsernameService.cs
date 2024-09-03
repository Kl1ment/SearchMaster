namespace SearchMaster.Application.Services
{
    public interface IUsernameService
    {
        Task<string> Generate(string surname, string name);
    }
}