using ColorPicker.ColorModels.ColorComponents;
using ColorPicker.ColorModels.Interfaces;
using System;
using System.Diagnostics;
using System.Windows.Media;

namespace ColorPicker.ColorModels
{
    public class HsvColor : IColor
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
        public HsvColor MostSaturated => new HsvColor(Hue.Value, 100, 100);

        public RgbColor ToRgbColor()
        {
            double sat = (double)Saturation.Value / 100.0d;
            double val = (double)Value.Value / 100.0d;
            double hue = (double)Hue.Value;

            double c = val * sat;
            double x = c * (1 - Math.Abs((hue / 60.0d) % 2 - 1));
            double m = val - c;

            double red = 0, green = 0, blue = 0;

            if (hue >= 0 && hue < 60)
            {
                red = c; green = x; blue = 0;
            }
            else if (hue >= 60 && hue < 120)
            {
                red = x; green = c; blue = 0;
            }
            else if (hue >= 120 && hue < 180)
            {
                red = 0; green = c; blue = x;
            }
            else if (hue >= 180 && hue < 240)
            {
                red = 0; green = x; blue = c;
            }
            else if (hue >= 240 && hue < 300)
            {
                red = x; green = 0; blue = c;
            }
            else if (hue >= 300 && hue <= 360)
            {
                red = c; green = 0; blue = x;
            }

            return new RgbColor((int)((red + m) * 255), (int)((green + m) * 255), (int)((blue + m) * 255));
        }

        public SolidColorBrush ToSolidColorBrush(int alphaVal = 255)
        {
            RgbColor toRgb = this.ToRgbColor();

            return new SolidColorBrush(
                Color.FromArgb(
                    (byte)alphaVal, (byte)toRgb.Red.Value, (byte)toRgb.Green.Value, (byte)toRgb.Blue.Value)
                );
        }
        public string ToHexString(bool isAlphaIncluded, int alphaVal = 255)
        {
            RgbColor toRgb = this.ToRgbColor();

            string alphaHex = "";

            if (isAlphaIncluded)
            {
                alphaHex = alphaVal.ToString("X2");
            }

            return "#" + alphaHex + toRgb.Red.Value.ToString("X2") + toRgb.Green.Value.ToString("X2") + toRgb.Blue.Value.ToString("X2");
        }
    }
}
