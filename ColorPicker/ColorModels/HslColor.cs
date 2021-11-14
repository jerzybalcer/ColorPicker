using ColorPicker.ColorModels.ColorComponents;

namespace ColorPicker.ColorModels
{
    public class HslColor
    {
        public DegreesComponent Hue { get; set; }
        public PercentComponent Saturation { get; set; }
        public PercentComponent Lightness { get; set; }
        public HslColor(int hue, int saturation, int lightness)
        {
            Hue = new DegreesComponent(hue);
            Saturation = new PercentComponent(saturation);
            Lightness = new PercentComponent(lightness);
        }
    }
}