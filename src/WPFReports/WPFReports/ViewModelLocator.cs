using GalaSoft.MvvmLight.Ioc;
using WPFReports.ViewModels;

namespace WPFReports
{
    public class ViewModelLocator
    {
        public ViewModelLocator()
        {
            var bootstrap = new Bootstrap();
            bootstrap.Initialize(SimpleIoc.Default);
        }


        public ShellViewModel Shell
        {
            get
            {
                return SimpleIoc.Default.GetInstance<ShellViewModel>();
            }
        }
    }
}
