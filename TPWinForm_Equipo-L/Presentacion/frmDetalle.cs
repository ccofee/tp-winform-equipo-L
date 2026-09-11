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

        private int indiceImagen = 0;

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

            //inicializa el carrusel de imagenes
            indiceImagen = 0;
            MostrarImagenActual();
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

        //metodo para mostrar la imagen actual segun el indice y botones de siguiente y anterior
        private void MostrarImagenActual()
        {
            //si no hay o lalista esta vacia
            if(articulo.Imagenes == null || articulo.Imagenes.Count == 0)
            {
                pbxArticulo.Image = Properties.Resources.sinfoto;
                btnSiguiente.Enabled = false;
                btnAnterior.Enabled = false;
                lblContadorImagen.Text = "0 / 0";
                return;
            }

            //si tiene una sola imagen
            if(articulo.Imagenes.Count == 1)
            {
                btnAnterior.Enabled = false;
                btnSiguiente.Enabled = false;
            }
            else
            {
                btnAnterior.Enabled = true;
                btnSiguiente.Enabled = true;
            }

            lblContadorImagen.Text = $"{indiceImagen + 1} / {articulo.Imagenes.Count}";

            CargarImagen(articulo.Imagenes[indiceImagen].ImagenUrl);
        }


        //boton anterior y siguiente para recorrer la lista de imagenes del objeto articulo
        private void btnAnterior_Click(object sender, EventArgs e)
        {
            indiceImagen++;

            if(indiceImagen >= articulo.Imagenes.Count)
            {
                indiceImagen = 0;
            }

            MostrarImagenActual();
        }

        private void btnSiguiente_Click(object sender, EventArgs e)
        {
            indiceImagen--;

            if(indiceImagen < 0)
            {
                indiceImagen = articulo.Imagenes.Count - 1;
            }

            MostrarImagenActual();
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            Close();
        }

    }
}
