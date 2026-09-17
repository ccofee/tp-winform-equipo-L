using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;
namespace Negocio
{
    public class ImagenNegocio
    {
        public void agregar(Imagen imagen)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearConsulta("INSERT INTO IMAGENES (IdArticulo, ImagenUrl) VALUES (@idArticulo, @imagenUrl)");

                datos.setearParametro("@idArticulo", imagen.IdArticulo);
                datos.setearParametro("@imagenUrl", imagen.ImagenUrl);

                datos.ejecutarAccion();
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
        public void eliminarPorArticulo(int idArticulo)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearConsulta("DELETE FROM IMAGENES WHERE IdArticulo = @idArticulo");
                datos.setearParametro("@idArticulo", idArticulo);

                datos.ejecutarAccion();
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
        public List<Imagen> listarPorArticulo(int idArticulo)
        {
            List<Imagen> lista = new List<Imagen>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearConsulta(
                    "SELECT Id, IdArticulo, ImagenUrl FROM IMAGENES WHERE IdArticulo = @idArticulo"
                );

                datos.setearParametro("@idArticulo", idArticulo);
                datos.EjecutarLectura();

                while (datos.Lector.Read())
                {
                    Imagen imagen = new Imagen();

                    imagen.Id = (int)datos.Lector["Id"];
                    imagen.IdArticulo = (int)datos.Lector["IdArticulo"];
                    imagen.ImagenUrl = (string)datos.Lector["ImagenUrl"];

                    lista.Add(imagen);
                }

                return lista;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
    }
}
