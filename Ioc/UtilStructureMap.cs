using StructureMap;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WpfApp
{
    /// <summary>
    /// Xaml view model to view registration.
    /// </summary>
    public interface IRegisterViewModelToView
    {
        Type TypeView { get; }

        Type TypeViewModel { get; }
    }

    /// <summary>
    /// Type safe xaml view model to view registration.
    /// </summary>
    public class RegisterViewModelToView<TViewModel, TView> : 
        IRegisterViewModelToView where TViewModel : 
        INotifyPropertyChanged where TView : ContentControl
    {
        public Type TypeView => typeof(TView);

        public Type TypeViewModel => typeof(TViewModel);
    }

    /// <summary>
    /// StructureMap help functions to manage xaml view model to view registration. See also: https://structuremap.github.io/
    /// </summary>
    public static class UtilStructureMap
    {
        /// <summary>
        /// Scan and find all IRegisterViewModelToView declarations.
        /// </summary>
        public static void RegisterViewModelToView(ConfigurationExpression configuration)
        {
            configuration.Scan(scanner => { scanner.TheCallingAssembly(); scanner.AddAllTypesOf<IRegisterViewModelToView>(); });
        }

        /// <summary>
        /// Returns xaml view for view model.
        /// </summary>
        public static ContentControl CreateView(StructureMap.Container container, INotifyPropertyChanged viewModel)
        {
            ContentControl result = null;
            foreach (var item in container.GetAllInstances<IRegisterViewModelToView>())
            {
                if (item.TypeViewModel == viewModel.GetType())
                {
                    result = (ContentControl)container.GetInstance(item.TypeView);
                }
            }
            result.DataContext = result;
            return result;
        }

        /// <summary>
        /// Returns view model for xaml view.
        /// </summary>
        public static INotifyPropertyChanged CreateViewModel(StructureMap.Container container, ContentControl view)
        {
            INotifyPropertyChanged result = null;
            foreach (var item in container.GetAllInstances<IRegisterViewModelToView>())
            {
                if (item.TypeView == view.GetType())
                {
                    if (item.TypeViewModel != null)
                    {
                        result = (INotifyPropertyChanged)container.GetInstance(item.TypeViewModel);
                    }
                }
            }
            view.DataContext = result;
            return result;
        }
    }
}
