using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MySql.Data.MySqlClient;

namespace SakilaB
{
    public class AccesoDatos
    {
        //Campos privados
        MySqlConnection _conn;
        MySqlCommand _cmd;

        string _conectionString = "server=localhost;" +
                                  "user=root;" +
                                  "database=sakila;" +
                                  "port=3306;" +
                                  "password=1234";

       


        //Constructor(es)
        public AccesoDatos()
        {
            try
            {
                _conn = new MySqlConnection(_conectionString);
                _cmd = new MySqlCommand();
                _conn.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
        //Metodos
        public DataSet DameNombresActores()
        {
            DataSet ds = new DataSet();

            _cmd = new MySqlCommand();
            _cmd.Connection = _conn;
            _cmd.CommandType = CommandType.StoredProcedure;
            _cmd.CommandText = "DameNombreDeActores";

            _cmd.ExecuteNonQuery();

            IDataAdapter adapter = new MySqlDataAdapter(_cmd);

            // construct DataSet to store result
            adapter.Fill(ds);

            return ds;
        }

        public int IncrementaLongitudPelicula(string titulo)
        {
            int resultado = -99;

            try
            {
                _cmd = new MySqlCommand();
                _cmd.Connection = _conn;
                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.CommandText = "IncrementaDuracionPelicula";

                _cmd.Parameters.AddWithValue("_titulo", titulo);
                _cmd.Parameters["_titulo"].Direction = ParameterDirection.Input;

                _cmd.Parameters.Add(new MySqlParameter("_res", MySqlDbType.Int32));
                _cmd.Parameters["_res"].Direction = ParameterDirection.Output;

                _cmd.ExecuteNonQuery();


                resultado = (int)_cmd.Parameters["_res"].Value;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return resultado;
            }
            return resultado;
        }

        public int AltaActor(string nombre, string apellido)
        {
            int resultado = -99;

            try
            {
                _cmd = new MySqlCommand();
                _cmd.Connection = _conn;
                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.CommandText = "InsertaNuevoActor";

                _cmd.Parameters.AddWithValue("_nombre", nombre);
                _cmd.Parameters["_nombre"].Direction = ParameterDirection.Input;

                _cmd.Parameters.AddWithValue("_apellido", apellido);
                _cmd.Parameters["_apellido"].Direction = ParameterDirection.Input;

                _cmd.Parameters.Add(new MySqlParameter("_res", MySqlDbType.Int32));
                _cmd.Parameters["_res"].Direction = ParameterDirection.Output;

                _cmd.ExecuteNonQuery();


                resultado = (int)_cmd.Parameters["_res"].Value;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return resultado;
            }
            return resultado;
        }

        public int DameNumeroPeliculas()
        {
            int resultado = -99;
            try
            {
                _cmd = new MySqlCommand();
                _cmd.Connection = _conn;
                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.CommandText = "CuentaPeliculas";

                _cmd.Parameters.Add(new MySqlParameter("_res", MySqlDbType.Int32));
                _cmd.Parameters["_res"].Direction = ParameterDirection.Output;

                _cmd.ExecuteNonQuery();

                resultado = (int)_cmd.Parameters["_res"].Value;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return resultado;
            }
            return resultado;
        }

        public int PA_AltaEmpleado(string nombre, string apellido, string mail, int id_tienda,
                                string usuario, string pass)
        {
            int resultado = -99;

            try
            {
                _cmd = new MySqlCommand();
                _cmd.Connection = _conn;
                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.CommandText = "InsertarUsuario";

                _cmd.Parameters.AddWithValue("_first_name", nombre);
                _cmd.Parameters["_first_name"].Direction = ParameterDirection.Input;

                _cmd.Parameters.AddWithValue("_last_name", apellido);
                _cmd.Parameters["_last_name"].Direction = ParameterDirection.Input;

                _cmd.Parameters.AddWithValue("_email", mail);
                _cmd.Parameters["_email"].Direction = ParameterDirection.Input;

                _cmd.Parameters.AddWithValue("_store_id", id_tienda);
                _cmd.Parameters["_store_id"].Direction = ParameterDirection.Input;

                _cmd.Parameters.AddWithValue("_username", usuario);
                _cmd.Parameters["_username"].Direction = ParameterDirection.Input;

                string paswordHash = CreateMD5(pass).Substring(0, 20);

                _cmd.Parameters.AddWithValue("_password", paswordHash);
                _cmd.Parameters["_password"].Direction = ParameterDirection.Input;

                _cmd.Parameters.Add(new MySqlParameter("_res", MySqlDbType.Int32));
                _cmd.Parameters["_res"].Direction = ParameterDirection.Output;

                _cmd.ExecuteNonQuery();


                resultado = (int)_cmd.Parameters["_res"].Value;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return resultado;
            }
            return resultado;
        }
        public int PA_Login(string usuario, string pass)
        {
            int resultado = -99;

            try
            {
                _cmd = new MySqlCommand();
                _cmd.Connection = _conn;
                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.CommandText = "Login";

                _cmd.Parameters.AddWithValue("_username", usuario);
                _cmd.Parameters["_username"].Direction = ParameterDirection.Input;

                string paswordHash = CreateMD5(pass).Substring(0, 20);

                _cmd.Parameters.AddWithValue("_password", paswordHash);
                _cmd.Parameters["_password"].Direction = ParameterDirection.Input;

                _cmd.Parameters.Add(new MySqlParameter("_res", MySqlDbType.Int32));
                _cmd.Parameters["_res"].Direction = ParameterDirection.Output;

                _cmd.ExecuteNonQuery();

                resultado = (int)_cmd.Parameters["_res"].Value;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return resultado;
            }
            return resultado;

        }
        public string CreateMD5(string input)
        {
            // Use input string to calculate MD5 hash
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                return Convert.ToHexString(hashBytes); // .NET 5 +

            }
        }

        //public int DameNumeroPelis()
        //{
        //    return _numeroPeliculas;
        //}
        //public void SetNumeroPelis(int numeroPelis)
        //{
        //    _numeroPeliculas = numeroPelis;
        //}

    }
}
