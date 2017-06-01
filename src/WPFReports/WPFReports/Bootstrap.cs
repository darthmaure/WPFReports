using System;
using System.Configuration;
using GalaSoft.MvvmLight.Ioc;
using Statistics.Core.Widgets;
using Statistics.Core.Widgets.Designer;
using Statistics.Core.Widgets.Services;
using WPFReports.Services;
using WPFReports.ViewModels;
using WPFReports.Views;

namespace WPFReports
{
    public class Bootstrap
    {
        private const string assembliesKey = "compilerAssemblies";
        private const string reportsDirectoryKey = "reportsDirectory";


        public void Initialize(ISimpleIoc container)
        {

            container.Register<ShellViewModel>();

            container.Register<INavigationService>(
                () => new NavigationService(SimpleIoc.Default.GetInstance<ShellViewModel>()), true);
            container.Register<INotificationService>(
                ()=> new NotificationService(SimpleIoc.Default.GetInstance<ShellViewModel>()), true);


            var assemblies = GetConfigValue(assembliesKey).Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
            var reportsDirectory = GetConfigValue(reportsDirectoryKey);

            container.Register<IWidgetCompiler>(() => new WidgetCompiler(assemblies));
            container.Register<IWidgetManagerService>(() => new WidgetManagerService(reportsDirectory, Properties.Snippets.DefaultCode, Properties.Snippets.DefaultLayout));
            container.Register<IWidgetService, WidgetService>();
            container.Register<ISnippetService, SnippetService>();
            container.Register<IDialogService>(() => new DialogService(() => new ResultsWindow()));
            container.Register<IDesignerControlCreatorService, DesignerControlCreatorService>();
            container.Register<INameCreationService, NameCreationService>();

            container.Register<MainViewModel>(true);
            container.Register<DisplayViewModel>();
            container.Register<WidgetCompileResultViewModel>();
            container.Register<WidgetListViewModel>();
            container.Register<DesignerHostViewModel>();
            container.Register<DesignerViewModel>();

            container.Register<ICollectectionCreatorService, CollectionCreatorService>();

        }


        private string GetConfigValue(string name) => ConfigurationManager.AppSettings[name];
    }
}

