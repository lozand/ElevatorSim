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
            Tower tower = new Tower();
            tower.Elevators.Add(new Elevator(1));
            Console.WriteLine("Sending Call to floor 10 Up");
            tower.Call(10, Motion.Up);
            Console.WriteLine("Sending Call to floor 5 Down");
            tower.Call(5, Motion.Down);
            Console.WriteLine("done Here");
            Console.Read();
        }
    }
}
