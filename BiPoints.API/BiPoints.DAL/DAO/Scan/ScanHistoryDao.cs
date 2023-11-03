namespace BiPoints.DAL.DAO.Scan
{
    public class ScanHistoryDao
    {
        public string Name { get; set; }
        public string Image { get; set; }
        public int Points { get; set; }
        public bool ScanSuccess { get; set; }
        public DateTime AddDate { get; set; }
    }
}
