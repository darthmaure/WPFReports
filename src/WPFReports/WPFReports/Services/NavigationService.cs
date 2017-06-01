using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using WPFReports.ViewModels;

namespace WPFReports.Services
{
    public interface INavigationService 
    {
        void Navigate(Type target);
        void NavigateBack();
    }

    public class NavigationService : INavigationService
    {
        public NavigationService(ShellViewModel host)
        {
            _host = host;
        }

        private readonly ShellViewModel _host;


        public void Navigate(Type target)
        {
            var targetViewModel = SimpleIoc.Default.GetInstance(target) as ViewModelBase;
            if (targetViewModel == null) return;

            _host.Content = targetViewModel;
        }

        public void NavigateBack()
        {
            _host.Content = SimpleIoc.Default.GetInstance<MainViewModel>();
        }
    }
}
