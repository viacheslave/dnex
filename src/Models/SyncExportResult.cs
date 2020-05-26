namespace SpringOff.DNEx
{
  internal sealed class SyncExportResult
  {
    public int UserId { get; set; }

    public string FileLocation { get; set; }
    
    public SyncDataResponse Response { get; set; }
  }
}

