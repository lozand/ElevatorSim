﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ElevatorSim
{
    public class Elevator
    {
        public Elevator(int currentFloor, Log log)
        {
            log.WriteToFile("Constructing Elevator...");
            CurrentFloor = currentFloor;
            Direction = Motion.None;
            CommandQueue = new List<Command>();
            ElevatorState = true;
            IsAddingCommand = false;
            Thread thread = new Thread(new ThreadStart(RunElevator));
            //Task run = new Task(new Action(RunElevator));
            thread.Start();
            log.WriteToFile("Done Constructing Elevator.");
        }

        public List<Command> CommandQueue { get; set; }
        public int CurrentFloor { get; set; }
        public int LastFloor { get; set; }
        public bool IsAddingCommand { get; set; }
        public bool InbetweenFloors { get; set; }
        public Motion Direction { get; set; }
        public int? StopAt { get; set; }
        const int StoppingThreshhold = 2;

        Log log = new Log();

        public bool ElevatorState { get; set; }

        public void AddToCommandQueue(int floor)
        {
            IsAddingCommand = true;
            log.WriteToFile(string.Format("Adding command to floor {0}", floor.ToString()));
            if (CurrentFloor > floor)
            {
                Direction = Motion.Down;
            }
            else if (CurrentFloor < floor)
            {
                Direction = Motion.Up;
            }
            else
            {
                Direction = Motion.None;
            }
            CommandQueue.Add(new Command(floor));
            IsAddingCommand = false;
        }

        public void RemoveFromCommandQueue(Command cmd)
        {
            CommandQueue.Remove(cmd);
        }

        public void RemoveFromCommandQueue(int floor)
        {
            CommandQueue.RemoveAll(q => q.Floor == floor);
        }

        public void RunElevator()
        {
            log.WriteToFile("Starting RunElevator() in new thread");
            while (ElevatorState)
            {
                if (CommandQueue.Count() != 0)
                {
                    //var direction = GetDirection();
                    var direction = Direction;
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

                    if (cmd != null && cmd.Floor != 0)
                    {
                        nextFloor = cmd.Floor;
                        log.WriteToFile(String.Format("Headed to {0}!!", cmd.Floor.ToString()));
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
                        //log.WriteToFile("no motion");
                        //Direction = Motion.None;
                    }
                }
                else
                {
                    //Console.WriteLine("Command Queue Empty!!");
                    if (!IsAddingCommand)
                    {
                        Direction = Motion.None;
                    }
                }

                bool successfulCheck = false;
                while (!successfulCheck)
                {
                    if (!IsAddingCommand)
                    {
                        if (CommandQueue.Any(q => q.Floor == CurrentFloor))
                        {
                            List<Command> cmdToRemove = new List<Command>();
                            foreach (Command cmd in CommandQueue.Where(q => q.Floor == CurrentFloor))
                            {
                                cmdToRemove.Add(cmd);

                            }
                            foreach (Command cmd in cmdToRemove)
                            {
                                //CommandQueue.Remove(cmd);
                                RemoveFromCommandQueue(cmd);
                            }
                        }
                        successfulCheck = true;
                    }
                }

            }

            log.WriteToFile("Elevator Done!");
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
            //Console.WriteLine("I'm on floor {0}", CurrentFloor.ToString());
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
