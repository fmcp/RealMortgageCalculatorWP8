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
using Microsoft.Phone.Tasks;

namespace RealMortgageCalculator
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();

            // Lista principal de opciones.
            List<String> stringsList = new List<String>();
            stringsList.Add(AppResources.Calculate);
            stringsList.Add(AppResources.Sign);
            MainMenu.ItemsSource = stringsList;
        }


        private void email_Click(object sender, EventArgs e)
        {
            EmailComposeTask task = new EmailComposeTask();
            task.Subject = AppResources.EmailSubject;
            task.Body = AppResources.EmailContent;
            task.Show();
        }

        private void share_Click(object sender, EventArgs e)
        {
            ShareLinkTask shareLinkTask = new ShareLinkTask();
            shareLinkTask.LinkUri = new Uri("http://www.twitter.com/rmcalc", UriKind.Absolute);
            shareLinkTask.Message = AppResources.EmailContent;
            shareLinkTask.Show();
        }

        private void MainMenuSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Contains(AppResources.Calculate)) {
                NavigationService.Navigate(new Uri("/Calculate.xaml", UriKind.Relative));
            }
            else if (e.AddedItems.Contains(AppResources.Sign))
            {

            }
        }
    }
}