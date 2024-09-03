namespace SearchMaster.Contracts.Request
{
    public record ReviewRequest(
        int Mark,
        string TextData,
        Guid HolderId);
}
