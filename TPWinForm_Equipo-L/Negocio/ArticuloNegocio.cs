using System;
using System.Collections.Generic;
using Dominio;
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


    }
}
