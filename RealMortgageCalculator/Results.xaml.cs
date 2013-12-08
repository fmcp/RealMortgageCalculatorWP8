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
using System.ComponentModel;

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
        private void loadTables()
        {
            BackgroundWorker bw = new BackgroundWorker();
            bw.WorkerReportsProgress = false;
            bw.WorkerSupportsCancellation = false;

            bw.DoWork +=
                 new DoWorkEventHandler(bw_DoWork);
            bw.RunWorkerCompleted +=
                new RunWorkerCompletedEventHandler(bw_RunWorkerCompleted);
            bw.RunWorkerAsync(calcInt);

            BackgroundWorker bwCV = new BackgroundWorker();
            bwCV.WorkerReportsProgress = false;
            bwCV.WorkerSupportsCancellation = false;

            bwCV.DoWork +=
                 new DoWorkEventHandler(bw_DoWorkCV);
            bwCV.RunWorkerCompleted +=
                new RunWorkerCompletedEventHandler(bw_RunWorkerCompletedCV);
            bwCV.RunWorkerAsync(calcInt);



        }

        /**
         * Realiza en segundo plano el cálculo de la matriz de amortización
         **/
        private void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            e.Result =  (e.Argument as CalcularInteres).calcularMatriz();
            
        }

        /**
         * Actualiza la interfaz cuando se completa el cálculo de la matriz
         **/
        private void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Lista.DataContext = e.Result;
            progressBar.Visibility = Visibility.Collapsed;
            loading.Visibility = Visibility.Collapsed;
        }

        /**
       * Realiza en segundo plano el cálculo de la matriz de amortización
       **/
        private void bw_DoWorkCV(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            e.Result = (e.Argument as CalcularInteres).calcularMatrizCV();


        }

        /**
         * Actualiza la interfaz cuando se completa el cálculo de la matriz
         **/
        private void bw_RunWorkerCompletedCV(object sender, RunWorkerCompletedEventArgs e)
        {
            ListaCV.DataContext = e.Result;
            progressBar2.Visibility = Visibility.Collapsed;
            loading2.Visibility = Visibility.Collapsed;
        }

    }
}