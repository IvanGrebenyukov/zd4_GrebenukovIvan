using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Task4_grebenukov
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ExchangeRates :ContentPage
    {
       
        public ExchangeRates ()
        {
            InitializeComponent();
        }
        
        private void USDChanged (object sender, TextChangedEventArgs e)
        {
            try
            {
                if (double.Parse(usdEntry.Text) >= 0)
                {
                    eurLabel.Text = (double.Parse(usdEntry.Text) * 1.075).ToString();
                } else
                {
                    this.DisplayToastAsync("Введите положительное число", 5000);
                }
            } catch
            {
                this.DisplayToastAsync("Можно вводить только числа", 5000);
            }
        }
    }
}