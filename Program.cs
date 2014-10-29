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
            Tower tower = new Tower();
            tower.Elevators.Add(new Elevator(1));
            tower.Call(20, Motion.Up);
            Console.Read();
        }
    }
}
