using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.UITest;

namespace Xamarin.TestAutomation.Tests
{
    public static class BackDoorMethod
    {
        public static string GetMethodName(Platform platform, string methodName)
        {
            switch (platform)
            {
                case Platform.Android: return methodName;

                case Platform.iOS: return methodName + ":";

                default: return null;
            }

        }
    }
}
