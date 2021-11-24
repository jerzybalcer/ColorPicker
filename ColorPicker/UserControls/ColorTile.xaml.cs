using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ColorPicker.UserControls
{
    /// <summary>
    /// Interaction logic for ColorTile.xaml
    /// </summary>
    public partial class ColorTile : UserControl
    {
        public static readonly DependencyProperty BrushProperty =
   DependencyProperty.Register("Brush", typeof(string), typeof(ColorTile));

        public SolidColorBrush Brush
        {
            get { return (SolidColorBrush)GetValue(BrushProperty); }
            set { SetValue(BrushProperty, value); }
        }
        public ColorTile()
        {
            InitializeComponent();
        }
    }
}
