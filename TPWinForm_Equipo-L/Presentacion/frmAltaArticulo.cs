using System;
using System.Windows.Forms;

namespace Presentacion
{
    public partial class frmAltaArticulo : Form
    {
        public frmAltaArticulo()
        {
            InitializeComponent();
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
