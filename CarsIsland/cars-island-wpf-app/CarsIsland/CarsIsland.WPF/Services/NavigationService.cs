using CarsIsland.WPF.Core.Config;
using CarsIsland.WPF.Core.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace CarsIsland.WPF.Services
{
    public class NavigationService : INavigationService
    {
        private readonly Dictionary<AppViews, Type> _pagesByKey = new Dictionary<AppViews, Type>();
        private Page _rootView;

        public void Configure(AppViews viewKey, Type pageType)
        {
            lock (_pagesByKey)
            {
                if (_pagesByKey.ContainsKey(viewKey))
                {
                    _pagesByKey[viewKey] = pageType;
                }
                else
                {
                    _pagesByKey.Add(viewKey, pageType);
                }
            }
        }

        public void InitializeRootView(object rootView)
        {
            _rootView = rootView as Page;
        }

        public void NavigateTo(AppViews viewKey)
        {
            NavigateTo(viewKey, null);
        }

        public void NavigateTo(AppViews viewKey, object parameter)
        {
            lock (_pagesByKey)
            {

                if (_pagesByKey.ContainsKey(viewKey))
                {
                    var type = _pagesByKey[viewKey];
                    ConstructorInfo constructor;
                    object[] parameters = new object[] { };

                    constructor = type.GetTypeInfo()
                        .DeclaredConstructors
                        .FirstOrDefault();

                    if (constructor == null)
                    {
                        throw new InvalidOperationException(
                            "No suitable constructor found for page " + viewKey);
                    }

                    if(parameter != null)
                    {
                        parameters = new object[] { parameter };
                    }

                    var page = constructor.Invoke(parameters);

                    if(page.GetType().IsSubclassOf(typeof(Window)))
                    {
                       var test = page as Window;
                        test.Show();
                    }

                    else
                    {
                        if (parameter == null)
                        {
                            _rootView.NavigationService.Navigate(page);
                        }
                        else
                        {
                            _rootView.NavigationService.Navigate(page, parameter);
                        }
                    }
                }
                else
                {
                    throw new ArgumentException(
                        string.Format(
                            "No such page: {0}. Did you forget to call NavigationService.Configure?",
                            viewKey), nameof(viewKey));
                }
            }
        }
    }
}
