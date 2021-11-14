using ColorPicker.ColorModels;
using ColorPicker.ColorModels.ColorComponents;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows.Media;

namespace ColorPicker
{
    public class PickerViewModel : INotifyPropertyChanged
    {
        public EightBitComponent Alpha { get; set; } = new(255);

        public SolidColorBrush PickedColorBrush { get; set; }
        public SolidColorBrush PickedHexTextColor { get; set; }
        public string PickedColorHex => "#" + Alpha.Value.ToString("X2") + RgbColor.Red.Value.ToString("X2") 
            + RgbColor.Green.Value.ToString("X2") + RgbColor.Blue.Value.ToString("X2");

        public RgbColor RgbColor { get; set; }
        public HsvColor HsvColor { get; set; }
        public HslColor HslColor { get; set; }
        public CmykColor CmykColor { get; set; }

        public void PickNewColor()
        {
            PickedColorBrush = new SolidColorBrush(
                Color.FromArgb((byte)Alpha.Value, (byte)RgbColor.Red.Value, (byte)RgbColor.Green.Value, (byte)RgbColor.Blue.Value)
                );
            PickedHexTextColor = ColorProcessor.PickTextColor(RgbColor.Red.Value, RgbColor.Green.Value, RgbColor.Blue.Value, Alpha.Value);
            OnPropertyChanged(nameof(PickedColorHex));

        }

        public void ConvertValues()
        {
            HsvColor = RgbColor.ToHsvColor();
            HslColor = RgbColor.ToHslColor();
            CmykColor = RgbColor.ToCmykColor();
        }

        public PickerViewModel()
        {
            RgbColor = new RgbColor(0,0,0);
            HsvColor = new HsvColor(0,0,0);
            HslColor = new HslColor(0,0,0);
            CmykColor = new CmykColor(0, 0, 0, 100);
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
