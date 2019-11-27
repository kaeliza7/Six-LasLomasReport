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
    public partial class FrmRegistroProductos : Form
        
    {
        private clsCategoria C = new clsCategoria();
        private clsProducto P = new clsProducto();

        public FrmRegistroProductos()
        {
            InitializeComponent();
        }

        private void FrmRegistroProductos_Load(object sender, EventArgs e)
        {
            ListarElementos();
        }

        private void ListarElementos() {
            if (IdC.Text.Trim() != "")
            {
                cbxCategoria.DisplayMember = "Descripcion";
                cbxCategoria.ValueMember = "IdCategoria";
                cbxCategoria.DataSource = C.Listar();
                cbxCategoria.SelectedValue = IdC.Text;
            }
            else
            {
                cbxCategoria.DisplayMember = "Descripcion";
                cbxCategoria.ValueMember = "IdCategoria";
                cbxCategoria.DataSource = C.Listar();
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            String Mensaje = "";
            if (txtProducto.Text.Trim() != "")
            { 
                if (txtPCompra.Text.Trim() != "")
                {
                    if (txtStock.Text.Trim() != "")
                    {
                        if (Program.Evento == 0)
                        {
                            P.IdCategoria = Convert.ToInt32(cbxCategoria.SelectedValue);
                            P.Producto = txtProducto.Text;
                            P.PrecioCompra = Convert.ToDecimal(txtPCompra.Text);
                            P.Stock = Convert.ToInt32(txtStock.Text);
                            Mensaje = P.RegistrarProductos();
                            if (Mensaje == "Este Producto ya ha sido Registrado.")
                            {
                                MessageBox.Show(Mensaje, "Sistema de Ventas.", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            }
                            else
                            {
                                MessageBox.Show(Mensaje, "Sistema de Ventas.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                Limpiar();
                            }
                        }
                        else
                        {
                            P.IdP = Convert.ToInt32(txtIdP.Text);
                            P.IdCategoria = Convert.ToInt32(cbxCategoria.SelectedValue);
                            P.Producto = txtProducto.Text;
                            P.PrecioCompra = Convert.ToDecimal(txtPCompra.Text);
                            P.Stock = Convert.ToInt32(txtStock.Text);
                            MessageBox.Show(P.ActualizarProductos(), "Sistema de Ventas.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            Limpiar();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Por Favor Ingrese Stock del Producto.", "Sistema de Ventas.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtStock.Focus();
                    }
                }       
                else
                {
                    MessageBox.Show("Por Favor Ingrese Precio de Compra del Producto.", "Sistema de Ventas.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtPCompra.Focus();
                }              
            }
            else
            {
                MessageBox.Show("Por Favor Ingrese Nombre del Producto.", "Sistema de Ventas.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtProducto.Focus();
            }

            FrmListadoProductos LP = new FrmListadoProductos();
          /*  FrmMenuPrincipal M = new FrmMenuPrincipal()*/;
            LP.timer1.Start();
            //LP.MdiParent = this;
            //LP.Show();
            //LP.WindowState = FormWindowState.Maximized;
            LP.Visible = true;
            Visible = false;
        }

        private void btnCategoria_Click(object sender, EventArgs e)
        {
            FrmRegistrarCategoria C = new FrmRegistrarCategoria();
            C.Visible = true;
            Visible = true;
        }

        private void Limpiar() {
            txtProducto.Text = "";
            txtPCompra.Clear();
            IdC.Clear();
            txtIdP.Clear();
            txtStock.Clear();
            txtProducto.Focus();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Está Seguro que Desea Salir.?", "Sistema de Ventas.", MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.Yes) {
                this.Close();
            }

        }

        private void txtPCompra_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (this.txtPCompra.Text.Contains('.'))
            {
                if (!char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }

                if (e.KeyChar == '\b')
                {
                    e.Handled = false;
                }
            }
            else
            {
                if (!char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }

                if (e.KeyChar == '.' || e.KeyChar == '\b')
                {
                    e.Handled = false;
                }
            }
        }


        private void txtStock_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else
                if (Char.IsControl(e.KeyChar))
                {
                    e.Handled = false;
                }
                else
                {
                    e.Handled = true;
                } 
        }

    

        private void txtMarca_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (e.KeyChar >= 48 && e.KeyChar <= 57 || e.KeyChar == 8)
            //    e.Handled = false;
            //else
            //    e.Handled = true;
        }

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]

        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);

        private void FrmMenuPrincipal_Activated(object sender, EventArgs e)
        {
            //lblUsuario.Text = Program.NombreEmpleadoLogueado;
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
        private void btn_pro_Click(object sender, EventArgs e)
        {
            FrmListadoProductos LP = new FrmListadoProductos();
            LP.Visible = true;
            Visible = false;

        }

        private void btn_emp_Click(object sender, EventArgs e)
        {
            FrmListadoEmpleados LE = new FrmListadoEmpleados();
            LE.Visible = true;
            Visible = false;
        }

        private void btn_rep_Click(object sender, EventArgs e)
        {
            //pnl_rep.Visible = true;
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
            FrmListadoProductos RP = new FrmListadoProductos();
            RP.Visible = true;
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
           //pnl_rep.Visible = true;
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
