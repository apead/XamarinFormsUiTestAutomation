using System;
using System.IO;
using System.Linq;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace Xamarin.TestAutomation.Tests
{
    public class AppInitializer
    {
        public static IApp StartApp(Platform platform)
        {
            if (platform == Platform.Android)
            {
                return ConfigureApp
                    .Android
                    .DeviceSerial("emulator-5554")
                    .InstalledApp("com.companyname.Xamarin.TestAutomation")
               //     .ApkFile(".\\..\\..\\binaries\\com.companyname.Xamarin.TestAutomation.apk")
                    .StartApp();
            }

            return ConfigureApp
                .iOS
                .DeviceIdentifier("3729817E-BFAD-4A91-80E7-21A1221C7A5D")
                .InstalledApp("com.companyname.Xamarin.TestAutomation")
                .StartApp();
        }
    }
}

