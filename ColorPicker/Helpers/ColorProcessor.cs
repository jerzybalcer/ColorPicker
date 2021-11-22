using System;
using System.Windows.Media;

namespace ColorPicker
{
    public static class ColorProcessor
    {
        private const double RedBrightnessConst = 0.241;
        private const double GreenBrightnessConst = 0.691;
        private const double BlueBrightnessConst = 0.068;

        private const int AlphaContrastThreshold = 110;
        private const int BrightnessContrastThreshold = 130;

        public static SolidColorBrush PickContrastingColor(int red, int green, int blue, int alpha)
        {
            var brightness = Math.Sqrt(red * red * RedBrightnessConst + green * green * GreenBrightnessConst + blue * blue * BlueBrightnessConst);
            if (brightness > BrightnessContrastThreshold || alpha < AlphaContrastThreshold) return new SolidColorBrush(Colors.Black);
            else return new SolidColorBrush(Colors.White);
        }
    }
}
