using System.Collections.Generic;
using Dominio;

namespace Negocio
{
    public class MarcaNegocio
    {
        public List<Marca> Listar()
        {
            // Etapa 1: datos de prueba en memoria.
            // En la Etapa 2 el cuerpo de este método pasa a consultar la tabla MARCAS
            // de CATALOGO_P3_DB. Los Id y las descripciones son los mismos que trae el
            // script de la cátedra, así que el formulario no se entera del cambio.
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
