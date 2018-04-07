using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.UITest;
using System.Linq;

namespace Xamarin.TestAutomation.ControlHelpers
{
    public static class CheckBoxTestingHelper
    {

        public static bool IsChecked(IApp app, string automationId)
        {
            return 1 == app.Query(x => x.Marked(automationId).Descendant().Marked("Image_Container")).Count();
        }
    }
}
