using System;
using System.Windows.Forms;
using Dominio;
using System.Collections.Generic;

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
            List<Marca> marcas = new List<Marca>();

            marcas.Add(new Marca { Id = 1, Descripcion = "Samsung" });
            marcas.Add(new Marca { Id = 2, Descripcion = "Sony" });
            marcas.Add(new Marca { Id = 3, Descripcion = "Logitech" });

            cboMarca.DataSource = marcas;

            List<Categoria> categorias = new List<Categoria>();

            categorias.Add(new Categoria { Id = 1, Descripcion = "Celulares" });
            categorias.Add(new Categoria { Id = 2, Descripcion = "Televisores" });
            categorias.Add(new Categoria { Id = 3, Descripcion = "Accesorios" });

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
            // El guardado llega en la Etapa 2. Por ahora el formulario solo se cierra.
            Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
