using BiPoints.Models.Responses.Scan;

namespace BiPoints.Models.Responses.ScanHistory
{
    internal class ScanHistoryItemResponse : ItemResponse
    {
        public string DateAdded { get; set; }
        public int Points { get; set; }
        public bool SuccessVisible { get; set; }
        public bool DuplicateVisible { get; set; }
    }
}
