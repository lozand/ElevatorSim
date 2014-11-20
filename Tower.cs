﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatorSim
{
    public class Tower
    {
        public Tower()
        {
            Elevators = new List<Elevator>();
            CallList = new List<Call>();
            TowerState = true;
            RunTower();
        }

        public bool TowerState { get; set; }

        public List<Elevator> Elevators { get; set; }

        public List<Call> CallList { get; set; } // I think this is a good name, but I can't help but think that it's not.

        public void Call(int sourceFloor, Motion direction)
        {
            Call cmd = new Call(sourceFloor, direction);
            CallList.Add(cmd);
        }

        public void RunTower()
        {
            while (TowerState)
            {
                var unAddressedCommands = CallList.Where(c => !c.IsAddressed);

                foreach (Call cmd in unAddressedCommands)
                {
                    var direction = cmd.Direction;
                    var sourceFloor = cmd.Floor;
                    var possibleElevators = Elevators.Where(e => e.Direction == Motion.None
                                                                || (e.Direction == direction
                                                                    && ((sourceFloor > e.CurrentFloor && e.Direction == Motion.Down)
                                                                        || sourceFloor < e.CurrentFloor && e.Direction == Motion.Up)));
                    var elevatorToSend = possibleElevators.OrderBy(e => Math.Abs(sourceFloor - e.CurrentFloor)).FirstOrDefault();

                    cmd.IsAddressed = true;

                    elevatorToSend.CommandQueue.Add(new Command(cmd.Floor));
                }
            }

            Console.WriteLine("Done!");

            //foreach (Elevator shaft in Elevators)
            //{
            //    if (shaft.Direction == direction && sourceFloor < shaft.CurrentFloor)
            //    {
            //        shaft.MoveToFloor(sourceFloor);
            //    }
            //    else if (shaft.Direction == direction && sourceFloor > shaft.CurrentFloor)
            //    {
            //        shaft.MoveToFloor(sourceFloor);
            //    }
            //    else
            //    {
            //        shaft.MoveToFloor(sourceFloor);
            //    }
            //}
        }        

    }
}
