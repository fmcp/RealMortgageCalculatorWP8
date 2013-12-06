using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Threading.Tasks;
using System.Threading;

namespace RealMortgageCalculator
{
    public partial class Results : PhoneApplicationPage
    {

        private float capital, months, interest;
        CalcularInteres calcInt;


        public Results()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            // Getting parameters.
            string msg = "";
            capital = 0;
            months = 0;
            interest = 0;
            if (NavigationContext.QueryString.TryGetValue("capital", out msg))
                capital = float.Parse(msg, System.Globalization.CultureInfo.InvariantCulture.NumberFormat);
            if (NavigationContext.QueryString.TryGetValue("months", out msg))
                months = float.Parse(msg, System.Globalization.CultureInfo.InvariantCulture.NumberFormat);
            if (NavigationContext.QueryString.TryGetValue("interest", out msg))
                interest = float.Parse(msg, System.Globalization.CultureInfo.InvariantCulture.NumberFormat);




        }

        private void Pivot_Loaded(object sender, RoutedEventArgs e)
        {
            calcInt = new CalcularInteres(capital,  interest, (int)months);
            calcInt.calcular();
            interes.Text += " " + String.Format("{0:0.00}", 100 * calcInt.getInteresMes());
            interesAprox.Text += " " + String.Format("{0:0.00}", 100 * calcInt.getInteresMesCV());
            cuota.Text += " " + String.Format("{0:0.00}", calcInt.getCuotaMensual());
            cuotaAprox.Text += " " + String.Format("{0:0.00}", calcInt.getCuotaMensualCV()); ;
            interesAnualAprox.Text += " " + (int)(100 * calcInt.getInteresAnualCV());
            double dif = calcInt.getCuotaMensualCV() - calcInt.getCuotaMensual();
            pagoAnual.Text += " " + String.Format("{0:0.00}", dif * 12);
            pagoTotal.Text += " " + String.Format("{0:0.00}", dif * months);
            loadTables();
        }

        /**
         * Load the amortization tables into the GUI
         * */
        private async void loadTables()
        {
            List<TableElement> ElementsList = await calcInt.calcularMatriz();

            foreach (TableElement te in ElementsList)
                Lista.Items.Add(te);



            progressBar.Visibility = Visibility.Collapsed;
            loading.Visibility = Visibility.Collapsed;

            ElementsList = await calcInt.calcularMatrizCV();
            foreach (TableElement te in ElementsList)
                ListaCV.Items.Add(te);

            progressBar2.Visibility = Visibility.Collapsed;
            loading2.Visibility = Visibility.Collapsed;
        }

    }
}