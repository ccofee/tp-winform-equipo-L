using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    //clase que se encarga de la conexion a la base de datos y consultas SQL
    public class AccesoDatos
    {
        //objetos core de ADO.NET
        private SqlConnection conexion;
        private SqlCommand comando;
        private SqlDataReader lector;


        //
        public SqlDataReader Lector { get { return lector; } }


        //constructor
        public AccesoDatos()
        {
            string cadenaConexion = ConfigurationManager.AppSettings["cadena-conexion"];

            conexion = new SqlConnection(cadenaConexion);
            comando = new SqlCommand();
        }


        public void SetearConsulta(string consulta)
        {
            comando.CommandType = System.Data.CommandType.Text;
            comando.CommandText = consulta;
        }

        //setea un parametro para la consulta SQL
        public void setearParametro(string nombre, object valor)
        {
            comando.Parameters.AddWithValue(nombre, valor);
        }

        //ejecuta la consulta y devuelve un lector de datos
        public void EjecutarLectura()
        {
            comando.Connection = conexion;
            try
            {
                conexion.Open();
                lector = comando.ExecuteReader();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //ejecuta la consulta y no devuelve nada
        public void ejecutarAccion()
        {
            comando.Connection = conexion;
            try
            {
                conexion.Open();
                comando.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //cierra la conexion y el lector de datos
        public void cerrarConexion()
        {
            if (lector != null)
                lector.Close();

            conexion.Close();

        }
    }

}
