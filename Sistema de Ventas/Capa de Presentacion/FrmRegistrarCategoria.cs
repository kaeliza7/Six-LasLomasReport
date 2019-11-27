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
using System.Runtime.InteropServices;

namespace Capa_de_Presentacion
{
    public partial class FrmRegistrarCategoria : Form
    {
        private clsCategoria C = new clsCategoria();

        public FrmRegistrarCategoria()
        {
            InitializeComponent();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Está Seguro que Desea Salir.?", "Sistema de Ventas.", MessageBoxButtons.YesNo,MessageBoxIcon.Error) == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            clsCategoria C = new clsCategoria();
            FrmRegistroProductos RP = new FrmRegistroProductos();
            String Mensaje = "";
            try{
                if (txtCategoria.Text.Trim() != "")
                {
                    if (Program.Evento == 0){
                        C.Descripcion = txtCategoria.Text;
                        Mensaje = C.RegistrarCategoria();
                        if (Mensaje == "Categoria ya se encuentra Registrada."){
                            MessageBox.Show(Mensaje, "Sistema de Ventas.", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            RP.Visible = true;
                            Visible = false;
                        }
                        else {
                            MessageBox.Show(Mensaje, "Sistema de Ventas.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            Limpiar();
                            RP.Visible = true;
                            Visible = false;
                        }

                    }else{
                        C.IdC = Convert.ToInt32(IdC.Text);
                        C.Descripcion = txtCategoria.Text;
                        MessageBox.Show(C.ActualizarCategoria(), "Sistema de Ventas.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Limpiar();
                    }
                }else {
                    MessageBox.Show("Por Favor Digíte Datos.","Sistema de Ventas.",MessageBoxButtons.OK,MessageBoxIcon.Error);
                    txtCategoria.Focus();
                }
            }catch (Exception ex){
                MessageBox.Show(ex.Message);
            }
        }


        private void Limpiar() {
            txtCategoria.Clear();
            txtCategoria.Focus();
        }

        private void lblUsuario_Click(object sender, EventArgs e)
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

        private void btn_max_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            btn_max.Visible = false;
            btn_res.Visible = true;
        }

        private void btn_res_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
            btn_max.Visible = true;
            btn_res.Visible = false;
        }

        private void btn_cerrar_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
