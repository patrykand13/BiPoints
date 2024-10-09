namespace BiPoints.Common.DTO.Request.Base
{
    public class SkipRecordsRequest
    {
        public Guid UserId { get; set; }
        public int SkipRecords { get; set; }
    }
}
