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
    public partial class FrmRegistrarUsuarios : Form
    {
        clsUsuarios U = new clsUsuarios();

        public FrmRegistrarUsuarios()
        {
            InitializeComponent();
        }

        private void FrmRegistrarUsuarios_Load(object sender, EventArgs e)
        {
            
        }

        private void btnGrabar_Click(object sender, EventArgs e)
        {
            if (txtUser.Text.Trim() != "")
            {
                if (txtPassword.Text.Trim() != "")
                {
                    if (Program.IdEmpleado != 0)
                    {
                        U.IdEmpleado = Program.IdEmpleado;
                        U.User = txtUser.Text;
                        U.Password = txtPassword.Text;
                        MessageBox.Show(U.RegistrarUsuarios(), "Sistema de Ventas.", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                        Limpiar();
                    }
                    else {
                        MessageBox.Show("Lo Sentimos, Pero Usted debe Eligir un \n Empleado para Crear una Cuenta de Usuario.\n \n G R A C I A S.", "Sistema de Ventas.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Por Favor Ingrese su Contraseña.", "Sistema de Ventas.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtPassword.Focus();
                }
            }
            else {
                MessageBox.Show("Por Favor Ingrese su Usuario.", "Sistema de Ventas.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtUser.Focus();
            }
            FrmListadoEmpleados LE = new FrmListadoEmpleados();
            LE.Visible = true;
            Visible = false;
        }

        private void Limpiar() {
            txtPassword.Clear();
            txtUser.Clear();
            Program.IdEmpleado = 0;
            txtUser.Focus();
            lblCargo.Text = "";
            lblDni.Text = "";
            lblEmpleado.Text = "";
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]

        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);

        private void FrmMenuPrincipal_Activated(object sender, EventArgs e)
        {
            lblUsuario.Text = Program.NombreEmpleadoLogueado;
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

        private void btn_rep_Click_1(object sender, EventArgs e)
        {
            pnl_rep.Visible = true;
        }

        private void bnt_min_Click_1(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btn_res_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
            btn_max.Visible = true;
            btn_res.Visible = false;
        }

        private void btn_max_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            btn_max.Visible = false;
            btn_res.Visible = true;
        }

        private void btn_cerrar_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
