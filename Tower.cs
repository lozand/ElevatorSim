using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatorSim
{
    public class Tower
    {
        public Tower(Log logger)
        {
            logger.WriteToFile("Constructing Tower...");
            Elevators = new List<Elevator>();
            CallList = new List<Call>();
            TowerState = true;
            Task task = new Task(new Action(RunTower));
            task.Start();
            logger.WriteToFile("Done Constructing Tower.");
            log = logger;
        }

        private Log log { get; set; }

        public bool TowerState { get; set; }

        public List<Elevator> Elevators { get; set; }

        public List<Call> CallList { get; set; } // I think this is a good name, but I can't help but think that it's not.

        public void Call(int sourceFloor, Motion direction)
        {
            Call cmd = new Call(sourceFloor, direction);
            CallList.Add(cmd);
            log.WriteToFile(String.Format("Added to CallList: Floor {0}", sourceFloor.ToString()));
        }

        public void RunTower()
        {
            log.WriteToFile("RunTower()");
            while (TowerState)
            {
                log.WriteToFile("looping through RunTower()");    
                var unAddressedCommands = CallList.Where(c => !c.IsAddressed);

                if (unAddressedCommands.Count() > 1)
                {
                    log.WriteToFile("unAddressedCommands > 1");
                }

                foreach (Call call in unAddressedCommands)
                {
                    log.WriteToFile(String.Format("Tower.RunTower.Foreach - Command to floor: {0}", call.Floor.ToString()));
                    var direction = call.Direction;
                    var sourceFloor = call.Floor;
                    var possibleElevators = Elevators.Where(e => e.Direction == Motion.None
                                                                || (e.Direction == direction
                                                                    && ((sourceFloor > e.CurrentFloor && e.Direction == Motion.Down)
                                                                        || sourceFloor < e.CurrentFloor && e.Direction == Motion.Up)));
                    var elevatorToSend = possibleElevators.OrderBy(e => Math.Abs(sourceFloor - e.CurrentFloor)).FirstOrDefault();


                    if (elevatorToSend != null)
                    {
                        call.IsAddressed = true;
                        log.WriteToFile(String.Format("Floor {0} is addressed!!", call.Floor.ToString()));
                        elevatorToSend.AddToCommandQueue(call.Floor);
                        RemoveCall(call);
                    }
                    else
                    {
                        log.WriteToFile(string.Format("Floor {0} is NOT addressed!!", call.Floor.ToString()));
                        call.IsAddressed = false;
                    }
                }
            }

            log.WriteToFile("Done!");

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

        private void RemoveCall(Call call)
        {
            CallList.Remove(call);
        }

    }
}
