﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Weather.Services;
using Weather.Models;
using Weather.Consoles;

namespace Weather.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ConsolePage : ContentPage
    {
        public ConsolePage()
        {
            InitializeComponent();
            myOutput.Text = "";
        }
        public void WriteLine()
        {
            WriteLine("");
        }
        public void WriteLine(string str)
        {
            myOutput.Text += str + "\n";
        }
        public void Write(string str)
        {
            myOutput.Text += str;
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {

            myOutput.Text = "";
            WriteLine("---Start Console program---\n");
            var p = new Program(this);
            //await p.myMain();
            var ows = new OpenWeatherService();
            try
            {
                var f = await ows.GetForecastAsync(myInput.Text);
                await p.PrintForecastToConsole(f);
            }
            catch(Exception ex) { myOutput.Text = $"Error: {ex}"; }
            WriteLine("\n---End Console program---");
        }
    }
}