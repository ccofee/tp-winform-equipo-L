using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Dominio;
using Negocio;

namespace Presentacion
{
    public partial class frmPrincipal : Form
    {
        //private List<Articulo> listaArticulos;

        public frmPrincipal()
        {
            InitializeComponent();
        }

        private void frmPrincipal_Load(object sender, EventArgs e)
        {
            ArticuloNegocio negocio = new ArticuloNegocio();

            try
            {
                dgvArticulos.DataSource = negocio.listar();

                dgvArticulos.Columns["Id"].Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los artículos: " + ex.Message);
            }
        }

        private void btnVerDetalle_Click(object sender, EventArgs e)
        {
            
            if (dgvArticulos.CurrentRow == null)
                return;

            Articulo seleccionado = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;



            //////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // DATOS DE PRUEBA: inyectar imágenes temporales antes de abrir el detalle (BORRAR AL CONECTAR LA BD)

            if (seleccionado.Imagenes == null)
                seleccionado.Imagenes = new List<Imagen>();

            seleccionado.Imagenes.Clear();

            switch (seleccionado.Codigo?.Trim().ToUpper())
            {
                case "A01":
                    seleccionado.Imagenes.Add(new Imagen { ImagenUrl = "https://picsum.photos/id/237/500/400" }); // Perro
                    seleccionado.Imagenes.Add(new Imagen { ImagenUrl = "https://picsum.photos/id/0/500/400" });   // Laptop
                    break;

                case "A02":
                    seleccionado.Imagenes.Add(new Imagen { ImagenUrl = "https://picsum.photos/id/1060/500/400" }); // Café
                    seleccionado.Imagenes.Add(new Imagen { ImagenUrl = "https://picsum.photos/id/24/500/400" });   // Libro
                    break;

                default:
                    seleccionado.Imagenes.Add(new Imagen { ImagenUrl = "https://picsum.photos/id/250/500/400" }); // Cámara
                    seleccionado.Imagenes.Add(new Imagen { ImagenUrl = "https://picsum.photos/id/175/500/400" }); // Reloj
                    break;
            }
            //////////////////////////////////////////////////////////////////////////////////////////////////////////////

            //esto queda
            frmDetalle detalle = new frmDetalle(seleccionado);
            detalle.ShowDialog();
        }
    }
}
