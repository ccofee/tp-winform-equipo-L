using System.Collections.Generic;
using Dominio;

namespace Negocio
{
    public class CategoriaNegocio
    {
        // Etapa 1: las categorías viven en memoria mientras corre la aplicación, para poder
        // probar el alta y la modificación sin base de datos. En la Etapa 2 el cuerpo de
        // los tres métodos pasa a consultar la tabla CATEGORIAS de CATALOGO_P3_DB y esta
        // lista desaparece; las firmas quedan igual, así frmCategorias no se toca.
        private static List<Categoria> categorias = CargarDatosDePrueba();

        public List<Categoria> Listar()
        {
            // Devuelve una copia para que el formulario no modifique la lista por error.
            // Cuando esto consulte la base también va a devolver una lista nueva en cada
            // llamada, así que el comportamiento no cambia.
            return new List<Categoria>(categorias);
        }

        public void Agregar(Categoria nueva)
        {
            nueva.Id = ProximoId();
            categorias.Add(nueva);
        }

        public void Modificar(Categoria categoria)
        {
            Categoria existente = categorias.Find(x => x.Id == categoria.Id);

            if (existente != null)
                existente.Descripcion = categoria.Descripcion;
        }

        public void Eliminar(int id)
        {
            Categoria existente = categorias.Find(x => x.Id == id);

            if (existente != null)
                categorias.Remove(existente);
        }

        private int ProximoId()
        {
            // Imita el IDENTITY de la tabla CATEGORIAS.
            int mayor = 0;

            foreach (Categoria categoria in categorias)
            {
                if (categoria.Id > mayor)
                    mayor = categoria.Id;
            }

            return mayor + 1;
        }

        private static List<Categoria> CargarDatosDePrueba()
        {
            // Mismos Id y descripciones que trae el script de la cátedra.
            List<Categoria> lista = new List<Categoria>();

            lista.Add(new Categoria { Id = 1, Descripcion = "Celulares" });
            lista.Add(new Categoria { Id = 2, Descripcion = "Televisores" });
            lista.Add(new Categoria { Id = 3, Descripcion = "Media" });
            lista.Add(new Categoria { Id = 4, Descripcion = "Audio" });

            return lista;
        }
    }
}
