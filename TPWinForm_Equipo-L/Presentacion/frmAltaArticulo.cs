using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Presentacion
{
    public partial class frmAltaArticulo : Form
    {
        private Articulo articulo = null;

        public frmAltaArticulo()
        {
            InitializeComponent();
        }

        public frmAltaArticulo(Articulo articulo)
        {
            InitializeComponent();
            this.articulo = articulo;
        }
        private void frmAltaArticulo_Load(object sender, EventArgs e)
        {
            MarcaNegocio marcaNegocio = new MarcaNegocio();
            CategoriaNegocio categoriaNegocio = new CategoriaNegocio();

            List<Marca> marcas = marcaNegocio.Listar();
            cboMarca.DataSource = marcas;

            List<Categoria> categorias = categoriaNegocio.Listar();
            cboCategoria.DataSource = categorias;
            if (articulo != null)
            {
                Text = "Modificar artículo";
                lblTitulo.Text = "Modificar artículo";
                txtCodigo.Text = articulo.Codigo;
                txtNombre.Text = articulo.Nombre;
                txtDescripcion.Text = articulo.Descripcion;
                txtPrecio.Text = articulo.Precio.ToString();

                for (int i = 0; i < cboMarca.Items.Count; i++)
                {
                    Marca marca = (Marca)cboMarca.Items[i];

                    if (marca.Id == articulo.Marca.Id)
                    {
                        cboMarca.SelectedIndex = i;
                        break;
                    }
                }
                for (int i = 0; i < cboCategoria.Items.Count; i++)
                {
                    Categoria categoria = (Categoria)cboCategoria.Items[i];

                    if (categoria.Id == articulo.Categoria.Id)
                    {
                        cboCategoria.SelectedIndex = i;
                        break;
                    }
                }
            }
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            ArticuloNegocio negocio = new ArticuloNegocio();

            if (articulo == null)
            {
                Articulo nuevo = new Articulo();

                nuevo.Codigo = txtCodigo.Text;
                nuevo.Nombre = txtNombre.Text;
                nuevo.Descripcion = txtDescripcion.Text;
                nuevo.Precio = decimal.Parse(txtPrecio.Text);
                nuevo.Marca = (Marca)cboMarca.SelectedItem;
                nuevo.Categoria = (Categoria)cboCategoria.SelectedItem;

                nuevo.Id = negocio.agregar(nuevo);
                frmImagenes ventanaImagenes = new frmImagenes(nuevo.Id);
                ventanaImagenes.ShowDialog();

                MessageBox.Show("Artículo agregado correctamente. Id: " + nuevo.Id);
            }
            else
            {
                articulo.Codigo = txtCodigo.Text;
                articulo.Nombre = txtNombre.Text;
                articulo.Descripcion = txtDescripcion.Text;
                articulo.Precio = decimal.Parse(txtPrecio.Text);
                articulo.Marca = (Marca)cboMarca.SelectedItem;
                articulo.Categoria = (Categoria)cboCategoria.SelectedItem;

                negocio.modificar(articulo);

                MessageBox.Show("Artículo modificado correctamente.");
            }

            Close();
        }
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnImagenes_Click(object sender, EventArgs e)
        {
            if (articulo != null && articulo.Id > 0)
            {
                frmImagenes ventana = new frmImagenes(articulo.Id);
                ventana.ShowDialog();
            }
        }
    }
}
