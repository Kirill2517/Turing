using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TuringLogic
{
    public class Error
    {
        public char Value { get; }
        public int State { get; }
        public Result Result { get; }

        public Error(char value = '\0', int state = 0, Result result = Result.Ok)
        {
            Value = value;
            State = state;
            Result = result;
        }

        public Error(Result result)
        {
            Result = result;
        }
    }
}
