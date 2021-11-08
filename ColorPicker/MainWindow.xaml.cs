using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

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
            _viewModel.PickNewColor();
            CopiedColorHex.Visibility = Visibility.Hidden;
            CopyColorHex.Visibility = Visibility.Visible;
        }

        private void CopyColorHex_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Clipboard.SetText(ColorHex.Content.ToString());
            CopiedColorHex.Visibility = Visibility.Visible;
            CopyColorHex.Visibility = Visibility.Hidden;
        }
    }
}
