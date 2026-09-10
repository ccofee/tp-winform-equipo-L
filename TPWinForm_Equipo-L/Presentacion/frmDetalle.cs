using Dominio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Presentacion
{
    public partial class frmDetalle : Form
    {

        private Articulo articulo;

        public frmDetalle()
        {
            InitializeComponent();
        }

        //constructor sobrecargado, recibe el objeto seleccionado desde el la grilla de fmrPrincipal
        public frmDetalle(Articulo articulo)
        {
            InitializeComponent();
            this.articulo = articulo;
        }

        public void frmDetalle_load(object sender, EventArgs e)
        {
            //cargar los datos del objeto recibido en los controles del formulario
            txtCodigo.Text = articulo.Codigo;
            txtNombre.Text = articulo.Nombre;
            txtDescripcion.Text = articulo.Descripcion;
            txtPrecio.Text = articulo.Precio.ToString("0.00");


            //por si vienen null desde la base de datos
            if(articulo.Marca != null)
            {
                txtMarca.Text = articulo.Marca.Descripcion;
            }

            if(articulo.Categoria != null)
            {
                txtCategoria.Text = articulo.Categoria.Descripcion;
            }

            //evaluacion de la imagen
            if (articulo.Imagenes != null && articulo.Imagenes.Count > 0 && !string.IsNullOrEmpty(articulo.Imagenes[0].ImagenUrl))
            {
                CargarImagen(articulo.Imagenes[0].ImagenUrl);
            }
            else
            {
             //imageen por defecto si no hay imagenes
                pbxArticulo.Image = Properties.Resources.sinfoto;
            }
        }


        private void CargarImagen(string url)
        {
            try
            {
                pbxArticulo.Load(url);
            }
            catch (Exception)
            {

                pbxArticulo.Image = Properties.Resources.sinfoto;
            }
        }


        private void btnVolver_Click(object sender, EventArgs e)
        {
            Close();
        }

    }
}
