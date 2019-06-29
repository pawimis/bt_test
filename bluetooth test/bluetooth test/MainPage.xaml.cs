using Plugin.BluetoothLE;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace bluetooth_test
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            _ = Devices_GetConnected();
        }
        public async Task Devices_GetConnected()
        {
            var ad = CrossBleAdapter.Current;
            var status = ad.Status;
            System.Diagnostics.Debug.WriteLine(status);
            var devices = await ad.GetConnectedDevices();

            if (devices.Count() == 0)
            {
                Debug.WriteLine($"There are no connected Bluetooth devices. Trying to connect a device...");
                var paired = await ad.GetPairedDevices();

                // Get the first paired device
                var device = paired.FirstOrDefault();
                if (device != null)
                {
                    await device.ConnectWait().ToTask();
                    devices = await ad.GetConnectedDevices();
                }
                else
                {
                    Debug.WriteLine($"There are no connected Bluetooth devices. Connect a device and try again.");
                }
            }

            foreach (var device in devices)
            {
                Debug.WriteLine($"Connected Bluetooth Devices: Name={device.Name} UUID={device.Uuid} Connected={device.IsConnected()}");
            }
        }

    }
}
