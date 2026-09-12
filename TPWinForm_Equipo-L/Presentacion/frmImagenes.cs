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
    }
}
