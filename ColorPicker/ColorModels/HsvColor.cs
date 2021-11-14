using ColorPicker.ColorModels.ColorComponents;

namespace ColorPicker.ColorModels
{
    public class HsvColor
    {
        public DegreesComponent Hue { get; set; }
        public PercentComponent Saturation { get; set; }
        public PercentComponent Value { get; set; }
        public HsvColor(int hue, int saturation, int value)
        {
            Hue = new DegreesComponent(hue);
            Saturation = new PercentComponent(saturation);
            Value = new PercentComponent(value);
        }
    }
}
