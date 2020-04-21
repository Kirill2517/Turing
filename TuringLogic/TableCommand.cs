using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TuringLogic
{
    public class TableCommand
    {
        /// <summary>
        /// i(0) - alphabet.Count,
        /// j(0) - countState
        /// </summary>
        public readonly Command[,] Commands;
        /// <summary>
        /// имена состояние(начинаются от 0)
        /// </summary>
        public readonly List<int> NamesState = new List<int>();
        /// <summary>
        /// алфавит(начинается с 0)
        /// </summary>
        public readonly List<char> AlphabetNames = new List<char>();

        internal TableCommand(int countState, HashSet<char> alphabet)
        {
            Commands = new Command[alphabet.Count, countState];

            for (int i = 0; i < countState; i++)
            {
                NamesState.Add(i);
            }
            
            for (int i = 0; i < alphabet.Count; i++)
            {
                AlphabetNames.Add(alphabet.ElementAt(i));
            }
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < this.Commands.GetLength(0); i++)
            {
                for (int j = 0; j < this.Commands.GetLength(1); j++)
                {
                    if (this.Commands[i, j] is null) stringBuilder.Append("null ");
                    else stringBuilder.Append($"{this.Commands[i, j]} ");
                }
                stringBuilder.AppendLine();
            }

            return stringBuilder.ToString();
        }
    }
}