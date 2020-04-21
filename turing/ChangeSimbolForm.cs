using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace turing
{
    public partial class ChangeSimbolForm : Form
    {
        private readonly int keyIndex;

        public ChangeSimbolForm(int keyIndex)
        {
            InitializeComponent();

            AutoSize = false;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Text = "Задайте значение ячейки";
            ShowInTaskbar = false;
            MaximumSize = new Size(Screen.PrimaryScreen.Bounds.Width / 3, 100);
            AutoScroll = false;
            VerticalScroll.Enabled = false;
            VerticalScroll.Visible = false;
            VerticalScroll.Maximum = 0;
            AutoScroll = true;
            this.keyIndex = keyIndex;
        }
        public int a;
        private void ChangeSimbolForm_Load(object sender, EventArgs e)
        {
            CreateSimbolsButton();
        }

        private void CreateSimbolsButton()
        {
            int k = 0;
            foreach (var item in UIController.Instance.Turing.Alphabet)
            {
                if (item is '\0') continue;
                Button button = new DoubleButton()
                {
                    Text = item.ToString(),
                    Size = new Size(35, 35),
                    Location = new Point(k * 35, 0),
                    DialogResult = DialogResult.OK
                };
                k++;

                button.Click += Button_Click;

                ButtonOffBorder(ref button);
                this.Controls.Add(button);
            }
        }

        private void Button_Click(object sender, EventArgs e)
        {
            UIController.Instance.Turing.SetValueToTapePoint(keyIndex, (sender as Button).Text[0]);
        }

        private void ButtonOffBorder(ref Button button)
        {
            button.FlatAppearance.BorderSize = 0;
            button.FlatStyle = FlatStyle.Flat;
        }


    }
}
