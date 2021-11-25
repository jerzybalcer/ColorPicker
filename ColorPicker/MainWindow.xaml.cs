using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ColorPicker
{
    public partial class MainWindow : Window
    {
        private Picker _picker;
        public MainWindow()
        {
            InitializeComponent();
            _picker = new Picker();
            DataContext = _picker;
        }

        private void ColorSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            //CopiedColorHex.Visibility = Visibility.Hidden;
            //CopyColorHex.Visibility = Visibility.Visible;

            _picker.PickFromRgb();
        }
        private void ColorSlider_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            _picker.ConvertValuesFromRgb();
        }
        private void CopyColorHex_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            //Clipboard.SetText(ColorHex.Content.ToString());
            //CopiedColorHex.Visibility = Visibility.Visible;
            //CopyColorHex.Visibility = Visibility.Hidden;
        }

        private void RgbTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            _picker.ConvertValuesFromRgb();
        }

        private void HsvTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            _picker.PickFromHsv();
            _picker.ConvertValuesFromHsv();
        }
    }
}
