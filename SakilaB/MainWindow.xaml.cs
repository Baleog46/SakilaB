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
using System.Windows.Threading;

namespace SakilaB
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SakilaManager sm = new SakilaManager();
        public MainWindow()
        {
            InitializeComponent();

            //DispatcherTimer
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(0.25);
            timer.Tick += ActualizarNumeroPeliculas_Tick;
            timer.Start();


            MostrarLogin();

            DataContext = sm;
        }
        public void ActualizarNumeroPeliculas_Tick(object sender, EventArgs e)
        {
            sm.ActualizarNumeroPeliculas();
        }
        private void OcultarTodo()
        {
            sp_Login.Visibility = Visibility.Collapsed;
            sp_Registro.Visibility = Visibility.Collapsed;
        }
        private void MostrarLogin()
        {
            OcultarTodo();
            sp_Login.Visibility = Visibility.Visible;
        }
        private void MostrarRegistro()
        {
            OcultarTodo();
            sp_Registro.Visibility = Visibility.Visible;
        }


        private void Button_Registrar(object sender, RoutedEventArgs e)
        {
            MostrarRegistro();
        }

        private void Button_Entrar(object sender, RoutedEventArgs e)
        {
           sm.Login(tb_usu.Text, pb_password.Password);
        }

        private void Button_Sumar(object sender, RoutedEventArgs e)
        {
            sm.Sumador++;
        }
        //Botones del Registro
        private void Button_Cerrar(object sender, RoutedEventArgs e)
        {
            MostrarLogin(); //Volver a HOME
        }

        private void Button_Enviar(object sender, RoutedEventArgs e)
        {
            int id_tienda = 1;
            //Seleccionar el valor del id_ de la tienda
            if (cb_tienda.SelectedIndex == 0)
            {
                id_tienda = 1;
            }
            else if (cb_tienda.SelectedIndex == 1)
            {
                id_tienda = 2;
            }
            //Que las passwords coincidan
            if (pb_pass.Password != pb_pass_confirm.Password)
            {
                MessageBox.Show("Las constraseñas deben coincidir");
            }
            else
            {
                sm.Registrar(tb_nombre.Text, tb_apellido.Text, tb_mail.Text, id_tienda,
                                            tb_usuario.Text, pb_pass.Password);
            }
        }

    }
}
