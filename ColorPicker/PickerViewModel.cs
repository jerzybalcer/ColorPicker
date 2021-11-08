using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Media;

namespace ColorPicker
{
    public class PickerViewModel : INotifyPropertyChanged
    {
        private const int AlphaContrastThreshold = 110;
        private const int BrightnessContrastThreshold = 130;

        public ColorComponent Red { get; set; } = new(0);
        public ColorComponent Green { get; set; } = new(0);
        public ColorComponent Blue { get; set; } = new(0);
        public ColorComponent Alpha { get; set; } = new(255);

        public SolidColorBrush PickedColorBrush { get; set; }
        public string PickedColorHex => "#" + Alpha.Value.ToString("X2") + Red.Value.ToString("X2") + Green.Value.ToString("X2") + Blue.Value.ToString("X2");
        public SolidColorBrush PickedHexTextColor
        {
            get
            {
                double brightness = ColorProcessor.CalculateBrightness(Red.Value, Green.Value, Blue.Value);
                if (brightness > BrightnessContrastThreshold || Alpha.Value < AlphaContrastThreshold) return new SolidColorBrush(Colors.Black);
                else return new SolidColorBrush(Colors.White);
            }
        }
        public void PickNewColor()
        {
            PickedColorBrush = new SolidColorBrush(Color.FromArgb((byte)Alpha.Value, (byte)Red.Value, (byte)Green.Value, (byte)Blue.Value));
            OnPropertyChanged(nameof(PickedColorHex));
            OnPropertyChanged(nameof(PickedHexTextColor));
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
