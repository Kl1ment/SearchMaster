namespace SearchMaster.Infrastructure
{
    public interface IHasher
    {
        string Generate(string input);
        bool Verify(string input, string hash);
    }
}