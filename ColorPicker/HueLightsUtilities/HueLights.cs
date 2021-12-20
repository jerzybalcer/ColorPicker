using ColorPicker.ColorModels;
using Q42.HueApi;
using Q42.HueApi.ColorConverters;
using Q42.HueApi.ColorConverters.HSB;
using Q42.HueApi.Interfaces;
using Q42.HueApi.Models.Bridge;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ColorPicker.HueLightsUtilities
{
    public static class HueLights
    {
        private static ILocalHueClient _client;
        public static bool AlwaysSync { get; set; }
        public static List<Light> SelectedLights { get; set; } = new List<Light>();

        public static void CheckForExistingBridge()
        {
            string appKey = Environment.GetEnvironmentVariable("hueLightsKey", EnvironmentVariableTarget.User);
            string ip = Environment.GetEnvironmentVariable("hueBridgeIp", EnvironmentVariableTarget.User);

            if (appKey != null && ip != null)
            {
                _client = new LocalHueClient(ip);
                _client.Initialize(appKey);
            }
            else
            {
                _client = null;
            }
        }

        public static bool isBridgeConnected()
        {
            if(_client == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static void SetLights(RgbColor color)
        {
            if(_client != null)
            {
                var command = new LightCommand();
                command.On = true;
                command.TurnOn().SetColor(new RGBColor(color.Red.Value, color.Green.Value, color.Blue.Value));

                if(SelectedLights.Count > 0)
                {
                    _client.SendCommandAsync(command, SelectedLights.Select(light => light.Id));
                }
            }
        }

        public static async Task<IEnumerable<Light>> GetAllLights()
        {
            return await _client.GetLightsAsync();
        }

        public static async Task<IEnumerable<LocatedBridge>> SearchForBridges()
        {
            IBridgeLocator locator = new HttpBridgeLocator();
            return await locator.LocateBridgesAsync(TimeSpan.FromSeconds(5));
        }

        public static async Task<bool> ConnectToBridge(LocatedBridge bridge)
        {
            ILocalHueClient client = new LocalHueClient(bridge.IpAddress);
            string appKey;

            try
            {
                appKey = await client.RegisterAsync("ColorPickerWindows", "WindowsPC");

                Environment.SetEnvironmentVariable("hueLightsKey", appKey, EnvironmentVariableTarget.User);
                Environment.SetEnvironmentVariable("hueBridgeIp", bridge.IpAddress, EnvironmentVariableTarget.User);

                client.Initialize(appKey);
                _client = client;

                return true;
            }
            catch
            {
                return false;
            }
        }

        public static void DisconnectFromBridge()
        {
            SelectedLights.Clear();
            AlwaysSync = false;
            _client = null;

            Environment.SetEnvironmentVariable("hueLightsKey", "", EnvironmentVariableTarget.User);
            Environment.SetEnvironmentVariable("hueBridgeIp", "", EnvironmentVariableTarget.User);
        }
    }
}
