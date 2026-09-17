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
    public partial class frmImagenes : Form
    {
        private List<Imagen> imagenes = new List<Imagen>();
        private int indiceActual = 0;
        private int idArticulo;
        public frmImagenes()
        {
            InitializeComponent();
        }
        public frmImagenes(int idArticulo)
        {
            InitializeComponent();
            this.idArticulo = idArticulo;
        }
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            Imagen imagen = new Imagen();

            imagen.ImagenUrl = txtImagenLink.Text;

            imagenes.Add(imagen);

            lstImagenes.Items.Add(imagen);

            txtImagenLink.Clear();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (lstImagenes.SelectedItem != null)
            {
                Imagen imagen = (Imagen)lstImagenes.SelectedItem;

                imagenes.Remove(imagen);
                lstImagenes.Items.Remove(imagen);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnSiguiente_Click(object sender, EventArgs e)
        {
            if (imagenes.Count > 0)
            {
                indiceActual++;

                if (indiceActual >= imagenes.Count)
                    indiceActual = 0;

                lstImagenes.SelectedIndex = indiceActual;
            }
        }

        private void btnAnterior_Click(object sender, EventArgs e)
        {
            if (imagenes.Count > 0)
            {
                indiceActual--;

                if (indiceActual < 0)
                    indiceActual = imagenes.Count - 1;

                lstImagenes.SelectedIndex = indiceActual;
            }
        }

        private void txtImagenLink_TextChanged(object sender, EventArgs e)
        {
            try
            {
                pbImagen.Load(txtImagenLink.Text);
            }
            catch
            {
                pbImagen.Image = null;
            }
        }
        private void frmImagenes_Load(object sender, EventArgs e)
        {
            if (idArticulo > 0)
            {
                ImagenNegocio negocio = new ImagenNegocio();

                imagenes = negocio.listarPorArticulo(idArticulo);

                lstImagenes.Items.Clear();

                foreach (Imagen imagen in imagenes)
                {
                    lstImagenes.Items.Add(imagen);
                }

                if (imagenes.Count > 0)
                {
                    indiceActual = 0;
                    lstImagenes.SelectedIndex = indiceActual;
                }
            }
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            ImagenNegocio negocio = new ImagenNegocio();

            negocio.eliminarPorArticulo(idArticulo);

            foreach (Imagen imagen in imagenes)
            {
                imagen.IdArticulo = idArticulo;
                negocio.agregar(imagen);
            }

            Close();
        }
    }
}
