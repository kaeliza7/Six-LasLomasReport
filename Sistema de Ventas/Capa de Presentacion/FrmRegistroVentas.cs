﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CapaLogicaNegocio;
using System.Runtime.InteropServices;

namespace Capa_de_Presentacion
{
    public partial class FrmRegistroVentas :Form
    {
        clsVentas Ventas = new clsVentas();
        clsDetalleVenta Detalle = new clsDetalleVenta();

        private List<clsVenta> lst = new List<clsVenta>();

        public FrmRegistroVentas()
        {
            InitializeComponent();
        }

        private void FrmVentas_Load(object sender, EventArgs e)
        {
            GenerarIdVenta();
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }

        private void GenerarIdVenta()
        {
            txtIdVenta.Text = Ventas.GenerarIdVenta();            
        }

        private void FrmVentas_Activated(object sender, EventArgs e)
        {
            //txtIdProducto.Text = Program.IdCliente+"";
            txtIdProducto.Text = Program.IdProducto + "";
            txtDescripcion.Text = Program.Descripcion;
            txtStock.Text = Program.Stock + "";
            txtPVenta.Text = Program.PrecioVenta + "";
        }

        private void btnBusquedaProducto_Click(object sender, EventArgs e)
        {
        
            FrmListadoProductos LP = new FrmListadoProductos();
            LP.Show();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            clsVenta V = new clsVenta();
            Decimal SubTotal;
            
            if (txtDescripcion.Text.Trim() != "")
            {
                if (txtCantidad.Text.Trim() != "")
                {
                    if (Convert.ToInt32(txtCantidad.Text) >= 0)
                    {
                        if (Convert.ToInt32(txtCantidad.Text) <= Convert.ToInt32(txtStock.Text))
                        {

                            V.IdProducto = Convert.ToInt32(txtIdProducto.Text);
                            V.IdVenta = Convert.ToInt32(txtIdVenta.Text);
                            V.Descripcion = txtDescripcion.Text ;
                            V.Cantidad = Convert.ToInt32(txtCantidad.Text);
                            V.PrecioVenta = Convert.ToDecimal(txtPVenta.Text);
                            SubTotal = ((Convert.ToDecimal(txtPVenta.Text) * Convert.ToInt32(txtCantidad.Text)));
                            V.SubTotal = Math.Round(SubTotal, 2);
                            lst.Add(V);
                            LlenarGrilla();
                            Limpiar();
   
                        }
                        else
                        {
                            MessageBox.Show("Stock Insuficiente para Realizar la Venta.", "Sistema de Ventas.", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Cantidad Ingresada no Válida.", "Sistema de Ventas.", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                        txtCantidad.Clear();
                        txtCantidad.Focus();
                    }
                }
                else
                {
                    MessageBox.Show("Por Favor Ingrese Cantidad a Vender.", "Sistema de Ventas.", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                    txtCantidad.Focus();
                }
            }
            else
            {
                MessageBox.Show("Por Favor Busque el Producto a Vender.", "Sistema de Ventas.", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
    
        }

        private void LlenarGrilla()
        {
            Decimal SumaSubTotal = 0;  Decimal SumaTotal = 0;
            dataGridView1.Rows.Clear();
            for (int i = 0; i < lst.Count; i++)
            {
                dataGridView1.Rows.Add();
                dataGridView1.Rows[i].Cells[0].Value = lst[i].IdVenta;
                dataGridView1.Rows[i].Cells[1].Value = lst[i].Cantidad;
                dataGridView1.Rows[i].Cells[2].Value = lst[i].Descripcion;
                dataGridView1.Rows[i].Cells[3].Value = lst[i].PrecioVenta;
                dataGridView1.Rows[i].Cells[4].Value = lst[i].SubTotal;
                dataGridView1.Rows[i].Cells[5].Value = lst[i].IdProducto;
                SumaSubTotal += Convert.ToDecimal(dataGridView1.Rows[i].Cells[4].Value);
                
            }

            dataGridView1.Rows.Add();
            dataGridView1.Rows.Add();
            dataGridView1.Rows[lst.Count + 1].Cells[3].Value = "SUB-TOTAL  S/.";
            dataGridView1.Rows[lst.Count + 1].Cells[4].Value = SumaSubTotal;
            dataGridView1.Rows.Add();
            dataGridView1.Rows[lst.Count + 2].Cells[3].Value = "      I.G.V.        %";
            //dataGridView1.Rows[lst.Count + 2].Cells[4].Value = SumaIgv;
            dataGridView1.Rows.Add();
            dataGridView1.Rows[lst.Count + 2].Cells[3].Value = "     TOTAL     S/.";
            SumaTotal += SumaSubTotal;
            dataGridView1.Rows[lst.Count + 2].Cells[4].Value = SumaTotal;
            dataGridView1.ClearSelection();
        }

        private void Limpiar()
        {
            txtDescripcion.Clear();
            txtStock.Clear();
            txtPVenta.Clear();
            txtCantidad.Clear();
            txtCantidad.Focus();
            Program.Descripcion = "";
            Program.Stock = 0;
            Program.PrecioVenta = 0;
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Está Seguro que Desea Salir.?", "Sistema de Ventas", MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void btnEliminarItem_Click_1(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                if (dataGridView1.Rows[dataGridView1.CurrentRow.Index].Selected == true)
                {
                    if (Convert.ToString(dataGridView1.CurrentRow.Cells[2].Value) != "")
                    {
                        dataGridView1.Rows.RemoveAt(dataGridView1.CurrentRow.Index);
                        lst.RemoveAt(dataGridView1.CurrentRow.Index);
                        LlenarGrilla();
                        MessageBox.Show("Producto Eliminado de la Lista Ok.", "Sistema de Ventas.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("No Existe Ningun Elemento en la Lista.", "Sistema de Ventas.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        dataGridView1.ClearSelection();
                    }
                }
                else
                {
                    MessageBox.Show("Por Favor Seleccione Item a Eliminar de la Lista.", "Sistema de Ventas.", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            else
            {
                MessageBox.Show("No Existe Ningun Elemento en la Lista", "Sistema de Ventas.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRegistrarVenta_Click_1(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                if (Convert.ToString(dataGridView1.CurrentRow.Cells[2].Value) != "")
                {
                    GuardarVenta();
                    try
                    {
                        for (int i = 0; i < dataGridView1.Rows.Count; i++)
                        {
                             Decimal SumaSubTotal = 0;
                            if (Convert.ToString(dataGridView1.Rows[i].Cells[2].Value) != "")
                            {
                                SumaSubTotal += Convert.ToDecimal(dataGridView1.Rows[i].Cells[4].Value);
                                GuardarDetalleVenta(
                                Convert.ToInt32(dataGridView1.Rows[i].Cells[5].Value),
                                Convert.ToInt32(dataGridView1.Rows[i].Cells[0].Value),
                                Convert.ToInt32(dataGridView1.Rows[i].Cells[1].Value),
                                 SumaSubTotal
                                );
                                //DevComponents.DotNetBar.MessageBoxEx.Show("Contiene Datos.");
                            }
                            else
                            {
                                //DevComponents.DotNetBar.MessageBoxEx.Show("Fila Vacia.");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                else
                {
                    MessageBox.Show("No Existe Ningún Elemento en la Lista.", "Sistema de Ventas.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("No Existe Ningún Elemento en la Lista.", "Sistema de Ventas.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void dataGridView1_Click_1(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                dataGridView1.Rows[dataGridView1.CurrentRow.Index].Selected = true;
            }
        }
        private void GuardarVenta()
        {
            decimal Total = 0;
            if (Convert.ToString(dataGridView1.CurrentRow.Cells[2].Value) != "")
            {
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    Total = Convert.ToDecimal(dataGridView1.Rows[i].Cells[4].Value);
                }
                Ventas.IdEmpleado = Program.IdEmpleadoLogueado;
                Ventas.FechaVenta = Convert.ToDateTime(dateTimePicker1.Value);
                Ventas.Total = Total;
                MessageBox.Show(Ventas.RegistrarVenta(), "Sistema de Ventas.", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void GuardarDetalleVenta(Int32 objIdProducto, Int32 objIdVenta, Int32 objCantidad,
             Decimal objSubTotal)
        {
            Detalle.IdProducto = objIdProducto;
            Detalle.IdVenta = objIdVenta;
            Detalle.Cantidad = objCantidad;
            Detalle.SubTotal = objSubTotal;
            Detalle.RegistrarDetalleVenta();
            Limpiar1();
            GenerarIdVenta();
        }

        private void Limpiar1()
        {
            dataGridView1.Rows.Clear();
            Program.IdEmpleadoLogueado = 0;
            txtIdProducto.Clear();
        }

        private void btn_rep_Click(object sender, EventArgs e)
        {
            //pnl_rep.Visible = true;
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

        private void btn_ven_Click(object sender, EventArgs e)
        {
            FrmRegistroVentas RV = new FrmRegistroVentas();
            RV.Visible = true;
            Visible = false;

        }

        //private void btn_rep_Click(object sender, EventArgs e)
        //{
        //    pnl_rep.Visible = true;
        //}


        private void btn_rep_Click_1(object sender, EventArgs e)
        {
            //pnl_rep.Visible = true;
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

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void bnt_min_Click_1(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btn_max_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            //btn_max.Visible = false;
            //btn_res.Visible = true;
        }

        private void btn_cerrar_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btn_res_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
            //btn_max.Visible = true;
            //btn_res.Visible = false;
        }


        //private void btnQuitar_Click(object sender, EventArgs e)
        //{
        //    DialogResult Resultado = new DialogResult();
        //    Resultado = DevComponents.DotNetBar.MessageBoxEx.Show("¿Está Seguro Que Desea Quitar Este Producto.?", "Sistema de Ventas.", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
        //       if(Resultado==DialogResult.OK){
        //            try{	        
        //                foreach (DataGridViewRow row in dataGridView1.Rows)
        //                {
        //                    Boolean Activo=Convert.ToBoolean(row.Cells["Eliminar"].Value);
        //                    if(Activo){
        //                        for (int i = 0; i < dataGridView1.RowCount; i++)
        //                        {
        //                            dataGridView1.Rows.RemoveAt(i);
        //                        }
        //                    }
        //                }
        //            }catch (Exception ex){
        //                 DevComponents.DotNetBar.MessageBoxEx.Show(ex.Message);
        //            }
        //       }
        //}

    }
}
