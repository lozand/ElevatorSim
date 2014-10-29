using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ElevatorSim
{
    // Should this enum be renamed? I was thinking about using "Status", but that may not be the same thing. A status would be more like "Operating" or "Broken" or "Jammed"
    // Would this enum be more accurately captured with a nullable boolean? Perhaps having the enum would be more readable, though. 
    public enum Motion
    {        
        None = 1,
        Up = 2,
        Down = 3

    }
}
