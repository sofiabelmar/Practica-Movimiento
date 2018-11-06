using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
//Librerias para multiprocesamiento
using System.Threading;  
using System.Diagnostics;

namespace PracticaMovimiento
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Stopwatch stopwatch;
        TimeSpan tiempoAnterior;


        public MainWindow()
        {
            InitializeComponent();
            Canvas1.Focus();

            stopwatch = new Stopwatch();
            stopwatch.Start();
            tiempoAnterior = stopwatch.Elapsed;

            //1.Establecer instrucciones
            ThreadStart threadStart = new ThreadStart(moverEnemigo);
            //2.Inicializar el thread
            Thread threadMoverEnemigos = new Thread(threadStart);
            //3.Ejecutar el thread
            threadMoverEnemigos.Start();


        }

        void moverEnemigo()
        {
            while (true)
            {
                Dispatcher.Invoke(() =>
                {

                    var tiempoActual = stopwatch.Elapsed;
                    var deltaTime = tiempoActual - tiempoAnterior;

                    double leftCarroActual = Canvas.GetLeft(carro);
                    Canvas.SetLeft(carro, leftCarroActual - (90 * deltaTime.TotalSeconds));
                    if (Canvas.GetLeft(carro) <= -153)
                    {
                        Canvas.SetLeft(carro, 800);
                    }
                    tiempoAnterior = tiempoActual;

                });
            }

        }

        private void Canvas1_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Up)
            {
                double topFlorActual = Canvas.GetTop(flor);
                Canvas.SetTop(flor, topFlorActual - 15); //Esta funcion lleva dos parametros uno es el elemento que s está moviendo y el segundo la cantidad
            }
        }
    }
}
