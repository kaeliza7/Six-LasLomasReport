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


namespace Capa_de_Presentacion
{
    public partial class FrmMenuPrincipal : Form
    {

        public FrmMenuPrincipal()
        {
            InitializeComponent();
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


        private void bnt_min_Click(object sender, EventArgs e)
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
        private void panel3_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        

        private void btn_emp_Click(object sender, EventArgs e)
        {
            FrmListadoEmpleados LE = new FrmListadoEmpleados();
            LE.Visible = true;
            Visible = false;
        }
    


        private void Abrirformularios(object f)
        {
            if (this.pnl_cont.Controls.Count > 0)
                this.pnl_cont.Controls.RemoveAt(0);

            Form abrir = f as Form;
            abrir.TopLevel = false;
            abrir.Dock = DockStyle.Fill;
            this.pnl_cont.Controls.Add(abrir);
            this.pnl_cont.Tag = abrir;
            abrir.Show();
           
        }


        private void btn_cerrar_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btn_ven_Click_1(object sender, EventArgs e)
        {
            //FrmRegistroVentas RV = new FrmRegistroVentas();
            //RV.Visible = true;
            //Visible = false;

            Abrirformularios(new FrmRegistroVentas());
        }

        private void btn_rep_Click(object sender, EventArgs e)
        {
            pnl_rep.Visible = true;
        }

        private void btn_comp_Click(object sender, EventArgs e)
        {
            //FrmRegistroProductos RP = new FrmRegistroProductos();
            //RP.Visible = true;
            //Visible = false;

            Abrirformularios(new FrmRegistrarCategoria());
        }

        private void btn_pro_Click(object sender, EventArgs e)
        {
            //FrmListadoProductos LP = new FrmListadoProductos();
            //LP.Visible = true;
            //Visible = false;

            Abrirformularios(new FrmListadoProductos());
        }

        private void btn_emp_Click_1(object sender, EventArgs e)
        {
            Abrirformularios(new FrmListadoEmpleados());
        }
    }
}
