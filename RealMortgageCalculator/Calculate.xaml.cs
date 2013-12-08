using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using RealMortgageCalculator.Resources;

namespace RealMortgageCalculator
{
    public partial class Calculate : PhoneApplicationPage
    {
        public Calculate()
        {
            InitializeComponent();
        }

        private void MonthBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (MonthsBox.Text != String.Empty)
            {
                int months = int.Parse(MonthsBox.Text, System.Globalization.CultureInfo.InvariantCulture.NumberFormat);

                if (months < 0)
                    MonthsBox.Text = "0";
                else if (months > 11)
                {
                    int aux = months;
                    months = months % 12;
                    MonthsBox.Text = months.ToString();
                    int years = 0;
                    if (YearsBox.Text != String.Empty)
                        years = int.Parse(YearsBox.Text, System.Globalization.CultureInfo.InvariantCulture.NumberFormat);

                    years = years + aux / 12;
                    YearsBox.Text = years.ToString();
                }
            }
        }

        void OnClickAccept(object sender, RoutedEventArgs e)
        {
            if (CapitalBox.Text != String.Empty
                && YearsBox.Text != String.Empty
                && MonthsBox.Text != String.Empty 
                && InterestBox.Text != String.Empty)
            {
                NavigationService.Navigate(new Uri("/Results.xaml?capital=" + CapitalBox.Text + "&months=" + MonthsBox.Text + "&interest=" + InterestBox.Text, UriKind.Relative));
            }
            else
            {
                MessageBox.Show(AppResources.EmptyBoxesMessage, AppResources.EmptyBoxesCaption, MessageBoxButton.OK);
            }
        }
    }
}