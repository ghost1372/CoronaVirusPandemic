namespace CoronaVirusPandemic.Models
{
    public class CoronavirusCountry
    {
        public string CountryName { get; set; }
        public int CaseCount { get; set; }
        public int TodayCasesCount { get; set; }
        public int DeathsCount { get; set; }
        public int TodayDeathsCount { get; set; }
        public int RecoveredCount { get; set; }
        public int ActiveCount { get; set; }
        public int CriticalCount { get; set; }
        public string FlagUri { get; set; }
    }
}
