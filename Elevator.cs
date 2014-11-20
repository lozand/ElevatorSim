using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ElevatorSim
{
    public class Elevator
    {
        public Elevator(int currentFloor)
        {
            CurrentFloor = currentFloor;
            Direction = Motion.None;
            CommandQueue = new List<Command>();
            ElevatorState = true;
            RunElevator();
        }

        public List<Command> CommandQueue { get; set; }
        public int CurrentFloor { get; set; }
        public int LastFloor { get; set; }
        public bool InbetweenFloors { get; set; }
        public Motion Direction { get; set; }
        public int? StopAt { get; set; }
        const int StoppingThreshhold = 2;

        Log log = new Log();

        public bool ElevatorState { get; set; }

        public void RunElevator()
        {
            while (ElevatorState)
            {
                var direction = GetDirection();
                var nextFloor = 0;
                var cmd = new Command(0);
                if (direction == Motion.Up)
                {
                    cmd = CommandQueue.Where(c => c.Floor > CurrentFloor).OrderBy(c => c.Floor).FirstOrDefault();
                    
                }
                else if (direction == Motion.Down)
                {
                    cmd = CommandQueue.Where(c => c.Floor < CurrentFloor).OrderByDescending(c => c.Floor).FirstOrDefault();
                }

                if (cmd != null)
                {
                    nextFloor = cmd.Floor;
                    if (nextFloor > CurrentFloor)
                    {
                        GoUpOne();
                    }
                    else
                    {
                        GoDownOne();
                    }
                }
                else
                {
                    Direction = Motion.None;
                }
                
            }

            Console.Write("Elevator Done!");
        }

        public void MoveToFloor(int floor)
        {
            ShowFloor();
            if (floor > CurrentFloor)
            {
                Direction = Motion.Up;
                while (floor > CurrentFloor)
                {
                    GoUpOne();
                }
            }
            else
            {
                Direction = Motion.Down;
                while (floor < CurrentFloor)
                {
                    GoDownOne();
                }
            }
            Direction = Motion.None;
        }

        public void GoUpOne()
        {
            LastFloor = CurrentFloor;
            InbetweenFloors = true;
            Thread.Sleep(2000);
            InbetweenFloors = false;
            CurrentFloor++;
            ShowFloor();
        }

        public void GoDownOne()
        {
            LastFloor = CurrentFloor;
            InbetweenFloors = true;
            Thread.Sleep(2000);
            InbetweenFloors = false;
            CurrentFloor--;
            ShowFloor();
        }

        private void ShowFloor()
        {
            Console.WriteLine("I'm on floor {0}", CurrentFloor.ToString());
            log.WriteToFile(String.Format("I'm on floor {0}", CurrentFloor.ToString()));
        }

        private Motion GetDirection()
        {
            if (CommandQueue.FirstOrDefault() != null)
            {
                if (CommandQueue.FirstOrDefault().Floor > CurrentFloor)
                {
                    return Motion.Up;
                }
                else
                {
                    return Motion.Down;
                }
            }
            else
            {
                //return Direction;
                return Motion.None;
            }
        }

        //private void Task AwaitCommand()
        //{
        //    while(Direction != Motion.None)
        //    {

        //    }
        //}
    }
}
