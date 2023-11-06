using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SakilaB
{
    public class SakilaManager : INotifyPropertyChanged
    {
        // Declarar el evento para MVVM
        public event PropertyChangedEventHandler PropertyChanged;

        private AccesoDatos _ad = new AccesoDatos();
        //Campos privados
        private int _numeroPeliculas;
        private int _sumador;
        private int _resX;
        private int _resY;

        //Propiedades (campos publicos)
        public int NumeroPeliculas
        {
            get { return _numeroPeliculas; }
            set { _numeroPeliculas = value;
                OnPropertyChanged("NumeroPeliculas");
            }
        }
        public int Sumador
        {
            get { return _sumador; }
            set { 
                _sumador = value;
                OnPropertyChanged("Sumador");
            }
        }
        public int ResX
        {
            get { return _resX; }
            set { 
                _resX = value;
                OnPropertyChanged("ResX");
            }
        }
        public int ResY
        {
            get { return _resY; }
            set
            {
                _resY = value;
                OnPropertyChanged("ResY");
            }
        }


        //Constructor(es)
        public SakilaManager()
        {
            _numeroPeliculas = 0;
            _sumador = 0;
            _resX = 1024;
            _resY = 768;
        }
        //Metodos (de Negocio)
        public void Login(string usuario, string pass)
        {
            //Comprobaciones previas

            if(_ad.PA_Login(usuario, pass) == 0)
            {
                MessageBox.Show("Bienvenid@");
            }
            else
            {
                MessageBox.Show("Error de Login");
            }


        }
        public void Registrar(string nombre, string apellido, string mail, int id_tienda,
                                string usuario, string pass)
        {
            //Comprobaciones previas

            if (_ad.PA_AltaEmpleado(nombre,apellido,mail,id_tienda,usuario,pass) == 0)
            {
                MessageBox.Show("Bienvenid@");
            }
            else
            {
                MessageBox.Show("Error de Login");
            }


        }
       
        public void ActualizarNumeroPeliculas()
        {
            NumeroPeliculas = _ad.DameNumeroPeliculas();
        }
        
        // Create the OnPropertyChanged method to raise the event
       // The calling member's name will be used as the parameter.
            protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
