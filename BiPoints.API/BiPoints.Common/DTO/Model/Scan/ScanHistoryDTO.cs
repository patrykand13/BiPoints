namespace BiPoints.Common.DTO.Model.Scan
{
    public class ScanHistoryDTO
    {
        public string Name { get; set; }
        public string Image { get; set; }
        public int Points { get; set; }
        public bool ScanSuccess { get; set; }
        public DateTime AddDate { get; set; }
    }
}
