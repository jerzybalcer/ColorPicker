using ColorPicker.ColorModels;
using ColorPicker.ColorModels.ColorComponents;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace ColorPicker
{
    public class PickedColor : INotifyPropertyChanged
    {
        public SolidColorBrush Brush { get; set; }
        public SolidColorBrush ContrastingBrush { get; set; }
        public string Hex { get; set; }
        public EightBitComponent Alpha { get; set; }
        public RgbColor RgbColor { get; set; }
        public HsvColor HsvColor { get; set; }
        public HslColor HslColor { get; set; }
        public CmykColor CmykColor { get; set; }

        public PickedColor()
        {
            Alpha = new EightBitComponent(255);
            RgbColor = new RgbColor(255, 0, 0);
            HsvColor = new HsvColor(360, 100, 100);
            HslColor = new HslColor(0, 100, 50);
            CmykColor = new CmykColor(0, 100, 100, 0);
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
