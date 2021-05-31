using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Weather.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPageFlyout : ContentPage
    {
        public ListView ListView;

        public MainPageFlyout()
        {
            InitializeComponent();

            BindingContext = new MainPageFlyoutViewModel();
            ListView = MenuItemsListView;
        }

        class MainPageFlyoutViewModel
        {
            public ObservableCollection<MainPageFlyoutMenuItem> MenuItems { get; set; }

            public MainPageFlyoutViewModel()
            {
                MenuItems = new ObservableCollection<MainPageFlyoutMenuItem>(new[]
                {
                    new MainPageFlyoutMenuItem { Id = 0, Title = "Search", TargetType=typeof(SearchPage) },
                    new MainPageFlyoutMenuItem { Id = 1, Title = "Glasgow", TargetType=typeof(ForecastPage) },
                    new MainPageFlyoutMenuItem { Id = 2, Title = "Stockholm", TargetType=typeof(ForecastPage) },
                    new MainPageFlyoutMenuItem { Id = 3, Title = "Saratov", TargetType=typeof(ForecastPage) },
                    new MainPageFlyoutMenuItem { Id = 4, Title = "Addis Ababa", TargetType=typeof(ForecastPage) },
                    new MainPageFlyoutMenuItem { Id = 5, Title = "Maputo", TargetType=typeof(ForecastPage) },
                    new MainPageFlyoutMenuItem { Id = 6, Title = "Geneva", TargetType=typeof(ForecastPage) },
                    new MainPageFlyoutMenuItem { Id = 7, Title = "Paris", TargetType=typeof(ForecastPage) },
                    new MainPageFlyoutMenuItem { Id = 8, Title = "Wellington", TargetType=typeof(ForecastPage) },
                    new MainPageFlyoutMenuItem { Id = 9, Title = "About Weather", TargetType=typeof(AboutPage) },
                    new MainPageFlyoutMenuItem { Id = 10, Title = "Debug Console", TargetType=typeof(ConsolePage) },
                });
            }
        }
    }
}