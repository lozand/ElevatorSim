using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ElevatorSim;

namespace ElevatorSim
{
    public class Call
    {
        public Call(int floor, Motion direction)
        {
            Floor = floor;
            Direction = direction;
            IsAddressed = false;
        }
        public int Floor { get; set; }
        public Motion Direction { get; set; }
        public bool IsAddressed { get; set; }
    }
}
