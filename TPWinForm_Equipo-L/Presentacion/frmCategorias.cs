using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Dominio;
using Negocio;

namespace Presentacion
{
    public partial class frmCategorias : Form
    {
        private List<Categoria> listaCategorias;

        public frmCategorias()
        {
            InitializeComponent();
        }

        private void frmCategorias_Load(object sender, EventArgs e)
        {
            try
            {
                CargarListado();
                Limpiar();
            }
            catch (Exception ex)
            {
                ManejoErrores.Mostrar(ex, "cargar las categorías");
            }
        }

        private void dgvCategorias_SelectionChanged(object sender, EventArgs e)
        {
            Categoria seleccionada = CategoriaSeleccionada();

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
                CategoriaNegocio negocio = new CategoriaNegocio();
                Categoria nueva = new Categoria();

                nueva.Descripcion = txtDescripcion.Text.Trim();
                negocio.Agregar(nueva);

                CargarListado();
                Limpiar();
            }
            catch (Exception ex)
            {
                ManejoErrores.Mostrar(ex, "agregar la categoría");
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            Categoria seleccionada = CategoriaSeleccionada();

            if (seleccionada == null)
            {
                MessageBox.Show("Seleccioná la categoría que querés modificar.", "Categorías");
                return;
            }

            if (!HayDescripcion())
                return;

            try
            {
                CategoriaNegocio negocio = new CategoriaNegocio();
                Categoria modificada = new Categoria();

                modificada.Id = seleccionada.Id;
                modificada.Descripcion = txtDescripcion.Text.Trim();
                negocio.Modificar(modificada);

                CargarListado();
                Limpiar();
            }
            catch (Exception ex)
            {
                ManejoErrores.Mostrar(ex, "modificar la categoría");
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            Categoria seleccionada = CategoriaSeleccionada();

            if (seleccionada == null)
            {
                MessageBox.Show("Seleccioná la categoría que querés eliminar.", "Categorías");
                return;
            }

            try
            {
                CategoriaNegocio negocio = new CategoriaNegocio();
                int cantidad = negocio.ContarArticulos(seleccionada.Id);

                if (cantidad > 0)
                {
                    string detalle = cantidad == 1 ? "la usa 1 artículo" : "la usan " + cantidad + " artículos";

                    MessageBox.Show(
                        "No se puede eliminar la categoría " + seleccionada.Descripcion + ": " + detalle + ".",
                        "Eliminar categoría",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }

                DialogResult respuesta = MessageBox.Show(
                    "¿Querés eliminar la categoría " + seleccionada.Descripcion + "?",
                    "Eliminar categoría",
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
                ManejoErrores.Mostrar(ex, "eliminar la categoría");
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        private void CargarListado()
        {
            CategoriaNegocio negocio = new CategoriaNegocio();

            listaCategorias = negocio.Listar();
            dgvCategorias.DataSource = null;
            dgvCategorias.DataSource = listaCategorias;

            dgvCategorias.Columns["Id"].Visible = false;
            dgvCategorias.Columns["Descripcion"].HeaderText = "Categoría";
        }

        private Categoria CategoriaSeleccionada()
        {
            if (dgvCategorias.CurrentRow == null)
                return null;

            return dgvCategorias.CurrentRow.DataBoundItem as Categoria;
        }

        private bool HayDescripcion()
        {
            if (!Validacion.TieneTexto(txtDescripcion.Text))
            {
                MessageBox.Show("Escribí la descripción de la categoría.", "Categorías");
                txtDescripcion.Focus();
                return false;
            }

            return true;
        }

        private void Limpiar()
        {
            txtDescripcion.Text = "";
            dgvCategorias.ClearSelection();
        }
    }
}
