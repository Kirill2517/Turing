using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TuringLogic
{
    public enum Move
    {
        Right, Left, None
    }

    public enum Result
    { 
        Ok,
        ErrorNullCommand,
        ErrorTapeIsEnd,
        WordMoreTape, 
        StartPositionMoreTapeMax,
        StartPositionSmallerTapeMin

    }
}