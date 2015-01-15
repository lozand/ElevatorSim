using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace ElevatorSim
{
    class Program
    {
        static void Main(string[] args)
        {
            Log log = new Log();
            Tower tower = new Tower(log);
            tower.Elevators.Add(new Elevator(1, log, 1));
            tower.Elevators.Add(new Elevator(1, log, 2));
            tower.Call(14, Motion.Up);
            Thread.Sleep(4000);
            tower.Call(5, Motion.Down);
            bool isDone = false;
            Thread.Sleep(12000);
            tower.Call(3, Motion.Up);
            isDone = false;
            while (!isDone)
            {
                isDone = tower.Elevators.Where(e => e.Direction == Motion.Up || e.Direction == Motion.Down).FirstOrDefault() == null
                && tower.CallList.Count == 0
                && tower.Elevators.Where(e => e.CommandQueue.Count == 0).Count() == tower.Elevators.Count;
            }
            log.WriteToFile("done with simulation!", "g");

            foreach (Elevator e in tower.Elevators)
            {
                e.Terminate();
            }
            tower.Terminate();
        }
    }
}
