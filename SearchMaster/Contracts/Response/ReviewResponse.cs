namespace SearchMaster.Contracts.Response
{
    public record ReviewResponse(
        PersonResponse? Writer,
        int Mark,
        string TextData,
        DateTime CreatedDate);
}
