using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ElevatorSim
{
    public class Command
    {
        public Command(int floor)
        {
            Floor = floor;
            IsFulfilled = false;
        }

        public int Floor { get; set; }
        public bool IsFulfilled { get; set; }
    }
}
