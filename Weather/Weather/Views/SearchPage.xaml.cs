using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Weather.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SearchPage : ContentPage
    {
        public SearchPage()
        {
            InitializeComponent();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            var city = new MainPageFlyoutMenuItem { Id = 11, Title = CityInput.Text, TargetType = typeof(ForecastPage) };
            var page = (Page)Activator.CreateInstance(city.TargetType);
            page.Title = city.Title;
            Navigation.PushAsync(page);
        }
    }
}