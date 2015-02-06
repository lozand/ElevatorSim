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
        public Elevator(int currentFloor, Log log, int id)
        {
            log.WriteToFile(String.Format("Constructing Elevator {0}...", id.ToString()),"r");
            CurrentFloor = currentFloor;
            Direction = Motion.None;
            CommandQueue = new List<Command>();
            ElevatorState = true;
            IsAddingCommand = false;
            this.log = log;
            Thread thread = new Thread(new ThreadStart(RunElevator));
            thread.Start();
            log.WriteToFile(String.Format("Done Constructing Elevator {0}.", id.ToString()),"r");
            Id = id;
        }

        public List<Command> CommandQueue { get; set; }
        public int CurrentFloor { get; set; }
        public int LastFloor { get; set; }
        public bool IsAddingCommand { get; set; }
        public bool InbetweenFloors { get; set; }
        public Motion Direction { get; set; }
        public int? StopAt { get; set; }
        const int StoppingThreshhold = 2;
        public int Id { get; set; }

        Log log;

        private bool ElevatorState { get; set; }

        public void AddToCommandQueue(int floor)
        {
            IsAddingCommand = true;
            log.WriteToFile(string.Format("Adding to CommandList: Floor {0}", floor.ToString()),"i");
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
            log.WriteToFile("Starting RunElevator() in new thread","r");
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
                        if (nextFloor > CurrentFloor)
                        {
                            GoUpOne(cmd.Floor);
                        }
                        else
                        {
                            GoDownOne(cmd.Floor);
                        }
                    }
                    else
                    {
                        //Direction = Motion.None;
                    }
                }
                else
                {
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
                                RemoveFromCommandQueue(cmd);
                            }
                        }
                        successfulCheck = true;
                    }
                }

            }
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

        public void GoUpOne(int destination = 0)
        {
            LastFloor = CurrentFloor;
            InbetweenFloors = true;
            Thread.Sleep(2000);
            InbetweenFloors = false;
            CurrentFloor++;
            ShowFloor(destination);
        }

        public void GoDownOne(int destination = 0)
        {
            LastFloor = CurrentFloor;
            InbetweenFloors = true;
            Thread.Sleep(2000);
            InbetweenFloors = false;
            CurrentFloor--;
            ShowFloor(destination);
        }

        private void ShowFloor(int destination = 0)
        {
            if (destination != 0)
            {
                log.WriteToFile(String.Format("Elevator {0} - Floor: {1} - Destination: {2}", this.Id.ToString(), CurrentFloor.ToString(), destination), "l");
            }
            else
            {
                log.WriteToFile(String.Format("Elevator {0} - Floor: {1} - Destination: ???", this.Id.ToString(), CurrentFloor.ToString()),"l");
            }
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
                return Motion.None;
            }
        }

        public void Terminate()
        {
            ElevatorState = false;
        }
    }
}
