using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TuringLogic
{
    public class Tape
    {
        /// <summary>
        /// летна
        /// </summary>
        public readonly Dictionary<int, TapePoint> TapeDic = new Dictionary<int, TapePoint>();
        public readonly int Min;
        public readonly int Max;

        internal Tape(int min, int max)
        {
            Min = min;
            Max = max;

            for (int i = Min; i <= Max; i++)
            {
                Add(i);
            }
        }
        /// <summary>
        /// добавление значения в ленту
        /// </summary>
        /// <param name="key"></param>
        /// <param name="tapePointValue"></param>
        internal void Add(int key, char tapePointValue = ' ')
        {
            TapeDic.Add(key, new TapePoint(tapePointValue));
        }
        /// <summary>
        /// настройка дефолтного слова
        /// </summary>
        /// <param name="wordDefault"></param>
        internal Result SetWordDefault(string wordDefault)
        {
            foreach (var item in TapeDic.Values)
            {
                item.SetValue(' ');
            }
            //изменение дефолтного слова
            for (int i = 0; i < wordDefault.Length; i++)
            {
                TapeDic[i].SetValue(wordDefault[i]);
            }

            return Result.Ok;
        }
    }
}
