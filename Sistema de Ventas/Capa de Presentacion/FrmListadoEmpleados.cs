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
    public partial class FrmListadoEmpleados : Form
    {
        clsEmpleado E = new clsEmpleado();
        int Listado = 0;
        public FrmListadoEmpleados()
        {
            InitializeComponent();
        }

        private void FrmListadoEmpleados_Load(object sender, EventArgs e)
        {
            timer1.Start();
            timer1.Interval = 1000;
            MostrarListadoEmpleados();
            dataGridView1.RowHeadersVisible = false;
            //dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //dataGridView1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopCenter;
        }

        private void MostrarListadoEmpleados() {
            DataTable dt = new DataTable();
            dt = E.ListadoEmpleados();
            dataGridView1.Rows.Clear();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dataGridView1.Rows.Add(dt.Rows[i][0]);
                dataGridView1.Rows[i].Cells[0].Value = dt.Rows[i][0].ToString();
                dataGridView1.Rows[i].Cells[1].Value = dt.Rows[i][1].ToString();
                dataGridView1.Rows[i].Cells[2].Value = dt.Rows[i][2].ToString();
                dataGridView1.Rows[i].Cells[3].Value = dt.Rows[i][3].ToString();
                dataGridView1.Rows[i].Cells[4].Value = dt.Rows[i][4].ToString();
                dataGridView1.Rows[i].Cells[5].Value = dt.Rows[i][5].ToString();
                dataGridView1.Rows[i].Cells[6].Value = Convert.ToDateTime(dt.Rows[i][6].ToString()).ToShortDateString();
                dataGridView1.Rows[i].Cells[7].Value = dt.Rows[i][7].ToString();
                dataGridView1.Rows[i].Cells[8].Value = dt.Rows[i][8].ToString();
            }
            dataGridView1.ClearSelection();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            FrmRegistrarEmpleados E = new FrmRegistrarEmpleados();
            E.txtIdE.Text = "0";
            Program.IdCargo = 0;
            dataGridView1.ClearSelection();
            E.Visible = true;
            Visible = false;
            
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0) {
                if (dataGridView1.SelectedRows.Count > 0) {
                    FrmRegistrarEmpleados E = new FrmRegistrarEmpleados();
                    E.txtIdE.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                    Program.IdCargo = Convert.ToInt32(dataGridView1.CurrentRow.Cells[1].Value.ToString());
                    E.txtDni.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                    E.txtApellidos.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                    E.txtNombres.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
                    if (dataGridView1.CurrentRow.Cells[5].Value.ToString() == "M")
                        E.rbnMasculino.Checked = true;
                    else
                        E.rbnFemenino.Checked = true;
                    E.dateTimePicker1.Value = Convert.ToDateTime(dataGridView1.CurrentRow.Cells[6].Value.ToString());
                    E.txtDireccion.Text = dataGridView1.CurrentRow.Cells[7].Value.ToString();
                    E.Visible = true;
                    Visible = false;
                }
                dataGridView1.ClearSelection(); 
                timer1.Start();
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Está Seguro que Desea Salir.?", "Sistema de Ventas.", MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            switch(Listado){
                case 0: MostrarListadoEmpleados(); break;
            }
        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.RowCount > 0)
            {
                dataGridView1.Rows[dataGridView1.CurrentRow.Index].Selected = true;
                timer1.Stop();
            }
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Desea Crear Una Cuenta de Usuario Para este Empleado.?","Sistema de Ventas.", MessageBoxButtons.YesNoCancel) == DialogResult.Yes) {
                FrmRegistrarUsuarios U = new FrmRegistrarUsuarios();
                Program.IdEmpleado = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value.ToString());
                U.lblEmpleado.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString() + ", " +
                                     dataGridView1.CurrentRow.Cells[4].Value.ToString();
                U.lblDni.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                U.lblCargo.Text = dataGridView1.CurrentRow.Cells[8].Value.ToString();
                U.Show();
            }
        }

        private void txtDatos_TextChanged(object sender, EventArgs e)
        {
            if (txtDatos.TextLength>0)
            {

                DataTable dt = new DataTable();
                E.Nombres = txtDatos.Text;
                dt = E.BuscarEmpleado(E.Nombres);
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
                        dataGridView1.Rows[i].Cells[5].Value = dt.Rows[i][5].ToString();
                        dataGridView1.Rows[i].Cells[6].Value = Convert.ToDateTime(dt.Rows[i][6].ToString()).ToShortDateString();
                        dataGridView1.Rows[i].Cells[7].Value = dt.Rows[i][7].ToString();
                        dataGridView1.Rows[i].Cells[8].Value = dt.Rows[i][8].ToString();
                    }
                    dataGridView1.ClearSelection();
                    timer1.Stop();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                MostrarListadoEmpleados();
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


        private void btn_comp_Click(object sender, EventArgs e)
        {
            FrmRegistroProductos RP = new FrmRegistroProductos();
            RP.Visible = true;
            Visible = false;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void btn_ven_Click_1(object sender, EventArgs e)
        {
            FrmRegistroVentas RV = new FrmRegistroVentas();
            RV.Visible = true;
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

        private void bnt_min_Click_1(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btn_cerrar_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }


    }
}
