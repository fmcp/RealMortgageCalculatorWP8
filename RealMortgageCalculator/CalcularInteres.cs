using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/**
 *	Crear un objeto, llamar al constructor con los argumentos solicitados.
 *	Llamar al metodo run ejecuta las operaciones
 *	Finalmente en las variables matriz y matrizCV se encuentra el resultado.
 */
namespace ConsoleApplication1 {
    class CalcularInteres {
		static double capitalInvertido, interesAnual;
		static int periodos;

		static double cuotaMensual, cuotaMensualCV, interesMes, interesMesCV;
		static double[,] matriz, matrizCV;

		CalcularInteres(double auxCapital, double auxInteres, int auxPeriodos) {
			capitalInvertido = auxCapital;
			interesAnual = auxInteres;
			periodos = auxPeriodos;
		}

        static void Main(string[] args) {
			Console.Write("Capital invertido: ");
			capitalInvertido = 100000;//double.Parse(Console.ReadLine());

			Console.Write("Tasa de interes: ");
			interesAnual = 0.1;//double.Parse(Console.ReadLine());
			Console.WriteLine("Interes introducido: " + interesAnual);

			Console.Write("Número de periodos temporales: ");
			periodos = 30;//int.Parse(Console.ReadLine());

			/////////////////////////////////////////////////////////////////////////////////////

			inicializar();
			calcular();
			imprimir();

			Console.ReadLine();
        }

		static void inicializar() {
			cuotaMensual = interesComplejo(capitalInvertido, interesAnual, periodos);
			cuotaMensualCV = interesSimple(capitalInvertido, interesAnual, periodos);
			interesMes = calcularIM(interesAnual);
			interesMesCV = calcularIMCuentaVieja(interesAnual);
		}

		static void calcular()
		{
			matriz = new double[3, periodos * 12];
			matrizCV = new double[3, periodos * 12];

			double deuda = capitalInvertido, interes = 0, amortizacion = 0;

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

			deuda = capitalInvertido; interes = 0; amortizacion = 0;
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

		static void run() {
			inicializar();
			calcular();
		}

		static void imprimir() {
			Console.WriteLine("Cuota mensual: " + cuotaMensual * periodos * 12);
			Console.WriteLine("Cuota mensual CV: " + cuotaMensualCV * periodos * 12);
			Console.WriteLine("Total resta: " + (cuotaMensualCV * periodos * 12 - cuotaMensual * periodos * 12));

			Console.WriteLine("Interes mes: " + interesMes);
			Console.WriteLine("Interes mes CV: " + interesMesCV);

			Console.WriteLine("Cuota mensual: " + cuotaMensual);
			Console.WriteLine("Cuota mensual CV: " + cuotaMensualCV);

			int i = 0;
			Console.WriteLine("\nCálculo normal\nDeuda\tAmort\tinteres");
			for (i = 0; i < periodos; i++)
				Console.WriteLine(matriz[0, i] + "\t" + matriz[1, i] + "\t" + matriz[2, i]);
			Console.WriteLine("\nCálculo \"Cuenta la vieja\"\nDeuda\tAmort\tinteres");
			for (i = 0; i < periodos; i++)
				Console.WriteLine(matrizCV[0, i] + "\t" + matrizCV[1, i] + "\t" + matrizCV[2, i]);
		}

		static double interesSimple(double capital, double interes, int periodos) {
			double im = calcularIMCuentaVieja(interes);

			double A = Math.Pow((1 + im), 12 * periodos);
			double B = 1 - (1 / A);

			double aux = capital * (im / B);
			return aux;
		}

		static double interesComplejo(double capital, double interes, int anyos) {
			double im = calcularIM(interes);

			double A = Math.Pow((1 + im), 12 * anyos);
			double B = 1 - (1 / A);

			double aux = capital * (im / B);
			return aux;
		}

		static double calcularIM(double interes) {
			return Math.Pow((1.0 + interes), (1.0 / 12.0)) - 1.0;
		}

		static double calcularIMCuentaVieja(double interes)	{
			return interes / 12.0;
		}

		static double calcularCuotaMensual(double cantidad, double interes, int periodos) {
			return cantidad * (interes / (1 - (1 / (Math.Pow((1 + interes), (12.0 * periodos))))));
		}
    }
}
