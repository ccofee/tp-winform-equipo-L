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
            if (!Validacion.TieneTexto(txtCodigo.Text))
            {
                MessageBox.Show("Debe ingresar un código.");
                return;
            }

            if (!Validacion.TieneTexto(txtNombre.Text))
            {
                MessageBox.Show("Debe ingresar un nombre.");
                return;
            }

            if (!Validacion.TieneTexto(txtDescripcion.Text))
            {
                MessageBox.Show("Debe ingresar una descripción.");
                return;
            }

            if (!Validacion.EsPrecio(txtPrecio.Text))
            {
                MessageBox.Show("Debe ingresar un precio válido.");
                return;
            }

            if (cboMarca.SelectedItem == null)
            {
                MessageBox.Show("Debe seleccionar una marca.");
                return;
            }

            if (cboCategoria.SelectedItem == null)
            {
                MessageBox.Show("Debe seleccionar una categoría.");
                return;
            }
            
            ArticuloNegocio negocio = new ArticuloNegocio();
            if (articulo == null)
            {
                if (negocio.existeCodigo(txtCodigo.Text))
                {
                    MessageBox.Show("Ya existe un artículo con ese código.");
                    return;
                }
            }
            else
            {
                if (negocio.existeCodigo(txtCodigo.Text, articulo.Id))
                {
                    MessageBox.Show("Ya existe otro artículo con ese código.");
                    return;
                }
            }

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
