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
    public partial class SettingsForm : Form
    {
        public SettingsForm()
        {
            InitializeComponent();
        }
        FormSettings FormSettings = UIController.Instance.FormSettings;

        private void SettingsForm_Load(object sender, EventArgs e)
        {
            UIController.Instance.Controls.AddRange(new List<Control>() { label1 });
            UIController.Instance.UpdateControls();
        }

        private void ColorB_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
               FormSettings.TapeColor = colorDialog1.Color;
            }
        }

        private void FontB_Click(object sender, EventArgs e)
        {
            if (fontDialog1.ShowDialog() == DialogResult.OK)
            {
                FormSettings.Font = fontDialog1.Font;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
           // FormSettings.Milliseconds = (int)numericUpDown1.Value;
        }

        public FormSettings GetFormSettings()
        {
            return FormSettings;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                FormSettings.EmptyTapeColor = colorDialog1.Color;
            }
        }
    }
}
