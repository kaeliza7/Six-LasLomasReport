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
    public partial class FrmRegistrarEmpleados : Form
    {
        clsCargo C = new clsCargo();
        clsEmpleado E = new clsEmpleado();
        int Listado = 0;
        public FrmRegistrarEmpleados()
        {
            InitializeComponent();
        }

        private void FrmRegistrarEmpleados_Load(object sender, EventArgs e)
        {
            timer1.Start();
            timer1.Interval = 1000;
            CargarComboBox();
        }

        private void CargarComboBox(){
            comboBox1.DataSource = C.Listar();
            comboBox1.DisplayMember = "Descripcion";
            comboBox1.ValueMember = "IdCargo";
        }

        private void btnGrabar_Click(object sender, EventArgs e)
        {
            FrmListadoEmpleados LE = new FrmListadoEmpleados();
            E.IdEmpleado = Convert.ToInt32(txtIdE.Text);
            E.IdCargo = Convert.ToInt32(comboBox1.SelectedValue);
            E.Dni = txtDni.Text;
            E.Apellidos = txtApellidos.Text;
            E.Nombres = txtNombres.Text;
            E.Sexo=rbnMasculino.Checked==true?'M':'F';
            E.FechaNac = Convert.ToDateTime(dateTimePicker1.Value);
            E.Direccion = txtDireccion.Text;
            MessageBox.Show(E.MantenimientoEmpleados(), "Sistema de Ventas.", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            Limpiar();
            LE.Visible = true;
            Visible = false;
     
        }

        private void Limpiar() {
            txtApellidos.Clear();
            txtDireccion.Clear();
            txtDni.Clear();
            txtNombres.Clear();
            rbnMasculino.Checked = true;
            dateTimePicker1.Value = DateTime.Now;
            txtIdE.Clear();
            Program.IdCargo = 0;
            comboBox1.SelectedIndex = 0;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            switch (Listado) {
                case 0: CargarComboBox(); break;
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            timer1.Stop();
        }

        private void FrmRegistrarEmpleados_Activated(object sender, EventArgs e)
        {
            if (Program.IdCargo != 0)
                comboBox1.SelectedValue = Program.IdCargo;
        }

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]

        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);

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

        
        private void pictureBox1_Click(object sender, EventArgs e)
        {

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
