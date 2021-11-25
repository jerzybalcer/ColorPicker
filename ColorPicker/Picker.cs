using ColorPicker.ColorModels;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Media;

namespace ColorPicker
{
    public class Picker : INotifyPropertyChanged
    {
        public PickedColor PrimaryColor { get; set; }
        public PickedColor ComplementColor1 { get; set; }
        public PickedColor ComplementColor2 { get; set; }
        public PickedColor ComplementColor3 { get; set; }
        public List<PickedColor> Complements { get; set; }

        public Color PickedHue { get; set; }
        public SolidColorBrush PickingEllipseColor { get; set; }
        public bool AlreadyPicked { get; set; }
        public bool IncludeAlpha { get; set; }

        public void PickFromRgb()
        {
            if (AlreadyPicked) return;

            PrimaryColor.Brush = PrimaryColor.RgbColor.ToSolidColorBrush(PrimaryColor.Alpha.Value);
            PrimaryColor.Hex = PrimaryColor.RgbColor.ToHexString(isAlphaIncluded: IncludeAlpha);

            PrimaryColor.ContrastingBrush = ColorProcessor.PickContrastingColor(
                PrimaryColor.RgbColor.Red.Value, PrimaryColor.RgbColor.Green.Value, PrimaryColor.RgbColor.Blue.Value, PrimaryColor.Alpha.Value);
        }
        public void PickFromHsv()
        {
            if (AlreadyPicked) return;

            UpdateHueBrush();
            PickingEllipseColor = PrimaryColor.Brush;

            RgbColor tempRgb = PrimaryColor.HsvColor.ToRgbColor();

            PrimaryColor.Brush = tempRgb.ToSolidColorBrush(PrimaryColor.Alpha.Value);

            PrimaryColor.Hex = tempRgb.ToHexString(isAlphaIncluded: IncludeAlpha);

            PrimaryColor.ContrastingBrush = ColorProcessor.PickContrastingColor(
                tempRgb.Red.Value, tempRgb.Green.Value, tempRgb.Blue.Value, PrimaryColor.Alpha.Value);
        }
        public void ConvertValuesFromRgb()
        {
            AlreadyPicked = true;

            PrimaryColor.HsvColor = PrimaryColor.RgbColor.ToHsvColor();

            PickingEllipseColor = PrimaryColor.Brush;
            UpdateHueBrush();

            PrimaryColor.HslColor = PrimaryColor.RgbColor.ToHslColor();
            PrimaryColor.CmykColor = PrimaryColor.RgbColor.ToCmykColor();

            UpdateComplements();

            AlreadyPicked = false;
        }
        public void ConvertValuesFromHsv()
        {
            AlreadyPicked = true;

            PrimaryColor.RgbColor = PrimaryColor.HsvColor.ToRgbColor();
            PrimaryColor.HslColor = PrimaryColor.RgbColor.ToHslColor();
            PrimaryColor.CmykColor = PrimaryColor.RgbColor.ToCmykColor();

            UpdateComplements();

            AlreadyPicked = false;
        }
        private void UpdateHueBrush()
        {
            RgbColor toRgb = PrimaryColor.HsvColor.MostSaturated.ToRgbColor();
            PickedHue = Color.FromRgb((byte)toRgb.Red.Value, (byte)toRgb.Green.Value, (byte)toRgb.Blue.Value);
        }

        private void UpdateComplements()
        {
            for (var i = 0; i < Complements.Count; i++)
            {
                RgbColor newColor = new HsvColor(PrimaryColor.HsvColor.Hue.Value + 90 * (i + 1),
                    PrimaryColor.HsvColor.Saturation.Value, PrimaryColor.HsvColor.Value.Value).ToRgbColor();

                Complements[i].Brush = newColor.ToSolidColorBrush(PrimaryColor.Alpha.Value);
                Complements[i].Hex = newColor.ToHexString(isAlphaIncluded: IncludeAlpha);
                Complements[i].ContrastingBrush = ColorProcessor.PickContrastingColor(newColor.Red.Value, newColor.Green.Value,
                    newColor.Blue.Value, PrimaryColor.Alpha.Value);
            }
        }

        public Picker()
        {
            PrimaryColor = new();
            ComplementColor1 = new();
            ComplementColor2 = new();
            ComplementColor3 = new();
            Complements = new List<PickedColor> { ComplementColor1, ComplementColor2, ComplementColor3 };
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
