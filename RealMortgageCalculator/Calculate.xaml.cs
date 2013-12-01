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
                NavigationService.Navigate(new Uri("/Results.xaml?capital=" + CapitalBox.Text + "&months=" + MonthsBox.Text + "&interest=" + InterestBox.Text, UriKind.Relative));
            }
            else
            {
                MessageBox.Show(AppResources.EmptyBoxesMessage, AppResources.EmptyBoxesCaption, MessageBoxButton.OK);
            }
        }
    }
}