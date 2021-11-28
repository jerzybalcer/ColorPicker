using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

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

        public static readonly DependencyProperty HexProperty =
            DependencyProperty.Register("Hex", typeof(string), typeof(ColorTile));

        public SolidColorBrush Hex
        {
            get { return (SolidColorBrush)GetValue(HexProperty); }
            set { SetValue(HexProperty, value); }
        }

        public static readonly DependencyProperty ContrastingBrushProperty =
            DependencyProperty.Register("ContrastingBrush", typeof(string), typeof(ColorTile));

        public SolidColorBrush ContrastingBrush
        {
            get { return (SolidColorBrush)GetValue(ContrastingBrushProperty); }
            set { SetValue(ContrastingBrushProperty, value); }
        }

        public ColorTile()
        {
            InitializeComponent();
        }

        private void UserControl_MouseEnter(object sender, MouseEventArgs e)
        {
            CopyColorHex.Visibility = Visibility.Visible;
        }

        private void UserControl_MouseLeave(object sender, MouseEventArgs e)
        {
            CopyColorHex.Visibility = Visibility.Collapsed;
        }

        private void UserControl_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Clipboard.SetText(ColorHex.Content.ToString());
        }
    }
}
