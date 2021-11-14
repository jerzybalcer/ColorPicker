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

        public static SolidColorBrush PickTextColor(int red, int green, int blue, int alpha)
        {
            var brightness = Math.Sqrt(red * red * RedBrightnessConst + green * green * GreenBrightnessConst + blue * blue * BlueBrightnessConst);
            if (brightness > BrightnessContrastThreshold || alpha < AlphaContrastThreshold) return new SolidColorBrush(Colors.Black);
            else return new SolidColorBrush(Colors.White);
        }

        public static double[] CalculateRgbFractions(int red, int green, int blue)
        {
            double[] fractions = new double[3];
            fractions[0] = (double)red / 255.0d;
            fractions[1] = (double)green / 255.0d;
            fractions[2] = (double)blue / 255.0d;

            return fractions;
        }
    }
}
