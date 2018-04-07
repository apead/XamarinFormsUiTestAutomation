using Xamarin.Forms;

namespace Xamarin.TestAutomation.Testing
{
    public class FormsTestAutomationHelper
    {
        public static FormsTestAutomationHelper Instance = new FormsTestAutomationHelper();

        private Page _topLevelPage;


        public Page ResolveLevelPage()
        {
            return _topLevelPage;

        }
        public void SetTopLevelPage(Page page)
        {
            _topLevelPage = page;
        }

        public object ResolveTopLevelViewModel()
        {
            var page = FormsTestAutomationHelper.Instance.ResolveLevelPage();

            if (page != null)
            {
                var vm = page.BindingContext;
                return vm;
            }

            return null;
        }

}
}