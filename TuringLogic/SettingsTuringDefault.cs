using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TuringLogic
{
    public class SettingsTuringDefault
    {
        public readonly int maxValueTape;
        public readonly int minValueTape;
        public readonly HashSet<char> Alphabet;
        public string wordDefault;
        public readonly int countState;
        public readonly int startPosition;
        public readonly uint milliseconds;

        public SettingsTuringDefault(HashSet<char> alphabet, int maxValueTape = 100, int minValueTape = -100, int countState = 2, int startPosition = 0, string wordDefault = "10", uint milliseconds = 500)
        {
            if (alphabet is null)
            {
                throw new ArgumentNullException(nameof(alphabet));
            }

            var alp = alphabet.ToList();
            if (!alp.Contains('\0')) alp.Insert(0, '\0');
            if (!alp.Contains(' ')) alp.Add(' ');
            Alphabet = alp.ToHashSet();

            this.maxValueTape = maxValueTape;
            this.minValueTape = minValueTape;
            this.countState = countState;
            this.wordDefault = wordDefault ?? throw new ArgumentNullException(nameof(wordDefault));
            this.milliseconds = milliseconds;
            this.startPosition = startPosition;
        }
    }
}
