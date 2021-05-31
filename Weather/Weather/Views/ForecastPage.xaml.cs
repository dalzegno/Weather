using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

using System.Collections.ObjectModel;

using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Weather.Models;
using Weather.Services;
using System.ComponentModel;

namespace Weather.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]

    public partial class ForecastPage : ContentPage
    {
        OpenWeatherService service;
        GroupedForecast groupedforecast;
        public string titel { get; set; }
        public ForecastPage()
        {
            InitializeComponent();
            
            service = new OpenWeatherService();
            groupedforecast = new GroupedForecast();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            titel = Title;
            Title = $"Forecast for {Title}";
            
            MainThread.BeginInvokeOnMainThread(async () => {await LoadForecast();});
        }

        private async Task LoadForecast()
        {
            try
            {
                OpenWeatherService ows = new OpenWeatherService();
                var f = await service.GetForecastAsync(titel);
                groupedforecast.Items = f.Items.GroupBy(x => x.dateTime.Date);
                groupedforecast.City = titel;
                lw.ItemsSource = groupedforecast.Items;
            }
            catch
            {
                Title = $"Couldn't find {titel}";
            };
        }

        private async void refresh_Clicked(object sender, EventArgs e)
        {
            lw.ItemsSource = null;
            await Task.Delay(200);
            MainThread.BeginInvokeOnMainThread(async () => { await LoadForecast(); });
        }
    }
}