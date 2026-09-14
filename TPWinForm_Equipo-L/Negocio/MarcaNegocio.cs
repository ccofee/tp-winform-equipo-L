using System.Collections.Generic;
using Dominio;

namespace Negocio
{
    public class MarcaNegocio
    {
        // Etapa 1: las marcas viven en memoria mientras corre la aplicación, para poder
        // probar el alta y la modificación sin base de datos. En la Etapa 2 el cuerpo de
        // los tres métodos pasa a consultar la tabla MARCAS de CATALOGO_P3_DB y esta
        // lista desaparece; las firmas quedan igual, así frmMarcas no se toca.
        private static List<Marca> marcas = CargarDatosDePrueba();

        public List<Marca> Listar()
        {
            // Devuelve una copia para que el formulario no modifique la lista por error.
            // Cuando esto consulte la base también va a devolver una lista nueva en cada
            // llamada, así que el comportamiento no cambia.
            return new List<Marca>(marcas);
        }

        public void Agregar(Marca nueva)
        {
            nueva.Id = ProximoId();
            marcas.Add(nueva);
        }

        public void Modificar(Marca marca)
        {
            Marca existente = marcas.Find(x => x.Id == marca.Id);

            if (existente != null)
                existente.Descripcion = marca.Descripcion;
        }

        public void Eliminar(int id)
        {
            Marca existente = marcas.Find(x => x.Id == id);

            if (existente != null)
                marcas.Remove(existente);
        }

        private int ProximoId()
        {
            // Imita el IDENTITY de la tabla MARCAS.
            int mayor = 0;

            foreach (Marca marca in marcas)
            {
                if (marca.Id > mayor)
                    mayor = marca.Id;
            }

            return mayor + 1;
        }

        private static List<Marca> CargarDatosDePrueba()
        {
            // Mismos Id y descripciones que trae el script de la cátedra.
            List<Marca> lista = new List<Marca>();

            lista.Add(new Marca { Id = 1, Descripcion = "Samsung" });
            lista.Add(new Marca { Id = 2, Descripcion = "Apple" });
            lista.Add(new Marca { Id = 3, Descripcion = "Sony" });
            lista.Add(new Marca { Id = 4, Descripcion = "Huawei" });
            lista.Add(new Marca { Id = 5, Descripcion = "Motorola" });

            return lista;
        }
    }
}
