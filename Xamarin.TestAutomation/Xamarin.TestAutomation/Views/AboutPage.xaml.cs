using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.TestAutomation.Testing;

namespace Xamarin.TestAutomation.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AboutPage : ContentPage
	{
		public AboutPage ()
		{
			InitializeComponent ();
		}

        protected override void OnAppearing()
        {
            base.OnAppearing();

#if ENABLE_TEST_CLOUD
            FormsTestAutomationHelper.Instance.SetTopLevelPage(this);
#endif
        }
    }
}