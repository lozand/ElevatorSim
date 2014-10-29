using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ElevatorSim
{
    public class Tower
    {
        public Tower()
        {
            Elevators = new List<Elevator>();
        }

        public List<Elevator> Elevators { get; set; }

        public void Call(int sourceFloor, Motion direction)
        {
            foreach (Elevator shaft in Elevators)
            {
                if (shaft.Direction == direction && sourceFloor < shaft.CurrentFloor)
                {
                    shaft.MoveToFloor(sourceFloor);
                }
                else if (shaft.Direction == direction && sourceFloor > shaft.CurrentFloor)
                {
                    shaft.MoveToFloor(sourceFloor);
                }
                else
                {
                    shaft.MoveToFloor(sourceFloor);
                }
            }
        }
    }
}
