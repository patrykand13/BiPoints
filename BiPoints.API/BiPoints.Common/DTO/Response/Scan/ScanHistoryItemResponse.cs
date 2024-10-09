namespace BiPoints.Common.DTO.Response.Scan
{
    public class ScanHistoryItemResponse : ItemResponse
    {
        public string DateAdded { get; set; }
        public int Points { get; set; }
        public bool SuccessVisible { get; set; }
        public bool DuplicateVisible { get; set; }
    }
}
