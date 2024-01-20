namespace MontyHallWeb.API.Models
{
    public class SimulationAttemptResult
    {
        public int AttemptNumber { get; set; }
        public int CarBehindDoor { get; set; }
        public int StartingChoice { get; set; }
        public int RemainingDoor { get; set; }
        public bool IsWin { get; set; }
        public double WinPercentage { get; set; }
    }
}
