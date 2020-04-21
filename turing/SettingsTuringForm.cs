using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TuringLogic;

namespace turing
{
    public partial class SettingsTuringForm : Form
    {
        public SettingsTuringForm()
        {
            InitializeComponent();
        }

        private void Settings_Load(object sender, EventArgs e)
        {
            SettingsTuringDefault settingsTuringDefault = new SettingsTuringDefault(new HashSet<char>());
            UIController.Instance.Controls.AddRange(new List<Control>() { label1, label2, label3, label4, label5, label6, alphabetTB, defaultWordTB });
            UIController.Instance.UpdateControls();
            minNUD.Value = settingsTuringDefault.minValueTape;
            maxNUD.Value = settingsTuringDefault.maxValueTape;
            countStateNUD.Value = settingsTuringDefault.countState;
            startPositionNUD.Value = maxNUD.Value - (Math.Abs(minNUD.Value) + Math.Abs(maxNUD.Value)) / 2;
        }

        public SettingsTuringDefault GetSettings()
        {
            HashSet<char> alph = alphabetTB.Text.ToHashSet();
            //if (startPositionNUD.Value > maxNUD.Value || startPositionNUD.Value < minNUD.Value)
            //{ 
            //    MessageBox.Show("Начальная позиция вне границ ленты!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    return null;
            //}
            SettingsTuringDefault settings = new SettingsTuringDefault(alph, (int)maxNUD.Value, (int)minNUD.Value, (int)countStateNUD.Value, (int)startPositionNUD.Value, defaultWordTB.Text);
            return settings;
        }

        private void minNUD_ValueChanged(object sender, EventArgs e)
        {
            startPositionNUD.Minimum = minNUD.Value;
            minNUD.Maximum = maxNUD.Value;
        }

        private void maxNUD_ValueChanged(object sender, EventArgs e)
        {
            startPositionNUD.Maximum = maxNUD.Value;
            maxNUD.Minimum = minNUD.Value;
        }

        private void SaveB_Click(object sender, EventArgs e)
        {

        }
    }
}
