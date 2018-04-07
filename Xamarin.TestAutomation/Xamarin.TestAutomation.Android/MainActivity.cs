using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Linq;
using Xamarin.Forms;
using System.Reflection;
using Xamarin.TestAutomation.Views;
using Xamarin.TestAutomation.Testing;
using Xamarin.Testing.Core.Helpers;

namespace Xamarin.TestAutomation.Droid
{
    [Activity(Label = "Xamarin.TestAutomation", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);
            LoadApplication(new App());
        }

        [Java.Interop.Export("GetFormsViewModelProperty")]
        public Java.Lang.String GetFormsViewModelProperty(Java.Lang.String query)
        {
           var propertyName = query.ToString();
           var vm = FormsTestAutomationHelper.Instance.ResolveTopLevelViewModel();
           return new Java.Lang.String(ReflectionHelper.GetPropertyValueAsString(vm, query.ToString()));
        }


        [Java.Interop.Export("EchoProperty")]
        public Java.Lang.String EchoProperty(Java.Lang.String value)
        {
            return value;
        }
    }
}

