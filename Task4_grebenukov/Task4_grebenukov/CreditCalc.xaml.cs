using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Task4_grebenukov
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CreditCalc : ContentPage
    {
        public CreditCalc ()
        {
            InitializeComponent();
        }
        private void SliderValueChange (object sender, ValueChangedEventArgs e)
        {
            SliderLabel.Text = $"{Slider.Value}%";

            if (LoanEntry.Text != "" && MonthEntry.Text != "")
            {
                Calculation(LoanEntry.Text, MonthEntry.Text, PaymentTypePicker.SelectedIndex, Slider.Value);
            } else
            {
                this.DisplayToastAsync("Заполните все поля", 1000);
                
                MonthlyPaymentLabel.Text = "Ежемесячный платеж: ...";
                TotalLabel.Text = "Общая сумма: ...";
                OverpaymentLabel.Text = "Переплата: ...";
            }
        }

        private void Calculation (string loanAmount, string loanTermInMonths, int PickerPayment, double interestRate)
        {
            try
            {
                if (Convert.ToDouble(loanTermInMonths) > 0 && Convert.ToDouble(loanAmount) > 0)
                {

                    double monthlyInterestRate = interestRate / 1200; // перевод процентной ставки в месячную долю
                    double annuityFactor = monthlyInterestRate * Math.Pow(1 + monthlyInterestRate, int.Parse(loanTermInMonths)) / (Math.Pow(1 + monthlyInterestRate, int.Parse(loanTermInMonths)) - 1); // вычисляем коэффициент аннуитета
                    double annuityPayment = Math.Round(double.Parse(loanAmount) * annuityFactor, 2); // вычисляем ежемесячный платеж

                    MonthlyPaymentLabel.Text = $"Ежемесячный платеж: {annuityPayment}";
                    TotalLabel.Text = $"Общая сумма: {Math.Round(annuityPayment, 2) * int.Parse(loanTermInMonths)}";
                    OverpaymentLabel.Text = $"Переплата: {Math.Round(Math.Round(annuityPayment, 2) * int.Parse(loanTermInMonths) - Math.Round(double.Parse(loanAmount), 2), 2)}";


                } else
                {
                    this.DisplayToastAsync("Введите положительное число", 1000);
                    MonthlyPaymentLabel.Text = "Ежемесячный платеж: NULL";
                    TotalLabel.Text = "Общая сумма: NULL";
                    OverpaymentLabel.Text = "Переплата: NULL";
                }
            } catch
            {
                this.DisplayToastAsync("Можно вводить только числа");
            }
        }

    }
}