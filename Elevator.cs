using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace ElevatorSim
{
    public class Elevator
    {
        public Elevator(int currentFloor)
        {
            CurrentFloor = currentFloor;
            Direction = Motion.None;
        }

        public int CurrentFloor { get; set; }
        public int LastFloor { get; set; }
        public int InbetweenFloors { get; set; }
        public Motion Direction { get; set; }
        public int? StopAt { get; set; }

        Log log = new Log();

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
            Thread.Sleep(2000);
            CurrentFloor++;
            ShowFloor();
        }

        public void GoDownOne()
        {
            LastFloor = CurrentFloor;
            Thread.Sleep(2000);
            CurrentFloor--;
            ShowFloor();
        }

        private void ShowFloor()
        {
            Console.WriteLine("I'm on floor {0}", CurrentFloor.ToString());
            log.WriteToFile(String.Format("I'm on floor {0}", CurrentFloor.ToString()));
        }
    }
}
