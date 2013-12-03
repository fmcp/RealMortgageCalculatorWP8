using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace RealMortgageCalculator {

    class CalcularInteres {
        
        //Capital de la hipoteca: 
	    private double capital;

        //Interés anual aplicado a la hipoteca
        private  double interesAnual;

        //Número de mensualidades en las que se pagará el préstamo
		private int periodos;


        //Resultados del cálculo de la hipoteca
        private double cuotaMensual;
        private double cuotaMensualCV;
        private double interesMes;
        private double interesMesCV;
		private double[,] matriz, matrizCV;

        /**
         * Constructor de la clase. Crea un
         * @param capital Capital de la hipoteca
         * @param interes Interés aplicado al préstamo
         * @param periodos Periodos en los que se dividirá el pago
         **/
		CalcularInteres(double capital, double interes, int periodos) {
			this.capital = capital;
			this.interesAnual = interes;
			this.periodos = periodos;
		}

        /**
         * Calcula los intereses y la cuota mensual tanto por el sistema tradicional como por el correcto
         **/
		public void calcular() {
			cuotaMensual = interesComplejo(capital, interesAnual, periodos);
			cuotaMensualCV = interesSimple(capital, interesAnual, periodos);
			interesMes = calcularIM(interesAnual);
			interesMesCV = calcularIMCuentaVieja(interesAnual);
		}

        /**
         * Calcula las matrices de amortización tanto por el sistema tradicional como por el correcto
         **/
		public void calcularMatriz()
		{
			matriz = new double[3, periodos * 12];
			matrizCV = new double[3, periodos * 12];

			double deuda = capital, interes = 0, amortizacion = 0;

			int i = 0;
			for (i = 0; i < periodos * 12; i++)
			{
				interes = deuda * interesMes;
				amortizacion = cuotaMensual - interes;
				deuda = deuda - amortizacion;
				matriz[0, i] = deuda;
				matriz[1, i] = amortizacion;
				matriz[2, i] = interes;
			}

			deuda = capital; interes = 0; amortizacion = 0;
			for (i = 0; i < periodos; i++)
			{
				interes = deuda * interesMesCV;
				amortizacion = cuotaMensualCV - interes;
				deuda = deuda - amortizacion;
				matrizCV[0, i] = deuda;
				matrizCV[1, i] = amortizacion;
				matrizCV[2, i] = interes;
			}
		}

        /**
         * Calcula tanto los intereses como las matrices de amortización
         **/
		public void run() {
			this.calcular();
			this.calcularMatriz();
		}

        /**
         * Calcula el interes simple de una hipoteca
         * @param capital Capital de la hipoteca
         * @param interes Interés aplicado al préstamo
         * @param periodos Periodos en los que se dividirá el pago
         **/
		static double interesSimple(double capital, double interes, int periodos) {
			double im = calcularIMCuentaVieja(interes);

			double A = Math.Pow((1 + im), 12 * periodos);
			double B = 1 - (1 / A);

			double aux = capital * (im / B);
			return aux;
		}

        /**
         * Calcula el interes complejo de una hipoteca
         * @param capital Capital de la hipoteca
         * @param interes Interés aplicado al préstamo
         * @param periodos Periodos en los que se dividirá el pago
         **/
		static double interesComplejo(double capital, double interes, int periodos) {
			double im = calcularIM(interes);

			double A = Math.Pow((1 + im), 12 * periodos);
			double B = 1 - (1 / A);

			double aux = capital * (im / B);
			return aux;
		}

        //TODO Comentar
		static double calcularIM(double interes) {
			return Math.Pow((1.0 + interes), (1.0 / 12.0)) - 1.0;
		}

        //TODO
		static double calcularIMCuentaVieja(double interes)	{
			return interes / 12.0;
		}

        /**
         *  Calcula la cuota mensual de un préstamo
         **/
		static double calcularCuotaMensual(double cantidad, double interes, int periodos) {
			return cantidad * (interes / (1 - (1 / (Math.Pow((1 + interes), (12.0 * periodos))))));
		}
    }
}
