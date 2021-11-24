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
        private PickerViewModel _viewModel;
        public MainWindow()
        {
            InitializeComponent();
            _viewModel = new PickerViewModel();
            DataContext = _viewModel;
        }

        private void ColorSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            //CopiedColorHex.Visibility = Visibility.Hidden;
            //CopyColorHex.Visibility = Visibility.Visible;

            _viewModel.PickFromRgb();
        }
        private void ColorSlider_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            _viewModel.ConvertValuesFromRgb();
        }
        private void CopyColorHex_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            //Clipboard.SetText(ColorHex.Content.ToString());
            //CopiedColorHex.Visibility = Visibility.Visible;
            //CopyColorHex.Visibility = Visibility.Hidden;
        }

        private void RgbTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            _viewModel.ConvertValuesFromRgb();
        }

        private void HsvTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            _viewModel.PickFromHsv();
            _viewModel.ConvertValuesFromHsv();
        }
    }
}
