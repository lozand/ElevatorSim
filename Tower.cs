﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
            IsAssigning = false;
            Thread thread = new Thread(new ThreadStart(RunTower));
            //Task task = new Task(new Action(RunTower));
            thread.Start();
            logger.WriteToFile("Done Constructing Tower.");
            log = logger;
        }

        private Log log { get; set; }

        public bool TowerState { get; set; }

        public bool IsAssigning { get; set; }

        public List<Elevator> Elevators { get; set; }

        public List<Call> CallList { get; set; } // I think this is a good name, but I can't help but think that it's not.

        public void Call(int sourceFloor, Motion direction)
        {
            bool successfullyAddedCall = false;
            Call cmd = new Call(sourceFloor, direction);
            while (!successfullyAddedCall)
            {
                if (!IsAssigning)
                {
                    CallList.Add(cmd);
                    log.WriteToFile(String.Format("Added to CallList: Floor {0}", sourceFloor.ToString()));
                    successfullyAddedCall = true;
                }
            }
        }

        public void RunTower()
        {
            log.WriteToFile("Starting RunTower() in new Thread");
            while (TowerState)
            {
                if (CallList.Count > 0)
                {
                    IsAssigning = true;
                    var unAddressedCommands = CallList.Where(c => !c.IsAddressed);
                    foreach (Call call in unAddressedCommands)
                    {
                        //log.WriteToFile(String.Format("Tower.RunTower.Foreach - Command to floor: {0}", call.Floor.ToString()));
                        var direction = call.Direction;
                        var sourceFloor = call.Floor;
                        var possibleElevators = Elevators.Where(e => e.Direction == Motion.None
                                                                    || (e.Direction == direction
                                                                        && ((sourceFloor > e.CurrentFloor && e.Direction == Motion.Down)
                                                                            || sourceFloor < e.CurrentFloor && e.Direction == Motion.Up)));
                        var elevatorToSend = possibleElevators.OrderBy(e => Math.Abs(sourceFloor - e.CurrentFloor)).FirstOrDefault();


                        if (elevatorToSend != null)
                        {
                            //SetCallAddress(call, true);
                            //call.IsAddressed = true;
                            SetToRemove(call);
                            log.WriteToFile(String.Format("Floor {0} is addressed!!", call.Floor.ToString()));
                            elevatorToSend.AddToCommandQueue(call.Floor);
                            //RemoveCall(call);
                        }
                        else
                        {
                            //log.WriteToFile(string.Format("Floor {0} is NOT addressed!!", call.Floor.ToString()));
                            //SetCallAddress(call, false);
                        }
                    }
                    IsAssigning = false;
                    RemoveCallsSetToRemove();
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

        private List<Call> CallsToRemove = new List<Call>();

        private void SetToRemove(Call call)
        {
            CallsToRemove.Add(call);
        }

        private void RemoveCallsSetToRemove()
        {
            
            foreach (Call call in CallsToRemove)
            {
                //log.WriteToFile("about to remove this shit");
                RemoveCall(call);
            }
            CallsToRemove.Clear();
        }

        private void RemoveCall(Call call)
        {
            //bool successfullyRemovedCall = false;
            //while (!successfullyRemovedCall)
            //{
            //    if (!IsAssigning)
            //    {
                    CallList.Remove(call);
                    log.WriteToFile(string.Format("removed call to floor {0}", call.Floor));
            //        successfullyRemovedCall = true;
            //    }
            //}
        }

        private void SetCallAddress(Call call, bool isAddressed)
        {
            int floor = call.Floor;
            Motion direction = call.Direction;
            var callsToAddress = CallList.Where(l => l.Floor == floor && l.Direction == direction);
            callsToAddress.ToList().ForEach(a =>
            {
                a.IsAddressed = isAddressed;
            });
        }

    }
}
