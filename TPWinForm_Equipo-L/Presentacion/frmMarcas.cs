using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Dominio;
using Negocio;

namespace Presentacion
{
    public partial class frmMarcas : Form
    {
        private List<Marca> listaMarcas;

        public frmMarcas()
        {
            InitializeComponent();
        }

        private void frmMarcas_Load(object sender, EventArgs e)
        {
            try
            {
                CargarListado();
                Limpiar();
            }
            catch (Exception ex)
            {
                ManejoErrores.Mostrar(ex, "cargar las marcas");
            }
        }

        private void dgvMarcas_SelectionChanged(object sender, EventArgs e)
        {
            Marca seleccionada = MarcaSeleccionada();

            if (seleccionada == null)
                return;

            txtDescripcion.Text = seleccionada.Descripcion;
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (!HayDescripcion())
                return;

            try
            {
                MarcaNegocio negocio = new MarcaNegocio();
                Marca nueva = new Marca();

                nueva.Descripcion = txtDescripcion.Text.Trim();
                negocio.Agregar(nueva);

                CargarListado();
                Limpiar();
            }
            catch (Exception ex)
            {
                ManejoErrores.Mostrar(ex, "agregar la marca");
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            Marca seleccionada = MarcaSeleccionada();

            if (seleccionada == null)
            {
                MessageBox.Show("Seleccioná la marca que querés modificar.", "Marcas");
                return;
            }

            if (!HayDescripcion())
                return;

            try
            {
                MarcaNegocio negocio = new MarcaNegocio();
                Marca modificada = new Marca();

                modificada.Id = seleccionada.Id;
                modificada.Descripcion = txtDescripcion.Text.Trim();
                negocio.Modificar(modificada);

                CargarListado();
                Limpiar();
            }
            catch (Exception ex)
            {
                ManejoErrores.Mostrar(ex, "modificar la marca");
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            Marca seleccionada = MarcaSeleccionada();

            if (seleccionada == null)
            {
                MessageBox.Show("Seleccioná la marca que querés eliminar.", "Marcas");
                return;
            }

            try
            {
                MarcaNegocio negocio = new MarcaNegocio();
                int cantidad = negocio.ContarArticulos(seleccionada.Id);

                if (cantidad > 0)
                {
                    string detalle = cantidad == 1 ? "la usa 1 artículo" : "la usan " + cantidad + " artículos";

                    MessageBox.Show(
                        "No se puede eliminar la marca " + seleccionada.Descripcion + ": " + detalle + ".",
                        "Eliminar marca",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }

                DialogResult respuesta = MessageBox.Show(
                    "¿Querés eliminar la marca " + seleccionada.Descripcion + "?",
                    "Eliminar marca",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (respuesta != DialogResult.Yes)
                    return;

                negocio.Eliminar(seleccionada.Id);

                CargarListado();
                Limpiar();
            }
            catch (Exception ex)
            {
                ManejoErrores.Mostrar(ex, "eliminar la marca");
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        private void CargarListado()
        {
            MarcaNegocio negocio = new MarcaNegocio();

            listaMarcas = negocio.Listar();
            dgvMarcas.DataSource = null;
            dgvMarcas.DataSource = listaMarcas;

            dgvMarcas.Columns["Id"].Visible = false;
            dgvMarcas.Columns["Descripcion"].HeaderText = "Marca";
        }

        private Marca MarcaSeleccionada()
        {
            if (dgvMarcas.CurrentRow == null)
                return null;

            return dgvMarcas.CurrentRow.DataBoundItem as Marca;
        }

        private bool HayDescripcion()
        {
            if (!Validacion.TieneTexto(txtDescripcion.Text))
            {
                MessageBox.Show("Escribí la descripción de la marca.", "Marcas");
                txtDescripcion.Focus();
                return false;
            }

            return true;
        }

        private void Limpiar()
        {
            txtDescripcion.Text = "";
            dgvMarcas.ClearSelection();
        }
    }
}
