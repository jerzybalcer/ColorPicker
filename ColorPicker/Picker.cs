using ColorPicker.ColorModels;
using ColorPicker.HueLightsUtilities;
using ColorPicker.PickedColors;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Media;

namespace ColorPicker
{
    public class Picker : INotifyPropertyChanged
    {
        public PickedColor PrimaryColor { get; set; }
        public PickedColor CombinationColor1 { get; set; }
        public PickedColor CombinationColor2 { get; set; }
        public PickedColor CombinationColor3 { get; set; }

        private readonly List<PickedColor> _complements;

        public CombinationType Combination { get; set; }
        public Color PickedHue { get; set; }
        public SolidColorBrush PickingEllipseColor { get; set; }
        public bool AlreadyPicked { get; set; }
        public bool IncludeAlpha { get; set; }

        public void PickFromRgb()
        {
            if (AlreadyPicked) return;

            PrimaryColor.Brush = PrimaryColor.RgbColor.ToSolidColorBrush(PrimaryColor.Alpha.Value);
            PrimaryColor.Hex = PrimaryColor.RgbColor.ToHexString(isAlphaIncluded: IncludeAlpha);

            PrimaryColor.ContrastingBrush = ColorProcessor.GetContrastingBrush(
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

            PrimaryColor.ContrastingBrush = ColorProcessor.GetContrastingBrush(
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

            HueLights.SetLights(PrimaryColor.RgbColor);

            AlreadyPicked = false;
        }
        public void ConvertValuesFromHsv()
        {
            AlreadyPicked = true;

            PrimaryColor.RgbColor = PrimaryColor.HsvColor.ToRgbColor();
            PrimaryColor.HslColor = PrimaryColor.RgbColor.ToHslColor();
            PrimaryColor.CmykColor = PrimaryColor.RgbColor.ToCmykColor();

            UpdateComplements();

            HueLights.SetLights(PrimaryColor.RgbColor);

            AlreadyPicked = false;
        }
        private void UpdateHueBrush()
        {
            RgbColor toRgb = PrimaryColor.HsvColor.MostSaturated.ToRgbColor();
            PickedHue = Color.FromRgb((byte)toRgb.Red.Value, (byte)toRgb.Green.Value, (byte)toRgb.Blue.Value);
        }

        public void UpdateComplements()
        {
            List<HsvColor> combinationColors = ColorProcessor.GetCombinationColors(Combination, PrimaryColor.HsvColor);

            for(int i = 0; i < combinationColors.Count; i++)
            {
                RgbColor toRgb = combinationColors[i].ToRgbColor();
                _complements[i].Brush = toRgb.ToSolidColorBrush(PrimaryColor.Alpha.Value);
                _complements[i].Hex = toRgb.ToHexString(isAlphaIncluded: IncludeAlpha);
                _complements[i].ContrastingBrush = ColorProcessor.GetContrastingBrush(toRgb.Red.Value, toRgb.Green.Value,
                    toRgb.Blue.Value, PrimaryColor.Alpha.Value);
            }
        }

        public Picker()
        {
            PrimaryColor = new();
            CombinationColor1 = new();
            CombinationColor2 = new();
            CombinationColor3 = new();
            _complements = new List<PickedColor> { CombinationColor1, CombinationColor2, CombinationColor3 };
            Combination = CombinationType.Shades;
            UpdateComplements();
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
