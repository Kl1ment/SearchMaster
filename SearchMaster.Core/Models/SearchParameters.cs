namespace SearchMaster.Core.Models
{
    public record SearchParameters(
        string? Profession,
        decimal MinPrice = 0,
        decimal MaxPrice = 0,
        int Page = 1);
}
