using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using CapaLogicaNegocio;

namespace Capa_de_Presentacion
{
    public partial class FrmListadoProductos : Form
    {
        int Listado = 0;
        private clsProducto P = new clsProducto();

        public FrmListadoProductos()
        {
            InitializeComponent();
        }

        private void FrmProductos_Load(object sender, EventArgs e)
        {
            timer1.Start();
            timer1.Interval = 5000;
            CargarListado();
            dataGridView1.ClearSelection();
            dataGridView1.RowHeadersVisible = false;
        }

        private void CargarListado() {
            DataTable dt = new DataTable();
            dt = P.Listar();

            dataGridView1.Rows.Clear();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dataGridView1.Rows.Add(dt.Rows[i][0]);
                dataGridView1.Rows[i].Cells[0].Value = dt.Rows[i][0].ToString();
                dataGridView1.Rows[i].Cells[1].Value = dt.Rows[i][1].ToString();
                dataGridView1.Rows[i].Cells[2].Value = dt.Rows[i][2].ToString();
                dataGridView1.Rows[i].Cells[3].Value = dt.Rows[i][3].ToString();
                dataGridView1.Rows[i].Cells[4].Value = dt.Rows[i][4].ToString();
            }
            
            dataGridView1.ClearSelection();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                dataGridView1.Rows[dataGridView1.CurrentRow.Index].Selected = true;
                timer1.Stop();
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Está Seguro que Desea Salir.?", "Sistema de Ventas.", MessageBoxButtons.YesNo,MessageBoxIcon.Error) == DialogResult.Yes)
            {
                Close();
            }
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            //if (dataGridView1.SelectedRows.Count > 0){
            //    //DevComponents.DotNetBar.MessageBoxEx.Show("La Fila no debe Estar Seleccionada.");
            //    //FrmRegistroProductos P = new FrmRegistroProductos();
            //    //dataGridView1.ClearSelection();
            //    //P.Show();
            //}else{
                FrmRegistroProductos RP = new FrmRegistroProductos();
                if (dataGridView1.SelectedRows.Count > 0)
                    Program.Evento = 1;
                else
                    Program.Evento = 0;
                dataGridView1.ClearSelection();

            RP.Visible = true;
            Visible = false;

           // }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0) {

                FrmRegistroProductos RP = new FrmRegistroProductos();
                RP.txtIdP.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                RP.IdC.Text= dataGridView1.CurrentRow.Cells[1].Value.ToString();
                RP.txtProducto.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                RP.txtPCompra.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                RP.txtStock.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
                if (dataGridView1.SelectedRows.Count > 0)

                    Program.Evento = 1;
                else
                    Program.Evento = 0;
                dataGridView1.ClearSelection();
                RP.Visible = true;
                Visible = false;
            }
            else {
                //FrmRegistroProductos P = new FrmRegistroProductos();
                MessageBox.Show("Debe Seleccionar la Fila a Editar.","Sistema de Ventas.",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                //P.Visible = true;
                //Visible = false;
            }

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            switch (Listado)
            {
                case 0: CargarListado(); break;        
            }
        }

      
        private void txtBuscarProducto_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                
                timer1.Stop();
            }
            else {
                CargarListado();
                timer1.Start();
            }
        }

        private void dataGridView1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar==13){
             if(dataGridView1.SelectedRows.Count>0)
            dataGridView1.ClearSelection();
            }
        }


        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]

        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);

        private void FrmMenuPrincipal_Activated(object sender, EventArgs e)
        {
            lblUsuario.Text = Program.NombreEmpleadoLogueado;
        }

        private void FrmMenuPrincipal_Load(object sender, EventArgs e)
        {
            timer1.Interval = 500;
            timer1.Start();
        }

        private void FrmMenuPrincipal_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }



        private void btn_cerrar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void bnt_min_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void panel3_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void btn_ven_Click(object sender, EventArgs e)
        {
            FrmRegistroVentas RV = new FrmRegistroVentas();
            RV.Visible = true;
            Visible = false;
            
        }

        private void btn_comp_Click_1(object sender, EventArgs e)
        {
            FrmRegistroProductos RP = new FrmRegistroProductos();
            RP.Visible = true;
            Visible = false;
        }

        private void btn_pro_Click_1(object sender, EventArgs e)
        {
            FrmListadoProductos LP = new FrmListadoProductos();
            LP.Visible = true;
            Visible = false;
        }

        private void btn_emp_Click_1(object sender, EventArgs e)
        {
            FrmListadoEmpleados LE = new FrmListadoEmpleados();
            LE.Visible = true;
            Visible = false;
        }

        private void txtBuscarProducto_TextChanged(object sender, EventArgs e)
        {
            if (txtBuscarProducto.TextLength > 0)
            {
                DataTable dt = new DataTable();
                P.Producto = txtBuscarProducto.Text;
                dt = P.BusquedaProductos(P.Producto);
                try
                { 
                    dataGridView1.Rows.Clear();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        dataGridView1.Rows.Add(dt.Rows[i][0]);
                        dataGridView1.Rows[i].Cells[0].Value = dt.Rows[i][0].ToString();
                        dataGridView1.Rows[i].Cells[1].Value = dt.Rows[i][1].ToString();
                        dataGridView1.Rows[i].Cells[2].Value = dt.Rows[i][2].ToString();
                        dataGridView1.Rows[i].Cells[3].Value = dt.Rows[i][3].ToString();
                        dataGridView1.Rows[i].Cells[4].Value = dt.Rows[i][4].ToString();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                dataGridView1.ClearSelection();
            }
            else
            {
                CargarListado();
            }
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            Program.IdProducto = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value.ToString());
            Program.Descripcion = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            Program.PrecioVenta = Convert.ToDecimal(dataGridView1.CurrentRow.Cells[3].Value.ToString());
            Program.Stock = Convert.ToInt32(dataGridView1.CurrentRow.Cells[4].Value.ToString());
            this.Close();
        }

        private void bnt_min_Click_1(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

    }
}
