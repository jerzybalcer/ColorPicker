using System.Windows;
using ColorPicker.HueLightsUtilities;

namespace ColorPicker
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            HueLights.CheckForExistingBridge();
        }
    }
}
