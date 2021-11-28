using ColorPicker.ColorModels;
using ColorPicker.PickedColors;
using System;
using System.Collections.Generic;
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

        public static SolidColorBrush GetContrastingBrush(int red, int green, int blue, int alpha)
        {
            var brightness = Math.Sqrt(red * red * RedBrightnessConst + green * green * GreenBrightnessConst + blue * blue * BlueBrightnessConst);
            if (brightness > BrightnessContrastThreshold || alpha < AlphaContrastThreshold) return new SolidColorBrush(Colors.Black);
            else return new SolidColorBrush(Colors.White);
        }

        public static List<HsvColor> GetCombinationColors(CombinationType combinationType, HsvColor baseColor)
        {
            var colors = new List<HsvColor>();

            if (combinationType == CombinationType.Complementary)
            {
                colors.Add(new HsvColor(baseColor.Hue.Value + 180, baseColor.Saturation.Value, baseColor.Value.Value));
            }
            else if (combinationType == CombinationType.SplitComplementary)
            {
                colors.Add(new HsvColor(baseColor.Hue.Value + 150, baseColor.Saturation.Value, baseColor.Value.Value));
                colors.Add(new HsvColor(baseColor.Hue.Value + 210, baseColor.Saturation.Value, baseColor.Value.Value));
            }
            else if (combinationType == CombinationType.Shades)
            {
                for (int i = 1; i < 4; i++)
                {
                    colors.Add(new HsvColor(baseColor.Hue.Value, baseColor.Saturation.Value , baseColor.Value.Value - i * 25));
                }
            }
            else if (combinationType == CombinationType.Tints)
            {
                for (int i = 1; i < 4; i++)
                {
                    colors.Add(new HsvColor(baseColor.Hue.Value, baseColor.Saturation.Value - i * 25, baseColor.Value.Value ));
                }
            }
            else if (combinationType == CombinationType.Triadic)
            {
                for (int i = 1; i < 3; i++)
                {
                    colors.Add(new HsvColor(baseColor.Hue.Value + i * 120, baseColor.Saturation.Value, baseColor.Value.Value));
                }
            }
            else if (combinationType == CombinationType.Tetradic)
            {
                for (int i = 1; i < 4; i++)
                {
                    colors.Add(new HsvColor(baseColor.Hue.Value + i * 90, baseColor.Saturation.Value, baseColor.Value.Value));
                }
            }
            else if (combinationType == CombinationType.Analogous)
            {
                colors.Add(new HsvColor(baseColor.Hue.Value + 30, baseColor.Saturation.Value, baseColor.Value.Value));
                colors.Add(new HsvColor(baseColor.Hue.Value - 30, baseColor.Saturation.Value, baseColor.Value.Value));
            }

            return colors;
        }
    }
}
