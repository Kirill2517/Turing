using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TuringLogic;
namespace turing
{
    internal class UIController
    {
        /// <summary>
        /// синголтон
        /// </summary>
        public static UIController Instance { get; private set; }
        public SettingsTuringDefault Settings { get; private set; }
        public Turing Turing { get; private set; }
        public FormSettings FormSettings;

        public List<Control> Controls = new List<Control>();

        public UIController()
        {
            Instance = this;
            FormSettings = new FormSettings();
        }

        public void UpdateMilliseconds(uint value)
        {
            this.FormSettings.Milliseconds = value;
            this.Turing.Milliseconds = this.FormSettings.Milliseconds;
        }

        public void UpdateControls()
        {
            foreach (var item in Controls)
            {
                item.Font = FormSettings.Font;
            }
        }

        public void SetSettings(SettingsTuringDefault settings)
        {
            Settings = settings ?? throw new ArgumentNullException(nameof(settings));
            Turing = new Turing(Settings)
            {
                Milliseconds = FormSettings.Milliseconds
            };
            UpdateSettings();
        }

        public void Restart()
        {
            Turing.StopWork();
            Turing = new Turing(Settings, Turing.TableCommand)
            {
                Milliseconds = FormSettings.Milliseconds
            };
        }

        public void UpdateSettings()
        {
            Settings = Turing.SettingsTuring;
        }

        public void UpdateCurrentTapePoint(int currentTapePoint)
        {
            this.Turing.CurrentTapePoint = currentTapePoint;
            UpdateSettings();
        }

        internal void AddState()
        {
            Turing.AddState();
            UpdateSettings();
        }

        internal void AddStringToAlph(string text)
        {
            Turing.AddStringToAlph(text);
            UpdateSettings();
        }

        internal void SetWordDefault(string text)
        {
            Turing.SetWordDefault(text);
            UpdateSettings();
        }

        internal void DeleteState()
        {
            Turing.DeleteState();
            UpdateSettings();
        }
    }
}
