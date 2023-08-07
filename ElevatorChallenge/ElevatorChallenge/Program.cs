//Enums for Elevator States
enum Direction
{
    Up,
    Down,
    Idle
}
// Interfaces
public interface IElevator
{
    void MoveToFloor(int targetFloor,int passengers);
}

public interface IElevatorController
{
    void AddElevator(Elevator elevator);
    int AssignElevator(int floorNumber);
}

// Classes
public class Elevator : IElevator
{
    public int elevatorID;

    public int CurrentFloor;
    private Direction CurrentDirection { get; set; } = Direction.Idle;
    public int Passengers { get; set; } = 0;

    public void MoveToFloor(int targetFloor,int Passengers)
    {
        if (targetFloor > CurrentFloor)
        {
            CurrentDirection = Direction.Up;
            for (int floor = CurrentFloor + 1; floor <= targetFloor; floor++)
            {
                CurrentFloor = floor;
                Console.WriteLine($"Elevator number {elevatorID} is on floor {CurrentFloor} Moving {Direction.Up} with {Passengers} Pasangers");
                Thread.Sleep(2000); // Simulate travel time
            }
        }
        else if (targetFloor < CurrentFloor)
        {
            CurrentDirection = Direction.Down;
            for (int floor = CurrentFloor - 1; floor >= targetFloor; floor--)
            {
                CurrentFloor = floor;
                Console.WriteLine($"Elevator number {elevatorID} is on floor {CurrentFloor} Moving {Direction.Down} with {Passengers} Pasangers");
                Thread.Sleep(2000); // Simulating travel time
            }
        }

        CurrentDirection = Direction.Idle;
    }
}

public class ElevatorController : IElevatorController
{
    private List<Elevator> Elevators = new List<Elevator>();

    public void AddElevator(Elevator elevator)
    {
        Elevators.Add(elevator);
    }
    
    public int AssignElevator(int floorNumber)
    {
        // Find the closest elevator and send it to the floor requested
        var closestNumber = Elevators[0].CurrentFloor;
        int minDifference = Math.Abs(floorNumber - closestNumber);
        int closestElevatorId = 0;

        foreach (var elevator in Elevators)
        {
            int difference = Math.Abs(floorNumber - elevator.CurrentFloor);
            if (difference < minDifference)
            {
                minDifference = difference;
                closestNumber = elevator.CurrentFloor;
                closestElevatorId = elevator.elevatorID;
            }
        }

        return closestElevatorId;
    }
}

public class FloorButton
{
    private int FloorNumber;

    public void PressButton()
    {
        // Implement logic to request an elevator to this floor
    }
}

// Main Program
class Program
{
    static void Main(string[] args)
    {
       
        const int numFloors = 10;
        const int maxPassengers = 15;
        
        IElevatorController elevatorController = new ElevatorController();

        // Create and add elevators to the controller
        var elevator1 = new Elevator() { elevatorID = 1, CurrentFloor = 0 };
        var elevator2 = new Elevator() { elevatorID = 2, CurrentFloor = 3 };
        var elevator3 = new Elevator() { elevatorID = 3, CurrentFloor = 7 };

        elevatorController.AddElevator(elevator1);
        elevatorController.AddElevator(elevator2);
        elevatorController.AddElevator(elevator3);

        //Create and use floor buttons
        //FloorButton floorButton1 = new FloorButton();
        //FloorButton floorButton2 = new FloorButton();
        //FloorButton floorButton3 = new FloorButton();

        while (true)
        {
            Console.WriteLine("Enter Floor Number where you are requesting from (1 to 10):or Press 0 to exit");
            int currentFloorNum = int.Parse(Console.ReadLine());

            if (currentFloorNum == 0)
                break;
                if (currentFloorNum < 1 || currentFloorNum > numFloors)
                {
                    Console.WriteLine("Invalid floor number. Please enter a valid floor.");
                     continue;
                }
            //Send Closest Elevator
            Console.WriteLine($"Finding the closest Elevator to you");
            var assignedElevator = elevatorController.AssignElevator(currentFloorNum);

            Console.WriteLine($"Elevator Moving to requested floor {currentFloorNum}");

            if (assignedElevator == 1)
                elevator1.MoveToFloor(currentFloorNum, 0);
            else if (assignedElevator == 2)
                elevator2.MoveToFloor(currentFloorNum, 0);
            else if (assignedElevator == 3)
                elevator3.MoveToFloor(currentFloorNum, 0);
            else
              break;
            Console.WriteLine($"The Elevator has arrived on floor {currentFloorNum}");
            Console.WriteLine("-------------------------------------------------------");
            Console.WriteLine("Enter your Target Floor Number (1 to 10):or Press 0 to exit");
            int targetFloor = int.Parse(Console.ReadLine());

            if (targetFloor == 0)
                break;
                if (targetFloor < 1 || targetFloor > numFloors)
                {
                    Console.WriteLine("Invalid floor number. Please enter a valid floor.");
                     continue;
                }
            Console.WriteLine("Enter the number of passengers waiting on the floor (1 to 15):or Press 0 to exit");
            int numPassengers = int.Parse(Console.ReadLine());

            if (numPassengers == 0)
            break;
            if (numPassengers >= maxPassengers || numPassengers < 1)
            {
                Console.WriteLine("Invalid Entry,Elevator can only hold maximum of 15 People,Please Enter a Valid number for Passengers(1-15)");
                continue;
            }
            if (assignedElevator == 1)
                elevator1.MoveToFloor(targetFloor, numPassengers);
            else if (assignedElevator == 2)
                elevator2.MoveToFloor(targetFloor, numPassengers);
            else if (assignedElevator == 3)
                elevator3.MoveToFloor(targetFloor, numPassengers);

            Console.WriteLine($"--------------------------------------------------------");
            Console.WriteLine($"Elevator has reached floor {targetFloor}");
            Console.WriteLine($"--------------------------------------------------------");
            // Repeat the process for other floors and elevators
        }

    }
}
