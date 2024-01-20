using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MontyHallWeb.API.Models;

namespace MontyHallWeb.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SimulationController : ControllerBase
    {
        private readonly Random _random = new Random();

        [HttpPost]
        public ActionResult<IEnumerable<SimulationResultEntity>> RunMontyHallGame([FromBody] SimulationEntity simulationEntity)
        {
            if (simulationEntity == null || simulationEntity.NumberOfAttempts <= 0)
            {
                return BadRequest("Invalid input");
            }

            List<SimulationResultEntity> results = new List<SimulationResultEntity>();

            for (int i = 0; i < simulationEntity.NumberOfAttempts; i++)
            {
                results.Add(SimulateSingleGame(simulationEntity.isDoorChanged));
            }

            return Ok(results);
        }

        private SimulationResultEntity SimulateSingleGame(bool changeDoor)
        {
            int winningDoor = _random.Next(1, 4);
            int selectedDoor = _random.Next(1, 4);

            int revealedDoor = GetRandomUnselectedDoor(selectedDoor, winningDoor);
            int finalChoice = changeDoor ? GetOtherDoor(selectedDoor, revealedDoor) : selectedDoor;

            SimulationResultEntity result = new SimulationResultEntity
            {
                IsWin = finalChoice == winningDoor,
                SelectedDoor = $"Door {selectedDoor}",
                RevealedDoor = $"Door {revealedDoor}",
                FinalChoice = $"Door {finalChoice}"
            };

            return result;
        }

        private int GetRandomUnselectedDoor(int selectedDoor, int winningDoor)
        {
            int[] doors = { 1, 2, 3 };
            Array.Sort(doors);
            doors = Array.FindAll(doors, door => door != selectedDoor && door != winningDoor);
            return doors[_random.Next(doors.Length)];
        }

        private int GetOtherDoor(int selectedDoor, int revealedDoor)
        {
            int[] doors = { 1, 2, 3 };
            Array.Sort(doors);
            doors = Array.FindAll(doors, door => door != selectedDoor && door != revealedDoor);
            return doors[0];
        }
    }
}
