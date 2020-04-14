using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TuringLogic;
using System.Xml;
using System.Xml.Serialization;
using System.Text.RegularExpressions;
using System.Runtime.InteropServices;

namespace turing
{
    public partial class Form1 : Form
    {
        UIController UIController;
        List<Button> ButtonsTapePoint;

        [DllImport("user32.dll")]
        static extern bool HideCaret(IntPtr hWnd);

        public Form1()
        {
            InitializeComponent();
            UIController = new UIController();
            UIController.Controls.AddRange(new List<Control>() { label1, label2, panel1, dataGridView1, Description, Log, CurrentStateL, logLabel });
            UIController.UpdateControls();
            Runblock();
            Restartblock();
            ButtonOffBorder(ref ChangeDefaultWord);
            ButtonOffBorder(ref runButton);
            ButtonOffBorder(ref stopButton);
            ButtonOffBorder(ref restartButton);
            ButtonOffBorder(ref addNewState);
            ButtonOffBorder(ref AddNewSimToAlph);
        }

        #region MethodsStyle
        #region buttonsBlock
        private void Restartblock()
        {
            restartButton.Enabled = false;
            RestartToolStripMenuItem.Enabled = false;
        }

        private void Runblock()
        {
            runToolStripMenuItem.Enabled = false;
            runButton.Enabled = false;
        }

        private void Stopblock()
        {
            stopButton.Enabled = false;
            StopToolStripMenuItem.Enabled = false;
        }

        private void RestartUnBlock()
        {
            restartButton.Enabled = true;
            RestartToolStripMenuItem.Enabled = true;
        }

        private void RunUnblock()
        {
            runButton.Enabled = true;
            runToolStripMenuItem.Enabled = true;
        }

        private void StopUnblock()
        {
            stopButton.Enabled = true;
            StopToolStripMenuItem.Enabled = true;
        }
        #endregion

        #region Speed
        private void оченьБыстроToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UIController.UpdateMilliseconds(25);
        }

        private void быстроToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UIController.UpdateMilliseconds(100);
        }

        private void среднеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UIController.UpdateMilliseconds(300);
        }

        private void медленноToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UIController.UpdateMilliseconds(500);
        }

        private void оченьМедленноToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UIController.UpdateMilliseconds(700);
        }
        #endregion

        private void ButtonOffBorder(ref Button button)
        {
            button.FlatAppearance.BorderSize = 0;
            button.FlatStyle = FlatStyle.Flat;
        }

        private void VisibleInit()
        {
            runButton.Visible = true;
            stopButton.Visible = true;
            restartButton.Visible = true;
            addNewState.Visible = true;
            addToAlphTextBox.Visible = true;
            label3.Visible = true;
            AddNewSimToAlph.Visible = true;
            DefaultWordtextBox.Visible = true;
            ChangeDefaultWord.Visible = true;
            label4.Visible = true;
            SpeedToolStripMenuItem.Enabled = true;
            button1.Visible = true;
        }
        #endregion

        #region Events

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            HideCaret(Log.Handle);
        }

        private void Log_MouseDown(object sender, MouseEventArgs e)
        {
            HideCaret(Log.Handle);
            if (e.Button == MouseButtons.Right)
            {
                var clear = new ToolStripButton("Очистить");
                clear.Click += ClearLog_Click;
                var copy = new ToolStripButton("Скопировать");
                copy.Click += CopyLog_Click;

                var contextMenuStrip = new ContextMenuStrip();
                contextMenuStrip.Items.AddRange(new ToolStripItem[] { clear, copy });

                contextMenuStrip.Show(MousePosition, ToolStripDropDownDirection.Right);
            }
        }

        private void CopyLog_Click(object sender, EventArgs e)
        {
            if (sender is ToolStripButton)
            {
                try
                {
                    Clipboard.SetText(Log.SelectedText, TextDataFormat.UnicodeText);
                }
                catch { };
            }
            HideCaret(Log.Handle);

        }

        private void ClearLog_Click(object sender, EventArgs e)
        {
            Log.Text = string.Empty;
            HideCaret(Log.Handle);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CurrentStateL.Visible = false;
            Stopblock();

        }

        /// <summary>
        /// нажатие на любую кнопку ленты для установки текущей позиции головки
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void Button_Click(object sender, EventArgs e)
        {
            await Task.Run(() =>
            {
                var button = sender as Button;
                UIController.UpdateCurrentTapePoint((int)button.Tag);
                ChangeTuringAsync();
            });
        }

        private void настройкиToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void установитьЗначенияМашиныToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!(UIController is null) && !(UIController.Turing is null)) UIController.Turing.StopWork();
            SettingsTuringForm settingsForm = new SettingsTuringForm();
            if (settingsForm.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    var settings = settingsForm.GetSettings();
                    SettingsTuringDefault tSettings = settings;
                    if (tSettings is null)
                        throw new NullReferenceException();
                    UIController.SetSettings(tSettings);
                    ShowTuringInterface();
                }
                catch (NullReferenceException)
                {
                    return;
                }
            }
            if (!(UIController is null) && !(UIController.Turing is null)) UIController.Turing.ContinueWork();
        }



        string PreviousCommand = "";
        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dataGridView = (sender as DataGridView);
            var value = dataGridView[e.ColumnIndex, e.RowIndex].Value;
            if (value is null)
            {
                UIController.Turing.AddCommand(e.ColumnIndex, e.RowIndex + 1, null);
                return;
            }

            string valueString = value.ToString();

            if (!Command.TryParse(valueString, out Command command) || !UIController.Turing.AddCommand(e.ColumnIndex, e.RowIndex + 1, command))
            {
                dataGridView[e.ColumnIndex, e.RowIndex].Value = PreviousCommand.Equals("null") ? null : PreviousCommand;
                return;
            }
        }

        private void dataGridView1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            object value = dataGridView1[e.ColumnIndex, e.RowIndex].Value;
            if (value is null)
                PreviousCommand = "null";
            else PreviousCommand = value.ToString();
        }

        private async void runToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StopUnblock();
            Runblock();
            if (!UIController.Turing.isWork) UIController.Turing.ContinueWork();
            var result = await UIController.Turing.WorkMachine(ChangeTuringAsync);
            if (result.Result == Result.ErrorNullCommand)
                PrintErrorToLog($"Команды со значением \"{result.Value}\" и состоянием {result.State} не существует!", Color.Red);
            else if (result.Result == Result.ErrorTapeIsEnd)
                PrintErrorToLog("Закончилась лента!", Color.Red);
            RunUnblock();
            RestartUnBlock();
            Stopblock();
        }

        private void начатьСНачалаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RunUnblock();
            UIController.Restart();
            ChangeTuringAsync();
            Log.Text = "";
        }

        private void остановитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UIController.Turing.StopWork();
        }

        private void настройкиФормыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //SettingsForm settingsForm = new SettingsForm();
            //if (settingsForm.ShowDialog() == DialogResult.OK)
            //{
            //    UIController.FormSettings = settingsForm.GetFormSettings();
            //    UIController.UpdateControls();
            //}
        }

        private void AddColumnToTable_Click(object sender, EventArgs e)
        {
            UIController.AddState();
            CreateTableTuring();
            GetTuringCommandToTable();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            UIController.DeleteState();
            CreateTableTuring();
            GetTuringCommandToTable();
        }

        private void AddNewSimToAlph_Click(object sender, EventArgs e)
        {
            if (addToAlphTextBox.Text.Equals(string.Empty)) return;
            UIController.AddStringToAlph(addToAlphTextBox.Text);
            CreateTableTuring();
            GetTuringCommandToTable();
            addToAlphTextBox.Text = string.Empty;
        }

        private void ChangeDefaultWord_Click(object sender, EventArgs e)
        {
            UIController.SetWordDefault(DefaultWordtextBox.Text);
            ChangeTuringAsync();
        }

        private void сохранитьКакToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (UIController.Turing is null || UIController.Settings is null)
                return;
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                var fileStream = new FileStream(saveFileDialog1.FileName, FileMode.OpenOrCreate);
                using (StreamWriter streamWriter = new StreamWriter(fileStream, Encoding.Default))
                {
                    StringBuilder stringBuilder = new StringBuilder();
                    stringBuilder.AppendLine("<Table>");
                    stringBuilder.AppendLine($"\t<countState>{UIController.Settings.countState}</countState>");
                    stringBuilder.AppendLine($"\t<maxValueTape>{UIController.Settings.maxValueTape}</maxValueTape>");
                    stringBuilder.AppendLine($"\t<minValueTape>{UIController.Settings.minValueTape}</minValueTape>");
                    stringBuilder.AppendLine($"\t<wordDefault>{UIController.Settings.wordDefault}</wordDefault>");
                    stringBuilder.AppendLine($"\t<startPosition>{UIController.Settings.startPosition}</startPosition>");
                    stringBuilder.Append("<Alphabet>");
                    for (int i = 0; i < UIController.Turing.TableCommand.AlphabetNames.Count; i++)
                    {
                        stringBuilder.Append(UIController.Turing.TableCommand.AlphabetNames[i]);
                    }
                    stringBuilder.AppendLine("<Alphabet>");
                    for (int i = 1; i < UIController.Turing.TableCommand.Commands.GetLength(0); i++)
                    {
                        for (int j = 1; j < UIController.Turing.TableCommand.Commands.GetLength(1); j++)
                        {
                            Command command = UIController.Turing.TableCommand.Commands[i, j];

                            if (command is null)
                            {
                                stringBuilder.Append("\t" + "null");
                                continue;
                            }

                            stringBuilder.Append("\t" + command);
                        }
                        stringBuilder.AppendLine();
                    }
                    stringBuilder.AppendLine("</Table>");

                    stringBuilder.AppendLine("<Description>");
                    stringBuilder.AppendLine(Description.Text);
                    stringBuilder.AppendLine("<Description>");

                    streamWriter.WriteLine(stringBuilder);
                }
            }
        }

        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                using (StreamReader reader = new StreamReader(openFileDialog1.FileName, Encoding.Default))
                {
                    if (reader.ReadLine() == "<Table>")
                    {
                        var count = GetIntFromFile(reader.ReadLine().Trim('\t', '\0'));
                        var max = GetIntFromFile(reader.ReadLine().Trim('\t', '\0'));
                        var min = GetIntFromFile(reader.ReadLine().Trim('\t', '\0'));
                        var wordDef = GetStringFromFile(reader.ReadLine().Trim('\t', '\0'));
                        var start = GetIntFromFile(reader.ReadLine().Trim('\t', '\0'));
                        string alp = GetStringFromFile(reader.ReadLine().Trim('\t', '\0'));

                        SettingsTuringDefault settingsTuringDefault = new SettingsTuringDefault(new HashSet<char>(alp), countState: count - 1, minValueTape: min,
                            maxValueTape: max, wordDefault: wordDef, startPosition: start);

                        var currentString = "";
                        int row = 1;
                        UIController.SetSettings(settingsTuringDefault);
                        while (currentString.Trim('\t', '\0') != "</Table>")
                        {
                            currentString = reader.ReadLine();
                            string[] commandsRow = currentString.Split('\t', '\0');
                            for (int i = 1; i <= commandsRow.Length; i++)
                            {
                                if (Command.TryParse(commandsRow[i - 1], out var command))
                                    UIController.Turing.AddCommand(i - 1, row, command);
                            }
                            row++;
                        }
                        ShowTuringInterface();
                        Description.Text = GetStringFromFile(reader.ReadToEnd());
                    }
                }
            }
        }

        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        #endregion

        #region Methods

        private void PrintErrorToLog(string error, Color color)
        {
            var startIndex = Log.Text.Length;
            Log.AppendText(error);
            Log.Select(startIndex, error.Length);
            Log.SelectionColor = color;
            Log.AppendText("\n");
        }

        /// <summary>
        /// Генерируем ленту
        /// </summary>
        private void GenerateTape(int min, int max)
        {
            int x = 0;
            for (int i = min; i <= max; i++)
            {
                x = SetPoint(i, lastX: x);
            }
            UIController.UpdateControls();
        }

        private int SetPoint(int index, char value = ' ', int lastX = 0)
        {
            const int Width = 45;
            int x = lastX + 2;
            lastX += Width;

            Label label = new Label()
            {
                Font = UIController.FormSettings.Font,
                Text = index.ToString(),
                Width = Width,
                Location = new Point(x, panel1.Height / 2 - 30),
                TextAlign = ContentAlignment.BottomCenter,
            };
            UIController.Controls.Add(label);
            panel1.Controls.Add(label);

            const int Height = 35;
            Button button = new Button()
            {
                Text = value.ToString(),
                BackColor = UIController.FormSettings.EmptyTapeColor,
                Font = UIController.FormSettings.Font,
                Size = new Size(Width, Height),
                Location = new Point(x, panel1.Height / 2),
                TextAlign = ContentAlignment.MiddleCenter,
                TabStop = false,
                Tag = index,
                Enabled = true
            };
            button.Click += Button_Click;
            ButtonOffBorder(ref button);
            UIController.Controls.Add(button);
            ButtonsTapePoint.Add(button);
            panel1.Controls.Add(button);
            return lastX;
        }

        private async void ShowTuringInterface()
        {
            ButtonsTapePoint = new List<Button>();
            Description.Text = "";
            Log.Text = "";
            panel1.Controls.Clear();
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();
            VisibleInit();

            RunUnblock();
            RestartUnBlock();
            GenerateTape(UIController.Settings.minValueTape, UIController.Settings.maxValueTape);
            CurrentStateL.Visible = true;
            await Task.Run(() =>
            {
                ChangeTuringAsync();
            });

            CreateTableTuring();
        }

        private void GetTuringCommandToTable()
        {
            for (int i = 1; i < UIController.Turing.TableCommand.Commands.GetLength(1); i++)
            {
                for (int j = 0; j < UIController.Turing.TableCommand.Commands.GetLength(0) - 1; j++)
                {
                    var command = UIController.Turing.TableCommand.Commands[j + 1, i];
                    if (command is null) continue;
                    else
                        dataGridView1[i, j].Value = command.ToString();
                }
            }
        }

        private void CreateTableTuring()
        {
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();
            var AlphNames = UIController.Turing.TableCommand.AlphabetNames;
            var States = UIController.Turing.TableCommand.NamesState;

            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("Алфавит: ");
            foreach (var item in AlphNames)
            {
                if (item is '\0') continue;
                stringBuilder.Append(item);
            }
            AlphL.Text = stringBuilder.ToString();

            foreach (var column in States)
            {
                dataGridView1.Columns.Add(column.ToString(), $"Q{column}");
            }
            dataGridView1.Rows.Add(AlphNames.Count - 1);
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                dataGridView1.Rows[i].Cells[0].Value = AlphNames[i + 1];
                if (AlphNames[i + 1] is ' ')
                {
                    dataGridView1.Rows[i].Cells[0].Value = "__";
                }
            }
            for (int i = 0; i < dataGridView1.Columns.Count; i++)
            {
                dataGridView1.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            dataGridView1.Columns[0].ReadOnly = true;
        }

        private async void ChangeTuringAsync()
        {
            //Description.Text = UIController.Turing.Milliseconds.ToString();
            //выставляем состояние
            int currentState = UIController.Turing.CurrentState;
            //логирование
            var word = UIController.Turing.CurrentWord;
            StringBuilder state = new StringBuilder();
            state.AppendLine($"Состояние = Q{currentState}");
            var indexCurrentValue = Math.Abs(UIController.Turing.Tape.Min) + UIController.Turing.CurrentTapePoint;
            StringBuilder wordSB = new StringBuilder(word);
            wordSB.Insert(indexCurrentValue, "(");
            wordSB.Insert(indexCurrentValue + 2, ")");
            wordSB.AppendLine();

            try
            {
                Log.AppendText(state + wordSB.ToString().Trim(' ') + '\n');
                CurrentStateL.Text = $"Q{currentState}";
            }
            catch
            {
            }

            //ставим головку
            await Task.Run(() =>
             {
                 var button = ButtonsTapePoint.Single(i => (int)i.Tag == UIController.Turing.CurrentTapePoint);
                 button.BackColor = UIController.FormSettings.TapeColor;

                 foreach (var item in ButtonsTapePoint)
                 {
                     if ((int)item.Tag != UIController.Turing.CurrentTapePoint)
                     {
                         item.BackColor = UIController.FormSettings.EmptyTapeColor;
                     }
                 }

                 //значения ленты
                 try
                 {
                     for (int i = 0; i < UIController.Turing.Tape.TapeDic.Count; i++)
                     {
                         ButtonsTapePoint[i].Text = UIController.Turing.Tape.TapeDic.ElementAt(i).Value.Value.ToString();
                     }
                     panel1.ScrollControlIntoView(button);
                 }
                 catch
                 {
                 }
             });
        }

        private int GetIntFromFile(string regexString)
        {
            Regex regex = new Regex(@"[-]?\d+");
            var value = Convert.ToInt32(regex.Match(regexString).Value);
            return value;
        }

        private string GetStringFromFile(string regexString)
        {
            var regex = new Regex(@">(.*)<", RegexOptions.Multiline | RegexOptions.Singleline);
            regexString = regexString.Trim('\t', '\n', '\r');
            var str = regex.Match(regexString).Value;
            str = str.Remove(0, 1);
            str = str.Remove(str.Length - 1);
            str = str.Trim('\t', '\n', '\r');
            return str;
        }
        #endregion

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
        }

        private void dataGridView1_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            //если выделена ячейка
            var count = dataGridView1.SelectedRows.Count;
            var contextMenuStrip = new ContextMenuStrip();
            if (e.Button == MouseButtons.Right)
            {
                if (count == 0)
                {
                    ToolStripMenuItem toolBarButton = new ToolStripMenuItem("Выделить всю строку");
                    toolBarButton.Click += ToolBarButton_Click;

                    contextMenuStrip.Items.Add(toolBarButton);

                    contextMenuStrip.Show(MousePosition, ToolStripDropDownDirection.Right);
                }
                //если выделены строки
                else if (count > 0)
                {
                    var d = count == 1 ? "у" : "и";
                    ToolStripMenuItem deleteRowsButton = new ToolStripMenuItem($"Удалить строк{d}");
                    deleteRowsButton.Click += DeleteRowsButton_Click;

                    contextMenuStrip.Items.Add(deleteRowsButton);

                    contextMenuStrip.Show(MousePosition, ToolStripDropDownDirection.Right);
                }
            }
        }



        private void DeleteRowsButton_Click(object sender, EventArgs e)
        {
            var hashset = new HashSet<char>();

            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                if (row.Index == dataGridView1.RowCount - 1) continue;
                char item = Convert.ToChar(row.Cells[0].Value);
                hashset.Add(item);
            }

            UIController.Turing.DeleteValuesFromAlph(hashset);
            CreateTableTuring();
            ChangeTuringAsync();
            UIController.UpdateSettings();
        }

        private void ToolBarButton_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewCell item in dataGridView1.SelectedCells)
            {
                var indexrow = item.RowIndex;
                if (indexrow != dataGridView1.RowCount - 1)
                    dataGridView1.Rows[indexrow].Selected = true;
            }
        }
    }
}
