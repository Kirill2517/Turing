using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TuringLogic
{
    public class Turing
    {
        private int currentTapePoint = 0;
        private UInt32 milliseconds;

        #region PublicFields
        /// <summary>
        /// алфавит
        /// </summary>
        public HashSet<char> Alphabet { get; private set; }

        /// <summary>
        /// таблица комманд
        /// </summary>
        public TableCommand TableCommand { get; private set; }

        /// <summary>
        /// количество состояний
        /// </summary>
        public int CountState { get; private set; }

        /// <summary>
        /// Текущее состояние
        /// </summary>
        public int CurrentState { get; private set; } = 1;

        /// <summary>
        /// Текущая позиция головки
        /// </summary>
        public int CurrentTapePoint
        {
            get { return currentTapePoint; }
            set
            {
                if (value >= Tape.Min && value <= Tape.Max)
                    currentTapePoint = value;
            }
        }
        /// <summary>
        /// Показывает состояние работы программы
        /// </summary>
        /// 
        public bool isWork { get; private set; } = false;

        /// <summary>
        /// Лента
        /// </summary>
        public readonly Tape Tape;

        public UInt32 Milliseconds
        {
            get
            {
                return milliseconds;
            }

            set
            {
                if (value >= 0)
                    milliseconds = value;
                else milliseconds = (uint)Math.Abs(value);
            }
        }

        public readonly int StartPosition = 0;

        public SettingsTuringDefault SettingsTuring =>
            new SettingsTuringDefault(Alphabet, Tape.Max, Tape.Min, CountState, StartPosition, WordDefault, Milliseconds);

        public string WordDefault { get; private set; }

        public string CurrentWord
        {
            get
            {
                StringBuilder stringBuilder = new StringBuilder();

                foreach (var item in this.Tape.TapeDic.Values)
                {
                    stringBuilder.Append(item.Value);
                }

                return stringBuilder.ToString();
            }
        }

        #endregion

        internal Turing(int countState, HashSet<char> alphabet)
        {
            if (countState < 1) countState = 1;
            if (alphabet is null) alphabet = new HashSet<char>();
            CountState = countState + 1;

            var alf = alphabet.ToList();
            if (!alf.Contains(' ')) alf.Add(' ');
            alf.Remove('\0');
            alf.Insert(0, '\0');

            Alphabet = alf.ToHashSet();

            TableCommand = new TableCommand(CountState, Alphabet);
        }

        public Turing(SettingsTuringDefault settings) : this(settings.countState, settings.Alphabet)
        {
            if (settings is null)
            {
                throw new ArgumentNullException(nameof(settings));
            }
            int maxValueTape = settings.maxValueTape;
            int minValueTape = settings.minValueTape;
            if (maxValueTape < minValueTape)
            {
                var T = maxValueTape;
                maxValueTape = minValueTape;
                minValueTape = T;
            }
            if (maxValueTape < 0) maxValueTape = Math.Abs(minValueTape);
            Tape = new Tape(minValueTape, maxValueTape);

            CurrentTapePoint = settings.startPosition;
            StartPosition = CurrentTapePoint;

            this.Milliseconds = settings.milliseconds;
            if (!(settings.wordDefault is null))
                SetWordDefault(settings.wordDefault);
        }

        public Turing(SettingsTuringDefault settings, TableCommand table) : this(settings)
        {
            TableCommand = table ?? throw new ArgumentNullException(nameof(table));
        }

        public Result SetWordDefault(string wordDefault = "101")
        {
            //проверка на существование всех символов
            for (int i = 0; i < wordDefault.Length; i++)
            {
                if (!Alphabet.Contains(wordDefault[i]))
                {
                    wordDefault = wordDefault.Remove(i, 1);
                    i--;
                }
            }

            WordDefault = wordDefault;

            return Tape.SetWordDefault(wordDefault);
        }

        /// <summary>
        /// Убивает работу программы
        /// </summary>
        public void StopWork()
        {
            isWork = false;
        }

        public void ContinueWork()
        {
            isWork = true;
        }

        public async Task<Error> WorkMachine(Action print)
        {
            var alph = Alphabet.ToList();
            isWork = true;
            while (CurrentState != 0 && isWork)
            {
                print?.Invoke();
                char value = Tape.TapeDic[CurrentTapePoint].Value;
                int indexValue = alph.IndexOf(value);
                var command = TableCommand.Commands[indexValue, CurrentState];

                if (command is null)
                {
                    isWork = false;
                    return new Error(value, CurrentState, Result.ErrorNullCommand);
                }

                Tape.TapeDic[CurrentTapePoint].SetValue(command.ChangeSim);

                if (command.Move == Move.Left) CurrentTapePoint -= 1;
                else if (command.Move == Move.Right) CurrentTapePoint += 1;
                else if (command.Move == Move.None) CurrentTapePoint = CurrentTapePoint;

                CurrentState = command.State;

                await Task.Delay((int)Milliseconds);

                if (CurrentTapePoint == Tape.Max || CurrentTapePoint == Tape.Min)
                {
                    print.Invoke();
                    isWork = false;
                    return new Error(Result.ErrorTapeIsEnd);
                }
            }

            print?.Invoke();
            isWork = false;
            return new Error(Result.Ok);
        }

        public bool AddCommand(int column, int row, Command command)
        {
            if (command is null)
            {
                TableCommand.Commands[row, column] = null;
                return true;
            }

            if (!Alphabet.Contains(command.ChangeSim)) return false;
            if (column <= 0 || row <= 0 || column > TableCommand.Commands.GetLength(1) || row > TableCommand.Commands.GetLength(0))
                return false;

            if (command.ChangeSim is '\0') return false;
            if (command.State >= CountState || command.State < 0) return false;

            TableCommand.Commands[row, column] = command;

            return true;
        }

        public void AddState()
        {
            CountState++;
            GetNewTablePlus(CountState, Alphabet);
        }

        public void DeleteState()
        {
            if (CountState == 2) return;
            CountState--;
            GetNewTableMinus(CountState, Alphabet);
        }

        public void AddStringToAlph(string letters)
        {
            this.Alphabet = this.Alphabet.Union(letters.ToHashSet()).ToHashSet();
            Alphabet.Remove(' ');
            Alphabet = Alphabet.Append(' ').ToHashSet();
            GetNewTablePlus(CountState, Alphabet);
        }

        public void DeleteValuesFromAlph(HashSet<char> removeAlph)
        {
            //проверка на существование всех символов в алфавите => новый алфавит для удаления создать
            removeAlph = removeAlph.Where(i => Alphabet.Contains(i)).ToHashSet();
            removeAlph.Remove(' ');
            //удалить символы из текущей строки

            WordDefault = new string(WordDefault.Where(i => !removeAlph.Contains(i)).ToArray());

            //удалить удалить символы из алфавита

            foreach (var item in removeAlph)
            {
                Alphabet.Remove(item);
            }
            Tape.SetWordDefault(WordDefault);
            GetNewTableMinus(CountState, Alphabet);
        }

        private void GetNewTablePlus(int countState, HashSet<char> alphabet)
        {
            var table = TableCommand;
            TableCommand = new TableCommand(countState, alphabet);
            for (int i = 1; i < table.Commands.GetLength(0); i++)
            {
                for (int j = 1; j < table.Commands.GetLength(1); j++)
                {
                    TableCommand.Commands[i, j] = table.Commands[i, j];
                }
            }
        }

        private void GetNewTableMinus(int countState, HashSet<char> alphabet)
        {
            var table = TableCommand;
            TableCommand = new TableCommand(countState, alphabet);
            for (int i = 1; i < TableCommand.Commands.GetLength(0); i++)
            {
                for (int j = 1; j < TableCommand.Commands.GetLength(1); j++)
                {
                    TableCommand.Commands[i, j] = table.Commands[i, j];
                }
            }
        }
    }
}
