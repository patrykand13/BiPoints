namespace BiPoints.Common.DTO.Response.Scan
{
    public class PointsInformationResponse
    {
        public string DayAchievementColor { get; set; }
        public string WeekAchievementColor { get; set; }
        public int DayAchievementToGet { get; set; }
        public int WeekAchievementToGet { get; set; }
        public int DayAchievementValue { get; set; }
        public int WeekAchievementValue { get; set; }
        public int BiPoints { get; set; }
        public int BiPointsForUse { get; set; }
        public int BiPointsUsed { get; set; }
        public double DayAchievementProgress { get; set; }
        public double WeekAchievementProgress { get; set; }
    }
}
