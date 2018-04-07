using System;
using System.IO;
using System.Linq;
using NUnit.Framework;
using Xamarin.TestAutomation.ControlHelpers;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace Xamarin.TestAutomation.Tests
{
    [TestFixture(Platform.Android)]
    [TestFixture(Platform.iOS)]
    public class Tests
    {
        IApp app;
        Platform platform;

        public Tests(Platform platform)
        {
            this.platform = platform;
        }

        [SetUp]
        public void BeforeEachTest()
        {

             
            app = AppInitializer.StartApp(platform);
        }

 /*       [Test]
        public void Repl()
        {
            app.Repl();
        }
*/
        [Test]
        public void ScrollToItem99AndNavigate()
        {
            var containerId = "ItemsListView";
            var listItemText = "Item 50";

            app.Screenshot("List Page");
            app.ScrollDownTo(x => x.Marked(listItemText), c => c.Marked(containerId), ScrollStrategy.Gesture, 0.67, 1000, true, new TimeSpan(0, 5, 0));
            var item = app.Query(x => x.Marked(listItemText));

            if (item.Count() > 0)
            {
                app.Tap(x => x.Marked(listItemText));
                app.WaitForElement(x => x.Marked("txtText"),timeout: new TimeSpan(0,0,30));
                app.Screenshot("Item Page");

                var textItem = app.Query(x => x.Marked("txtText"));
                Assert.AreEqual(listItemText, textItem[0].Text);
            }
            else
                Assert.Fail("Item Not in List");

        }

        [Test]
        public void UsingObjectStateConditionsCheckBoxDefaultTrue()
        {
            app.Screenshot("List Page");
            app.Tap(x => x.Marked("About"));
            app.WaitForElement(x => x.Marked("Learn more"));
            app.Screenshot("About Page");
            Assert.AreEqual(app.Invoke(BackDoorMethod.GetMethodName(platform,"GetFormsViewModelProperty"), "IsSigned").ToString(),"True");
        }

        [Test]
        public void UsingObjectStateConditionsCheckBoxTapChangesState()
        {
            app.Screenshot("List Page");
            app.Tap(x => x.Marked("About"));
            app.WaitForElement(x => x.Marked("Learn more"));
            app.Screenshot("About Page");
            app.Tap(x => x.Marked("uxAgreeCheckBox"));
            app.Screenshot("About Page");
            Assert.AreEqual(app.Invoke(BackDoorMethod.GetMethodName(platform,"GetFormsViewModelProperty"), "IsSigned").ToString(), "False");
        }


        [Test]
        public void UsingUiControlStateConditionsCheckBoxDefaultTrue()
        {
            app.Screenshot("List Page");
            app.Tap(x => x.Marked("About"));
            app.WaitForElement(x => x.Marked("Learn more"));
            app.Screenshot("About Page");

            Assert.IsTrue(CheckBoxTestingHelper.IsChecked(app, "uxAgreeCheckBox"));
        }

        [Test]
        public void UsingUiStateConditionsCheckBoxTapChangesState()
        {
            app.Screenshot("List Page");
            app.Tap(x => x.Marked("About"));
            app.WaitForElement(x => x.Marked("Learn more"));
            app.Screenshot("About Page");
            app.Tap(x => x.Marked("uxAgreeCheckBox"));
            app.Screenshot("About Page");

            Assert.IsFalse(CheckBoxTestingHelper.IsChecked(app, "uxAgreeCheckBox"));
        }
    }
}

