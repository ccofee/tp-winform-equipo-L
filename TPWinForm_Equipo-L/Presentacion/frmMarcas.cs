using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Dominio;
using Negocio;

namespace Presentacion
{
    public partial class frmMarcas : Form
    {
        private List<Marca> listaMarcas;

        public frmMarcas()
        {
            InitializeComponent();
        }

        private void frmMarcas_Load(object sender, EventArgs e)
        {
            CargarListado();
        }

        private void CargarListado()
        {
            MarcaNegocio negocio = new MarcaNegocio();

            listaMarcas = negocio.Listar();
            dgvMarcas.DataSource = null;
            dgvMarcas.DataSource = listaMarcas;

            dgvMarcas.Columns["Id"].Visible = false;
            dgvMarcas.Columns["Descripcion"].HeaderText = "Marca";
        }
    }
}
