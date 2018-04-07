using System;
using System.Windows.Input;

using Xamarin.Forms;

namespace Xamarin.TestAutomation.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        private bool _isSigned = true;

        public AboutViewModel()
        {
            Title = "About";

            OpenWebCommand = new Command(() => Device.OpenUri(new Uri("https://xamarin.com/platform")));
        }

        public ICommand OpenWebCommand { get; }

        public bool IsSigned
        {
            get { return _isSigned; }
            set { SetProperty(ref _isSigned, value); }
        }
    }
}