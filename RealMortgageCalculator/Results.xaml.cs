using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace RealMortgageCalculator
{
    public partial class Results : PhoneApplicationPage
    {

        private float capital, months, interest;

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
            CalcularInteres aux = new CalcularInteres(capital,  interest, (int)months);
            aux.calcular();
            interes.Text += " " + String.Format("{0:0.00}", 100 * aux.getInteresMes());
            interesAprox.Text += " " + String.Format("{0:0.00}",  100*aux.getInteresMesCV());
            cuota.Text += " " + String.Format("{0:0.00}",  aux.getCuotaMensual());
            cuotaAprox.Text += " " + String.Format("{0:0.00}", aux.getCuotaMensualCV()); ;
            interesAnualAprox.Text += " " + (int)(100*aux.getInteresAnualCV());
            double dif = aux.getCuotaMensualCV() - aux.getCuotaMensual();
            pagoAnual.Text += " " + String.Format("{0:0.00}", dif * 12);
            pagoTotal.Text += " " + String.Format("{0:0.00}", dif * months);

            aux.calcularMatriz();

            List<TableElement> ElementsList = aux.getMatrizCV();

            foreach(TableElement te in ElementsList)
                Lista.Items.Add(te);


        }
    }
}