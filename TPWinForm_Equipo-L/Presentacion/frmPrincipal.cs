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
        private List<Articulo> listaArticulos;

        public frmPrincipal()
        {
            InitializeComponent();
        }

        private void frmPrincipal_Load(object sender, EventArgs e)
        {
            cargar();
            cargarDesplegables();
        }


        private void cargar()
        {
            ArticuloNegocio negocio = new ArticuloNegocio();
            try
            {
                listaArticulos = negocio.listar();
                dgvArticulos.DataSource = listaArticulos;
                ocultarColumnas();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los artículos." + ex.Message);
            }
        }


        private void cargarDesplegables()
        {
            MarcaNegocio marcaNegocio = new MarcaNegocio();
            CategoriaNegocio categoriaNegocio = new CategoriaNegocio();

            try
            {
                
                cboMarca.DropDownStyle = ComboBoxStyle.DropDownList;
                cboCategoria.DropDownStyle = ComboBoxStyle.DropDownList;

                
                cboMarca.DataSource = marcaNegocio.Listar();
                cboMarca.ValueMember = "Id";
                cboMarca.DisplayMember = "Descripcion";
                cboMarca.SelectedIndex = -1;

                cboCategoria.DataSource = categoriaNegocio.Listar();
                cboCategoria.ValueMember = "Id";
                cboCategoria.DisplayMember = "Descripcion";
                cboCategoria.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Aviso al cargar desplegables: " + ex.Message);
            }

        }

        private void ocultarColumnas()
        {
            if (dgvArticulos.Columns["Id"] != null)
                dgvArticulos.Columns["Id"].Visible = false;

            //formato de precio a moneda
            if (dgvArticulos.Columns["Precio"] != null)
            {
                dgvArticulos.Columns["Precio"].DefaultCellStyle.Format = "C2";
                dgvArticulos.Columns["Precio"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
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

        private void txtFiltroRapido_TextChanged(object sender, EventArgs e)
        {
            List<Articulo> listaFiltrada;
            string filtro = txtFiltroRapido.Text.ToUpper();

            if (filtro.Length >= 1)
            {
                    listaFiltrada = listaArticulos.FindAll(x =>
                        (x.Nombre != null && x.Nombre.ToUpper().Contains(filtro)) ||
                        (x.Codigo != null && x.Codigo.ToUpper().Contains(filtro)) ||
                        (x.Descripcion != null && x.Descripcion.ToUpper().Contains(filtro))
                     );
            }
            else
            {
                listaFiltrada = listaArticulos;
            }

            dgvArticulos.DataSource = null;
            dgvArticulos.DataSource = listaFiltrada;
            ocultarColumnas();

        }

        private void btnBuscarAvanzado_Click(object sender, EventArgs e)
        {

            ArticuloNegocio negocio = new ArticuloNegocio();

            try
            {
                //marca
                string idMarca = "";
                if (cboMarca.SelectedIndex != -1)
                {
                    Marca seleccionada = (Marca)cboMarca.SelectedItem;
                    idMarca = seleccionada.Id.ToString();
                }
                //categoria
                string idCategoria = "";
                if (cboCategoria.SelectedIndex != -1)
                {
                    Categoria seleccionada = (Categoria)cboCategoria.SelectedItem;
                    idCategoria = seleccionada.Id.ToString();
                }

                //rango de precios
                string min = txtPrecioMin.Text;
                string max = txtPrecioMax.Text;

                //desvinculada anterior, me tiro unos errores
                dgvArticulos.DataSource = null;

                //vinculo los resultados filtrados al dgv
                dgvArticulos.DataSource = negocio.filtrar(idMarca, idCategoria, min, max);

                ocultarColumnas();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error en la búsqueda avanzada: " + ex.Message);
            }
        }


        private void controlFiltro_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnBuscarAvanzado.PerformClick();
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void btnLimpiarFiltro_Click(object sender, EventArgs e)
        {
            cboMarca.SelectedIndex = -1;
            cboCategoria.SelectedIndex = -1;
            txtPrecioMin.Clear();
            txtPrecioMax.Clear();
            txtFiltroRapido.Clear();
            
            cargar();
        }

    }
}
