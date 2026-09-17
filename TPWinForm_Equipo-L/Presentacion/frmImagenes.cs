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

namespace Presentacion
{
    public partial class frmImagenes : Form
    {
        private List<Imagen> imagenes = new List<Imagen>();
        private int indiceActual = 0;
        public frmImagenes()
        {
            InitializeComponent();
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
    }
}
