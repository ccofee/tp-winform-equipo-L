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
    public partial class frmPrincipal : Form
    {
        private List<Articulo> listaArticulos;

        public frmPrincipal()
        {
            InitializeComponent();
        }

        private void frmPrincipal_Load(object sender, EventArgs e)
        {
            listaArticulos = new List<Articulo>();

            Articulo art1 = new Articulo();
            art1.Codigo = "A01";
            art1.Nombre = "Televisor Samsung 50";
            art1.Descripcion = "Smart TV 4K UHD";
            art1.Precio = 450000;

            Articulo art2 = new Articulo();
            art2.Codigo = "A02";
            art2.Nombre = "Mouse Logitech G305";
            art2.Descripcion = "Mouse inalámbrico gamer";
            art2.Precio = 45000;

            listaArticulos.Add(art1);
            listaArticulos.Add(art2);

            dgvArticulos.DataSource = listaArticulos;

            dgvArticulos.Columns["Id"].Visible = false;
        }

        private void btnVerDetalle_Click(object sender, EventArgs e)
        {
            Articulo seleccionado = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;

            frmDetalle detalle = new frmDetalle(seleccionado);
            detalle.ShowDialog();
        }
    }
}
