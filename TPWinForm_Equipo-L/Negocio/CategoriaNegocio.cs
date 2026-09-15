using System;
using System.Collections.Generic;
using Dominio;

namespace Negocio
{
    public class CategoriaNegocio
    {
        public List<Categoria> Listar()
        {
            List<Categoria> lista = new List<Categoria>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearConsulta("SELECT Id, Descripcion FROM CATEGORIAS");
                datos.EjecutarLectura();

                while (datos.Lector.Read())
                {
                    Categoria aux = new Categoria();

                    aux.Id = (int)datos.Lector["Id"];

                    // Descripcion admite NULL en la tabla CATEGORIAS.
                    if (!(datos.Lector["Descripcion"] is DBNull))
                        aux.Descripcion = (string)datos.Lector["Descripcion"];
                    else
                        aux.Descripcion = string.Empty;

                    lista.Add(aux);
                }

                return lista;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public void Agregar(Categoria nueva)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearConsulta("INSERT INTO CATEGORIAS (Descripcion) VALUES (@descripcion)");
                datos.setearParametro("@descripcion", nueva.Descripcion);
                datos.ejecutarAccion();
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public void Modificar(Categoria categoria)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearConsulta("UPDATE CATEGORIAS SET Descripcion = @descripcion WHERE Id = @id");
                datos.setearParametro("@descripcion", categoria.Descripcion);
                datos.setearParametro("@id", categoria.Id);
                datos.ejecutarAccion();
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public void Eliminar(int id)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearConsulta("DELETE FROM CATEGORIAS WHERE Id = @id");
                datos.setearParametro("@id", id);
                datos.ejecutarAccion();
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
    }
}
