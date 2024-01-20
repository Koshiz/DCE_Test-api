namespace MontyHallWeb.API.Models
{
    public class SimulationResultEntity
    {

        public bool IsWin { get; set; }
        public string SelectedDoor { get; set; }
        public string RevealedDoor { get; set; }
        public string FinalChoice { get; set; }
    }
}
