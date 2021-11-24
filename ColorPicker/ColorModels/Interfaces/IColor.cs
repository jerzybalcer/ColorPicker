using System.Windows.Media;

namespace ColorPicker.ColorModels.Interfaces
{
    public interface IColor
    {
        public SolidColorBrush ToSolidColorBrush(int alphaVal = 255);

        public string ToHexString(bool isAlphaIncluded, int alphaVal = 255);
    }
}
