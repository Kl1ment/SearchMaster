namespace SearchMaster.Infrastructure
{
    public class Hasher : IHasher
    {
        public string Generate(string input) =>
            BCrypt.Net.BCrypt.EnhancedHashPassword(input);

        public bool Verify(string input, string hash) =>
            BCrypt.Net.BCrypt.EnhancedVerify(input, hash);
    }
}
