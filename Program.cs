using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ElevatorSim
{
    class Program
    {
        static void Main(string[] args)
        {
            Log log = new Log();
            Tower tower = new Tower(log);
            tower.Elevators.Add(new Elevator(1, log));
            log.WriteToFile("Sending Call to floor 10 Up");
            tower.Call(10, Motion.Up);
            log.WriteToFile("Sending Call to floor 5 Down");
            tower.Call(5, Motion.Down);
            log.WriteToFile("done Here");
            bool isDone = false;
            isDone = tower.Elevators.Where(e => e.Direction == Motion.None).FirstOrDefault() == null 
                && tower.CallList.Count == 0 
                && tower.Elevators.Where(e => e.CommandQueue.Count == 0) == tower.Elevators;
            while (!isDone)
            {
                //continue looping
            }
        }
    }
}
