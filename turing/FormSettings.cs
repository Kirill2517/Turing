using System.Drawing;

namespace turing
{
    public class FormSettings
    {
        public Font Font;
        public Color TapeColor;
        public Color EmptyTapeColor;
        public uint Milliseconds;
        public FormSettings(Font font, Color tapeColor, uint milliseconds = 500)
        {
            Font = font ?? throw new System.ArgumentNullException(nameof(font));
            TapeColor = tapeColor;
            Milliseconds = milliseconds;
        }

        public FormSettings()
        {
            Font = new Font("Microsoft Tai Le", 9f, FontStyle.Regular);
            TapeColor = Color.Red;
            Milliseconds = 500;
            EmptyTapeColor = Color.LightGray;
        }
    }
}