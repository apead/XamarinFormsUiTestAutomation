using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;
using Xamarin.TestAutomation.Testing;
using Xamarin.Testing.Core.Helpers;

namespace Xamarin.TestAutomation.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //

        [Export("GetFormsViewModelProperty:")] // notice the colon at the end of the method name
        public NSString GetFormsViewModelProperty(NSString value)
        {
            var propertyName = value.ToString();
            var vm = FormsTestAutomationHelper.Instance.ResolveTopLevelViewModel();
            return new NSString(ReflectionHelper.GetPropertyValueAsString(vm, value.ToString()));
        }

        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            #if ENABLE_TEST_CLOUD
            Xamarin.Calabash.Start();
            #endif

            global::Xamarin.Forms.Forms.Init();
            LoadApplication(new App());

            return base.FinishedLaunching(app, options);
        }
    }
}
