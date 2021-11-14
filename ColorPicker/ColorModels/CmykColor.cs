using ColorPicker.ColorModels.ColorComponents;

namespace ColorPicker.ColorModels
{
    public class CmykColor
    {
        public PercentComponent Cyan { get; set; }
        public PercentComponent Magenta { get; set; }
        public PercentComponent Yellow { get; set; }
        public PercentComponent Key { get; set; }
        public CmykColor(int cyan, int magenta, int yellow, int key)
        {
            Cyan = new PercentComponent(cyan);
            Magenta = new PercentComponent(magenta);
            Yellow = new PercentComponent(yellow);
            Key = new PercentComponent(key);
        }
    }
}
