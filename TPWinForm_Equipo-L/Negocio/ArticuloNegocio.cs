using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class ArticuloNegocio
    {

        public List<Articulo> listar()
        {
            List<Articulo> lista = new List<Articulo>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                string consulta = @"SELECT 
                                        A.Id, 
                                        A.Codigo, 
                                        A.Nombre, 
                                        A.Descripcion AS DescArticulo, 
                                        A.Precio, 
                                        A.IdMarca, 
                                        M.Descripcion AS Marca, 
                                        A.IdCategoria, 
                                        C.Descripcion AS Categoria 
                                    FROM ARTICULOS A 
                                    LEFT JOIN MARCAS M ON A.IdMarca = M.Id 
                                    LEFT JOIN CATEGORIAS C ON A.IdCategoria = C.Id";

                datos.SetearConsulta(consulta);
                datos.EjecutarLectura();

                while (datos.Lector.Read())
                {
                    Articulo aux = new Articulo();

                    aux.Id = (int)datos.Lector["Id"];

                    //validacion de null para codigo
                    if (!(datos.Lector["Codigo"] is DBNull))
                        aux.Codigo = (string)datos.Lector["Codigo"];
                    else
                        aux.Codigo = string.Empty;

                    //validacion de nombre
                    if (!(datos.Lector["Nombre"] is DBNull))
                        aux.Nombre = (string)datos.Lector["Nombre"];
                    else
                        aux.Nombre = string.Empty;

                    //validacion de descripcion
                    if (!(datos.Lector["DescArticulo"] is DBNull))
                        aux.Descripcion = (string)datos.Lector["DescArticulo"];
                    else
                        aux.Descripcion = string.Empty;

                    //validacion de precio
                    if (!(datos.Lector["Precio"] is DBNull))
                        aux.Precio = (decimal)datos.Lector["Precio"];
                    else
                        aux.Precio = 0;

                    //validacion de MARCA
                    aux.Marca = new Marca();

                    if (!(datos.Lector["IdMarca"] is DBNull))
                    {
                        aux.Marca.Id = (int)datos.Lector["IdMarca"];

                        if (!(datos.Lector["Marca"] is DBNull))
                            aux.Marca.Descripcion = (string)datos.Lector["Marca"];
                        else
                            aux.Marca.Descripcion = "Sin Marca";
                    }
                    else
                    {
                        aux.Marca.Descripcion = "Sin Marca";
                    }

                    //validacion de CATEGORIA
                    aux.Categoria = new Categoria();

                    if (!(datos.Lector["IdCategoria"] is DBNull))
                    {
                        aux.Categoria.Id = (int)datos.Lector["IdCategoria"];

                        if (!(datos.Lector["Categoria"] is DBNull))
                            aux.Categoria.Descripcion = (string)datos.Lector["Categoria"];
                        else
                            aux.Categoria.Descripcion = "Sin Categoría";
                    }
                    else
                    {
                        aux.Categoria.Descripcion = "Sin Categoría";
                    }

                    lista.Add(aux);
                }

                return lista;

            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }

        }


        public List<Articulo> filtrar(string idMarca, string idCategoria, string precioMin, string precioMax)
        {
            List<Articulo> lista = new List<Articulo>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                string consulta = @"SELECT 
                                        A.Id, 
                                        A.Codigo, 
                                        A.Nombre, 
                                        A.Descripcion AS DescArticulo, 
                                        A.Precio, 
                                        A.IdMarca, 
                                        M.Descripcion AS Marca, 
                                        A.IdCategoria, 
                                        C.Descripcion AS Categoria 
                                    FROM ARTICULOS A 
                                    LEFT JOIN MARCAS M ON A.IdMarca = M.Id 
                                    LEFT JOIN CATEGORIAS C ON A.IdCategoria = C.Id
                                    WHERE 1=1";


                if (!string.IsNullOrEmpty(idMarca))
                {
                    consulta += " AND A.IdMarca = @idMarca";
                    datos.setearParametro("@idMarca", idMarca);
                }

                if (!string.IsNullOrEmpty(idCategoria))
                {
                    consulta += " AND A.IdCategoria = @idCategoria";
                    datos.setearParametro("@idCategoria", idCategoria);
                }

                if (!string.IsNullOrEmpty(precioMin))
                {
                    consulta += " AND A.Precio >= @precioMin";
                    datos.setearParametro("@precioMin", precioMin);
                }

                if (!string.IsNullOrEmpty(precioMax))
                {
                    consulta += " AND A.Precio <= @precioMax";
                    datos.setearParametro("@precioMax", precioMax);
                }


                datos.SetearConsulta(consulta);
                datos.EjecutarLectura();

                while (datos.Lector.Read())
                {
                    Articulo aux = new Articulo();
                    aux.Id = (int)datos.Lector["Id"];

                    //validaciones DBNull
                    if (!(datos.Lector["Codigo"] is DBNull)) aux.Codigo = (string)datos.Lector["Codigo"];
                    else aux.Codigo = string.Empty;

                    if (!(datos.Lector["Nombre"] is DBNull)) aux.Nombre = (string)datos.Lector["Nombre"];
                    else aux.Nombre = string.Empty;

                    if (!(datos.Lector["DescArticulo"] is DBNull)) aux.Descripcion = (string)datos.Lector["DescArticulo"];
                    else aux.Descripcion = string.Empty;

                    if (!(datos.Lector["Precio"] is DBNull)) aux.Precio = (decimal)datos.Lector["Precio"];
                    else aux.Precio = 0;

                    //relaciones
                    aux.Marca = new Marca();
                    if (!(datos.Lector["IdMarca"] is DBNull))
                    {
                        aux.Marca.Id = (int)datos.Lector["IdMarca"];
                        if (!(datos.Lector["Marca"] is DBNull))
                            aux.Marca.Descripcion = (string)datos.Lector["Marca"];
                        else
                            aux.Marca.Descripcion = "Sin Marca";
                    }
                    else
                    {
                        aux.Marca.Descripcion = "Sin Marca";
                    }

                    aux.Categoria = new Categoria();
                    if (!(datos.Lector["IdCategoria"] is DBNull))
                    {
                        aux.Categoria.Id = (int)datos.Lector["IdCategoria"];
                        if (!(datos.Lector["Categoria"] is DBNull))
                            aux.Categoria.Descripcion = (string)datos.Lector["Categoria"];
                        else
                            aux.Categoria.Descripcion = "Sin Categoría";
                    }
                    else
                    {
                        aux.Categoria.Descripcion = "Sin Categoría";
                    }

                    lista.Add(aux);
                }

                return lista;

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }



        }






        public int agregar(Articulo nuevo)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                string consulta = @"INSERT INTO ARTICULOS 
                            (Codigo, Nombre, Descripcion, IdMarca, IdCategoria, Precio)
                            VALUES
                            (@codigo, @nombre, @descripcion, @idMarca, @idCategoria, @precio);
                            SELECT SCOPE_IDENTITY();";

                datos.SetearConsulta(consulta);

                datos.setearParametro("@codigo", nuevo.Codigo);
                datos.setearParametro("@nombre", nuevo.Nombre);
                datos.setearParametro("@descripcion", nuevo.Descripcion);
                datos.setearParametro("@idMarca", nuevo.Marca.Id);
                datos.setearParametro("@idCategoria", nuevo.Categoria.Id);
                datos.setearParametro("@precio", nuevo.Precio);

                datos.EjecutarLectura();

                datos.Lector.Read();

                return Convert.ToInt32(datos.Lector[0]);
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
    
    public void modificar(Articulo articulo)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                string consulta = @"UPDATE ARTICULOS
                            SET Codigo = @codigo,
                                Nombre = @nombre,
                                Descripcion = @descripcion,
                                IdMarca = @idMarca,
                                IdCategoria = @idCategoria,
                                Precio = @precio
                            WHERE Id = @id";

                datos.SetearConsulta(consulta);

                datos.setearParametro("@codigo", articulo.Codigo);
                datos.setearParametro("@nombre", articulo.Nombre);
                datos.setearParametro("@descripcion", articulo.Descripcion);
                datos.setearParametro("@idMarca", articulo.Marca.Id);
                datos.setearParametro("@idCategoria", articulo.Categoria.Id);
                datos.setearParametro("@precio", articulo.Precio);
                datos.setearParametro("@id", articulo.Id);

                datos.ejecutarAccion();
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
        public void eliminar(int id)
        {
            ImagenNegocio imagenNegocio = new ImagenNegocio();
            imagenNegocio.eliminarPorArticulo(id);
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearConsulta("DELETE FROM ARTICULOS WHERE Id = @id");
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