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

        void OnClickAccept(object sender, RoutedEventArgs e)
        {
            if (CapitalBox.Text != String.Empty 
                && MonthsBox.Text != String.Empty 
                && InterestBox.Text != String.Empty)
            {
                float capital = float.Parse(CapitalBox.Text, System.Globalization.CultureInfo.InvariantCulture.NumberFormat);
                float months = float.Parse(MonthsBox.Text, System.Globalization.CultureInfo.InvariantCulture.NumberFormat);
                float interest = float.Parse(InterestBox.Text, System.Globalization.CultureInfo.InvariantCulture.NumberFormat);
            }
            else
            {
                MessageBox.Show(AppResources.EmptyBoxesMessage, AppResources.EmptyBoxesCaption, MessageBoxButton.OK);
            }
        }
    }
}