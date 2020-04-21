using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
namespace TuringLogic
{
    public class Command
    {
        public char ChangeSim { get; private set; }
        public Move Move { get; private set; }
        public int State { get; private set; }

        internal Command()
        {

        }

        public static bool TryParse(string valueString, out Command command)
        {
            command = new Command();
            if (valueString is null || valueString is "") return false;
            command.ChangeSim = valueString[0];
            if (valueString[1] is '>')
                command.Move = Move.Right;
            else if (valueString[1] is '<')
                command.Move = Move.Left;
            else if (valueString[1] is '=')
                command.Move = Move.None;
            else return false;
            if (int.TryParse(valueString.Substring(2), out var state))
                command.State = state;
            else return false;
            return true;
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(ChangeSim);
            switch (Move)
            {
                case Move.Right:
                    stringBuilder.Append(">");
                    break;
                case Move.Left:
                    stringBuilder.Append("<");
                    break;
                case Move.None:
                    stringBuilder.Append("=");
                    break;
                default:
                    break;
            }
            stringBuilder.Append(State);
            return stringBuilder.ToString();
        }

    }
}