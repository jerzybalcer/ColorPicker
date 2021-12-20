using Q42.HueApi.Models.Bridge;
using Q42.HueApi;
using System.Diagnostics;
using System.Windows;
using System.Collections.Generic;
using System.Windows.Controls;

namespace ColorPicker.HueLightsUtilities
{
    /// <summary>
    /// Interaction logic for HueLightsConfigWindow.xaml
    /// </summary>
    public partial class HueLightsConfigWindow : Window
    {
        public HueLightsConfigWindow()
        {
            InitializeComponent();
        }

        private async void LocateBtn_Click(object sender, RoutedEventArgs e)
        {
            var bridges = await HueLights.SearchForBridges();
            FoundBridges.ItemsSource = bridges;
        }

        private async void ConnectBtn_Click(object sender, RoutedEventArgs e)
        {
            var selectedBridge = FoundBridges.SelectedItem as LocatedBridge;

            if (selectedBridge == null) return;

            var connectingResult = await HueLights.ConnectToBridge(selectedBridge);

            if (connectingResult == false)
            {
                MessageBox.Show("Press link button on the Hue Bridge before connecting!");
            }
            else
            {
                AfterConnecting.Visibility = Visibility.Visible;
                BeforeConnecting.Visibility = Visibility.Collapsed;

                AllLights.ItemsSource = await HueLights.GetAllLights();
                SelectedLights.ItemsSource = HueLights.SelectedLights;
            }
        }

        private async void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            if (HueLights.isBridgeConnected() == true)
            {
                AfterConnecting.Visibility = Visibility.Visible;
                BeforeConnecting.Visibility = Visibility.Collapsed;

                AllLights.ItemsSource = await HueLights.GetAllLights();
                SelectedLights.ItemsSource = HueLights.SelectedLights;
            }
        }

        private void Window_Closed(object sender, System.EventArgs e)
        {
            HueLights.AlwaysSync = (bool)AlwaysSyncCheck.IsChecked;

            HueLights.SelectedLights = (List<Light>)SelectedLights.ItemsSource;
        }

        private void SelectedLights_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            List<Light> selectedLights = (List<Light>)SelectedLights.ItemsSource;

            if (selectedLights != null)
            {
                Light toRemove = (sender as ListBox).SelectedItem as Light;

                selectedLights.Remove(toRemove);
                SelectedLights.ItemsSource = null;
                SelectedLights.ItemsSource = selectedLights;
            }
        }

        private void AllLights_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            List<Light> selectedLights = (List<Light>)SelectedLights.ItemsSource;

            Light toAdd = (sender as ListBox).SelectedItem as Light;

            foreach (var selected in selectedLights)
            {
                if (toAdd.Id == selected.Id) return;
            }

            selectedLights.Add(toAdd);
            SelectedLights.ItemsSource = null;
            SelectedLights.ItemsSource = selectedLights;
        }

        private void DisconnectBtn_Click(object sender, RoutedEventArgs e)
        {
            HueLights.DisconnectFromBridge();
            AfterConnecting.Visibility = Visibility.Collapsed;
            BeforeConnecting.Visibility = Visibility.Visible;
        }
    }
}
