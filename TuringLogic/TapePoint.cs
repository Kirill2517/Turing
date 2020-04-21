using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TuringLogic
{
    public class TapePoint
    {
        public bool HasValue { get; private set; }
        public char Value { get; private set; }
        internal TapePoint(char value = ' ')
        {
            SetValue(value);
        }

        internal void SetValue(char value)
        {
            if (value is ' ')
                HasValue = false;
            else HasValue = true;
            Value = value;
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}