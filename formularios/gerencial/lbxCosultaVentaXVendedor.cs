using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using lebrun.clases.gerencial;


namespace lebrun.formularios.gerencial
{
    public partial class lbxCosultaVentaXVendedor : Form
    {
        ConsultaVentaXVendedor consultaVendor;

        public lbxCosultaVentaXVendedor()
        {
            InitializeComponent();
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox2.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox3.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void lbxCosultaVentaXVendedor_Load(object sender, EventArgs e)
        {
            consultaVendor = new ConsultaVentaXVendedor(); ;
            string s;

            s = System.Globalization.CultureInfo.CurrentCulture.NumberFormat.CurrencyDecimalSeparator;
            dataGridView4.Columns[2].DefaultCellStyle.Format = "El separador decimal es: '" + s + "'";

            s = System.Globalization.CultureInfo.CurrentCulture.NumberFormat.CurrencyGroupSeparator;
            label2.Text = "El separador de miles es: '" + s + "'";
        }


        public void crearColunas() 
        {
            dataGridView1.ColumnCount = 14;
            dataGridView1.Columns[0].Name = "Codigo";
            dataGridView1.Columns[1].Name = "Vendedor";
            dataGridView1.Columns[2].Name = "Vta Dia";
            dataGridView1.Columns[3].Name = "    %";
            dataGridView1.Columns[4].Name = "Vta Semana";
            dataGridView1.Columns[5].Name = "    %";
            dataGridView1.Columns[6].Name = "Vta Meses";
            dataGridView1.Columns[7].Name = "    %";
            dataGridView1.Columns[8].Name = "Vta Años";
            dataGridView1.Columns[9].Name = "    %";
            dataGridView1.Columns[10].Name = "Vta 3 Meses";
            dataGridView1.Columns[11].Name = "Vta 6 Meses";
            dataGridView1.Columns[12].Name = "Vta 9 Meses";
            dataGridView1.Columns[13].Name = "Vta 12 Meses";
            dataGridView2.ColumnCount = 14;
            dataGridView2.Columns[0].Name = "Codigo";
            dataGridView2.Columns[1].Name = "Vendedor";
            dataGridView2.Columns[2].Name = "Vta Dia";
            dataGridView2.Columns[3].Name = "    %";
            dataGridView2.Columns[4].Name = "Vta Semana";
            dataGridView2.Columns[5].Name = "    %";
            dataGridView2.Columns[6].Name = "Vta Meses";
            dataGridView2.Columns[7].Name = "    %";
            dataGridView2.Columns[8].Name = "Vta Años";
            dataGridView2.Columns[9].Name = "    %";
            dataGridView2.Columns[10].Name = "Vta 3 Meses";
            dataGridView2.Columns[11].Name = "Vta 6 Meses";
            dataGridView2.Columns[12].Name = "Vta 9 Meses";
            dataGridView2.Columns[13].Name = "Vta 12 Meses";
        }

        private void btnBuscar_Click(object sender, EventArgs e)

        {

            dataGridView1.Rows.Clear();
            dataGridView1.Refresh();
            dataGridView2.Rows.Clear();
            dataGridView2.Refresh();
            dataGridView4.Rows.Clear();
            dataGridView4.Refresh();
            consultaVendor.cargarDataGrid(dataGridView1, dataGridView2, dataGridView4);
            dataGridView1.Columns[1].Width = 135;
            dataGridView1.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight;
            dataGridView1.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight;
            dataGridView1.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight;
            dataGridView1.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight;
            dataGridView1.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight;
            dataGridView1.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight;
            dataGridView1.Columns[8].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight;
            dataGridView1.Columns[9].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight;
            dataGridView1.Columns[10].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight;
            dataGridView1.Columns[11].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight;
            dataGridView1.Columns[12].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight;
            dataGridView1.Columns[13].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight;

            dataGridView1.Columns[2].DefaultCellStyle.Format = "N2";
            dataGridView1.Columns[3].DefaultCellStyle.Format = "N2";
            dataGridView1.Columns[4].DefaultCellStyle.Format = "N2";
            dataGridView1.Columns[5].DefaultCellStyle.Format = "N2";
            dataGridView1.Columns[6].DefaultCellStyle.Format = "N2";
            dataGridView1.Columns[7].DefaultCellStyle.Format = "N2";
            dataGridView1.Columns[8].DefaultCellStyle.Format = "N2";
            dataGridView1.Columns[9].DefaultCellStyle.Format = "N2";
            dataGridView1.Columns[10].DefaultCellStyle.Format = "N2";
            dataGridView1.Columns[11].DefaultCellStyle.Format = "N2";
            dataGridView1.Columns[12].DefaultCellStyle.Format = "N2";
            dataGridView1.Columns[13].DefaultCellStyle.Format = "N2";
           
            dataGridView2.Columns[1].Width = 135;
            dataGridView2.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight;
            dataGridView2.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight;
            dataGridView2.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight;
            dataGridView2.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight;
            dataGridView2.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight;
            dataGridView2.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight;
            dataGridView2.Columns[8].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight;
            dataGridView2.Columns[9].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight;
            dataGridView2.Columns[10].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight;
            dataGridView2.Columns[11].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight;
            dataGridView2.Columns[12].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight;
            dataGridView2.Columns[13].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight;

            dataGridView2.Columns[2].DefaultCellStyle.Format = "n2";
            dataGridView2.Columns[3].DefaultCellStyle.Format = "n2";
            dataGridView2.Columns[4].DefaultCellStyle.Format = "n2";
            dataGridView2.Columns[5].DefaultCellStyle.Format = "n2";
            dataGridView2.Columns[6].DefaultCellStyle.Format = "n2";
            dataGridView2.Columns[7].DefaultCellStyle.Format = "n2";
            dataGridView2.Columns[8].DefaultCellStyle.Format = "n2";
            dataGridView2.Columns[9].DefaultCellStyle.Format = "n2";
            dataGridView2.Columns[10].DefaultCellStyle.Format = "n2";
            dataGridView2.Columns[11].DefaultCellStyle.Format = "n2";
            dataGridView2.Columns[12].DefaultCellStyle.Format = "n2";
            dataGridView2.Columns[13].DefaultCellStyle.Format = "n2";

            dataGridView4.Columns[1].Width = 135;
            dataGridView4.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight;
            dataGridView4.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight;
            dataGridView4.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight;
            dataGridView4.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight;
            dataGridView4.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight;
            dataGridView4.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight;
            dataGridView4.Columns[8].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight;
            dataGridView4.Columns[9].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight;
            dataGridView4.Columns[10].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight;
            dataGridView4.Columns[11].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight;
            dataGridView4.Columns[12].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight;
            dataGridView4.Columns[13].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight;

            dataGridView4.Columns[2].DefaultCellStyle.Format = "n2";
            dataGridView4.Columns[3].DefaultCellStyle.Format = "n2";
            dataGridView4.Columns[4].DefaultCellStyle.Format = "n2";
            dataGridView4.Columns[5].DefaultCellStyle.Format = "n2";
            dataGridView4.Columns[6].DefaultCellStyle.Format = "n2";
            dataGridView4.Columns[7].DefaultCellStyle.Format = "n2";
            dataGridView4.Columns[8].DefaultCellStyle.Format = "n2";
            dataGridView4.Columns[9].DefaultCellStyle.Format = "n2";
            dataGridView4.Columns[10].DefaultCellStyle.Format = "n2";
            dataGridView4.Columns[11].DefaultCellStyle.Format = "n2";
            dataGridView4.Columns[12].DefaultCellStyle.Format = "n2";
            dataGridView4.Columns[13].DefaultCellStyle.Format = "n2";

            SumaColunas();
            btnBuscar1.Visible = true;
            btnBuscar2.Visible = true;
            btnBuscar3.Visible = true;
            btnBuscar4.Visible = true;
            btnBuscar5.Visible = true;
            btnBuscar6.Visible = true;
            btnBuscar7.Visible = true;
            btnBuscar8.Visible = true;
            btnBuscar9.Visible = true;
            dateTimePicker1.Visible = true;
            dateTimePicker2.Visible = true;
            dateTimePicker5.Visible = true;
            dateTimePicker6.Visible = true;
            dateTimePicker8.Visible = true;
            dateTimePicker9.Visible = true;
            comboBox1.Visible = true;
            comboBox2.Visible = true;
            comboBox3.Visible = true;
            formato();
        }
        public void SumaColunas()
        {
            double total = 0;
            double total1 = 0;
            double total2 = 0;
            double total3 = 0;
            double total4 = 0;
            double total5 = 0;
            double total6 = 0;
            double total7 = 0;
            double total8 = 0;
            double total9 = 0;
            double total10 = 0;
            double total11 = 0;
            double total12 = 0;
            double total13 = 0;
            double total14 = 0;
            double total15 = 0;
            double total16 = 0;
            double total17 = 0;
            double total18 = 0;
            double total19 = 0;
            double total20 = 0;
            double total21 = 0;
            double total22 = 0;
            double total23 = 0;

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                total += Convert.ToDouble(row.Cells[2].Value);
                total1 += Convert.ToDouble(row.Cells[4].Value);
                total2 += Convert.ToDouble(row.Cells[6].Value);
                total3 += Convert.ToDouble(row.Cells[8].Value);
                total4 += Convert.ToDouble(row.Cells[10].Value);
                total5 += Convert.ToDouble(row.Cells[11].Value);
                total6 += Convert.ToDouble(row.Cells[12].Value);
                total7 += Convert.ToDouble(row.Cells[13].Value);
            }
            foreach (DataGridViewRow row in dataGridView4.Rows)
            {
                total8 += Convert.ToDouble(row.Cells[2].Value);
                total9 += Convert.ToDouble(row.Cells[4].Value);
                total10 += Convert.ToDouble(row.Cells[6].Value);
                total11 += Convert.ToDouble(row.Cells[8].Value);
                total12 += Convert.ToDouble(row.Cells[10].Value);
                total13+= Convert.ToDouble(row.Cells[11].Value);
                total14 += Convert.ToDouble(row.Cells[12].Value);
                total15 += Convert.ToDouble(row.Cells[13].Value);
            }
            foreach (DataGridViewRow row in dataGridView2.Rows)
            {
                total16 += Convert.ToDouble(row.Cells[2].Value);
                total17 += Convert.ToDouble(row.Cells[4].Value);
                total18 += Convert.ToDouble(row.Cells[6].Value);
                total19 += Convert.ToDouble(row.Cells[8].Value);
                total20 += Convert.ToDouble(row.Cells[10].Value);
                total21 += Convert.ToDouble(row.Cells[11].Value);
                total22 += Convert.ToDouble(row.Cells[12].Value);
                total23 += Convert.ToDouble(row.Cells[13].Value);
            }

            txtTotalDia.Text = total.ToString("n2");
            txtTotalSemana.Text = total1.ToString("n2");
            txtTotalMes.Text = total2.ToString("n2");
            txtAño.Text = total3.ToString("n2");
            txtT3M.Text = total4.ToString("n2");
            txtT6M.Text = total5.ToString("n2");
            txtT9M.Text = total6.ToString("n2");
            txtT12M.Text = total7.ToString("n2");

            txtTotalDiaF.Text = total8.ToString("n2");
            txtTotalSemanaF.Text = total9.ToString("n2");
            txtTotalMesF.Text = total10.ToString("n2");
            txtAñoF.Text = total11.ToString("n2");
            txtT3MF.Text = total12.ToString("n2");
            txtT6MF.Text = total13.ToString("n2");
            txtT9MF.Text = total14.ToString("n2");
            txtT12MF.Text = total15.ToString("n2");

            txtTotalDiaV.Text = total16.ToString("n2");
            txtTotalSemanaVV.Text = total17.ToString("n2");
            txtTotalMesV.Text = total18.ToString("n2");
            txtAñoV.Text = total19.ToString("n2");
            txtT3MV.Text = total20.ToString("n2");
            txtT6MV.Text = total21.ToString("n2");
            txtT9MV.Text = total22.ToString("n2");
            txtT12MV.Text = total23.ToString("n2");
            
            //General
            if (txtTotalDia.Text != "0,00")
            {
                txtDXC.Text = (Convert.ToDouble(((total - consultaVendor.PorcenGenDia) * 100.00) / total)).ToString("n2");
            }
            else 
            {
                txtDXC.Text = "0,00";
            }
            if (txtTotalSemana.Text != "0,00")
            {
                txtSXC.Text = (Convert.ToDouble(((total1 - consultaVendor.PorcenGenSem) * 100.00) / total1)).ToString("n2");
            }
            else
            {
                txtSXC.Text = "0,00";
            }
            if (txtTotalMes.Text != "0,00")
            {
                txtMXC.Text = (Convert.ToDouble(((total2 - consultaVendor.PorcenGenMes) * 100.00) / total2)).ToString("n2");
            }
            else
            {
                txtMXC.Text = "0,00";
            }
            if (txtAño.Text != "0,00")
            {
                txtAXC.Text = (Convert.ToDouble(((total3 - consultaVendor.PorcenGenAño) * 100.00) / total3)).ToString("n2");
            }
            else
            {
                txtAXC.Text = "0,00";
            }
            if (txtT3M.Text != "0,00")
            {
                txtM3XC.Text = (Convert.ToDouble(((total4 - consultaVendor.PorcenGen3Mes) * 100.00) / total4)).ToString("n2");
            }
            else
            {
                txtM3XC.Text = "0,00";
            }
            if (txtT6M.Text != "0,00")
            {
                txtM6XC.Text = (Convert.ToDouble(((total5 - consultaVendor.PorcenGen6Mes) * 100.00) / total5)).ToString("n2");
            }
            else
            {
                txtM6XC.Text = "0,00";
            }
            if (txtT9M.Text != "0,00")
            {
                txtM9XC.Text = (Convert.ToDouble(((total6 - consultaVendor.PorcenGen9Mes) * 100.00) / total6)).ToString("n2");
            }
            else
            {
                txtM9XC.Text = "0,00";
            }
            if (txtT12M.Text != "0,00")
            {
                txtM12XC.Text = (Convert.ToDouble(((total7 - consultaVendor.PorcenGen12Mes) * 100.00) / total7)).ToString("n2");
            }
            else
            {
                txtM12XC.Text = "0,00";
            }
            //tienda
            if (txtTotalDiaF.Text != "0,00")
            {
                txtDXCF.Text = (Convert.ToDouble(((total8 - consultaVendor.PorcenGenDiaT) * 100.00) / total8)).ToString("n2");
            }
            else 
            {
                txtDXCF.Text = "0,00";
            }
            if (txtTotalSemanaF.Text != "0,00")
            {
                txtSXCF.Text = (Convert.ToDouble(((total9 - consultaVendor.PorcenGenSemT) * 100.00) / total9)).ToString("n2");
            }
            else
            {
                txtSXCF.Text = "0,00";
            }
            if (txtTotalMesF.Text != "0,00")
            {
                txtMXCF.Text = (Convert.ToDouble(((total10 - consultaVendor.PorcenGenMesT) * 100.00) / total10)).ToString("n2");
            }
            else
            {
                txtMXCF.Text = "0,00";
            }
            if (txtAñoF.Text != "0,00")
            {
                txtAXCF.Text = (Convert.ToDouble(((total11 - consultaVendor.PorcenGenAñoT) * 100.00) / total11)).ToString("n2");
            }
            else
            {
                txtAXCF.Text = "0,00";
            }
            if (txtT3MF.Text != "0,00")
            {
                txtM3XCF.Text = (Convert.ToDouble(((total12 - consultaVendor.PorcenGen3MesT) * 100.00) / total12)).ToString("n2");
            }
            else
            {
                txtM3XCF.Text = "0,00";
            }
            if (txtT6MF.Text != "0,00")
            {
                txtM6XCF.Text = (Convert.ToDouble(((total13 - consultaVendor.PorcenGen6MesT) * 100.00) / total13)).ToString("n2");
            }
            else
            {
                txtM6XCF.Text = "0,00";
            }
            if (txtT9MF.Text != "0,00")
            {
                txtM9XCF.Text = (Convert.ToDouble(((total14 - consultaVendor.PorcenGen9MesT) * 100.00) / total14)).ToString("n2");
            }
            else
            {
                txtM9XCF.Text = "0,00";
            }
            if (txtT12MF.Text != "0,00")
            {
                txtM12XCF.Text = (Convert.ToDouble(((total15 - consultaVendor.PorcenGen12MesT) * 100.00) / total15)).ToString("n2");
            }
            else
            {
                txtM12XCF.Text = "0,00";
            }
            //Vendedor
            if (txtTotalDiaV.Text != "0,00")
            {
                txtDXCV.Text= (Convert.ToDouble(((total16 - consultaVendor.PorcenGenDiaV) * 100.00) / total16)).ToString("n2");
            }
            else 
            {
                txtDXCV.Text = "0,00";
            }
            if (txtTotalSemanaVV.Text != "0,00")
            {
                txtSXCV.Text = (Convert.ToDouble(((total17 - consultaVendor.PorcenGenSemV) * 100.00) / total17)).ToString("n2");
            }
            else
            {
                txtSXCV.Text = "0,00";
            }
            if (txtTotalMesV.Text != "0,00")
            {
                txtMXCV.Text = (Convert.ToDouble(((total18 - consultaVendor.PorcenGenMesV) * 100.00) / total18)).ToString("n2");
            }
            else
            {
                txtMXCV.Text = "0,00";
            }
            if (txtAñoV.Text != "0,00")
            {
                txtAXCV.Text = (Convert.ToDouble(((total19 - consultaVendor.PorcenGenAñoV) * 100.00) / total19)).ToString("n2");
            }
            else
            {
                txtAXCV.Text = "0,00";
            }
            if (txtT3MV.Text != "0,00")
            {
                txtM3XCV.Text = (Convert.ToDouble(((total20 - consultaVendor.PorcenGen3MesV) * 100.00) / total20)).ToString("n2");
            }
            else
            {
                txtM3XCV.Text = "0,00";
            }
            if (txtT6MV.Text != "0,00")
            {
                txtM6XCV.Text = (Convert.ToDouble(((total21 - consultaVendor.PorcenGen6MesV) * 100.00) / total21)).ToString("n2");
            }
            else
            {
                txtM6XCV.Text = "0,00";
            }
            if (txtT9MV.Text != "0,00")
            {
                txtM9XCV.Text = (Convert.ToDouble(((total22 - consultaVendor.PorcenGen9MesV) * 100.00) / total22)).ToString("n2");
            }
            else
            {
                txtM9XCV.Text = "0,00";
            }
            if (txtT12MV.Text != "0,00")
            {
                txtM12XCV.Text = (Convert.ToDouble(((total23 - consultaVendor.PorcenGen12MesV) * 100.00) / total23)).ToString("n2");
            }
            else
            {
                txtM12XCV.Text = "0,00";
            }
            
            formato();
        }


        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)

        {
            dataGridView1.Rows.Clear();
            dataGridView1.Refresh();
            dataGridView2.Rows.Clear();
            dataGridView2.Refresh();
            dataGridView4.Rows.Clear();
            dataGridView4.Refresh();
            consultaVendor.cargarDataGrid(dataGridView1, dataGridView2, dataGridView4);
            dataGridView1.Columns[1].Width = 135;
            dataGridView1.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight;
            dataGridView1.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight;
            dataGridView1.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight;
            dataGridView1.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight;
            dataGridView1.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight;
            dataGridView1.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight;
            dataGridView1.Columns[8].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight;
            dataGridView1.Columns[9].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight;
            dataGridView1.Columns[10].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight;
            dataGridView1.Columns[11].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight;
            dataGridView1.Columns[12].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight;
            dataGridView1.Columns[13].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight;

            dataGridView1.Columns[2].DefaultCellStyle.Format = "n2";
            dataGridView1.Columns[3].DefaultCellStyle.Format = "n2";
            dataGridView1.Columns[4].DefaultCellStyle.Format = "n2";
            dataGridView1.Columns[5].DefaultCellStyle.Format = "n2";
            dataGridView1.Columns[6].DefaultCellStyle.Format = "n2";
            dataGridView1.Columns[7].DefaultCellStyle.Format = "n2";
            dataGridView1.Columns[8].DefaultCellStyle.Format = "n2";
            dataGridView1.Columns[9].DefaultCellStyle.Format = "n2";
            dataGridView1.Columns[10].DefaultCellStyle.Format = "n2";
            dataGridView1.Columns[11].DefaultCellStyle.Format = "n2";
            dataGridView1.Columns[12].DefaultCellStyle.Format = "n2";
            dataGridView1.Columns[13].DefaultCellStyle.Format = "n2";

            dataGridView2.Columns[1].Width = 135;
            dataGridView2.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight;
            dataGridView2.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight;
            dataGridView2.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight;
            dataGridView2.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight;
            dataGridView2.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight;
            dataGridView2.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight;
            dataGridView2.Columns[8].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight;
            dataGridView2.Columns[9].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight;
            dataGridView2.Columns[10].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight;
            dataGridView2.Columns[11].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight;
            dataGridView2.Columns[12].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight;
            dataGridView2.Columns[13].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight;

            dataGridView2.Columns[2].DefaultCellStyle.Format = "n2";
            dataGridView2.Columns[3].DefaultCellStyle.Format = "n2";
            dataGridView2.Columns[4].DefaultCellStyle.Format = "n2";
            dataGridView2.Columns[5].DefaultCellStyle.Format = "n2";
            dataGridView2.Columns[6].DefaultCellStyle.Format = "n2";
            dataGridView2.Columns[7].DefaultCellStyle.Format = "n2";
            dataGridView2.Columns[8].DefaultCellStyle.Format = "n2";
            dataGridView2.Columns[9].DefaultCellStyle.Format = "n2";
            dataGridView2.Columns[10].DefaultCellStyle.Format = "n2";
            dataGridView2.Columns[11].DefaultCellStyle.Format = "n2";
            dataGridView2.Columns[12].DefaultCellStyle.Format = "n2";
            dataGridView2.Columns[13].DefaultCellStyle.Format = "n2";

            dataGridView4.Columns[1].Width = 135;
            dataGridView4.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight;
            dataGridView4.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight;
            dataGridView4.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight;
            dataGridView4.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight;
            dataGridView4.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight;
            dataGridView4.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight;
            dataGridView4.Columns[8].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight;
            dataGridView4.Columns[9].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight;
            dataGridView4.Columns[10].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight;
            dataGridView4.Columns[11].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight;
            dataGridView4.Columns[12].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight;
            dataGridView4.Columns[13].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight;

            dataGridView4.Columns[2].DefaultCellStyle.Format = "n2";
            dataGridView4.Columns[3].DefaultCellStyle.Format = "n2";
            dataGridView4.Columns[4].DefaultCellStyle.Format = "n2";
            dataGridView4.Columns[5].DefaultCellStyle.Format = "n2";
            dataGridView4.Columns[6].DefaultCellStyle.Format = "n2";
            dataGridView4.Columns[7].DefaultCellStyle.Format = "n2";
            dataGridView4.Columns[8].DefaultCellStyle.Format = "n2";
            dataGridView4.Columns[9].DefaultCellStyle.Format = "n2";
            dataGridView4.Columns[10].DefaultCellStyle.Format = "n2";
            dataGridView4.Columns[11].DefaultCellStyle.Format = "n2";
            dataGridView4.Columns[12].DefaultCellStyle.Format = "n2";
            dataGridView4.Columns[13].DefaultCellStyle.Format = "n2";

            SumaColunas();
            btnBuscar1.Visible = true;
            btnBuscar2.Visible = true;
            btnBuscar3.Visible = true;
            btnBuscar4.Visible = true;
            btnBuscar5.Visible = true;
            btnBuscar6.Visible = true;
            btnBuscar7.Visible = true;
            btnBuscar8.Visible = true;
            btnBuscar9.Visible = true;
            dateTimePicker1.Visible = true;
            dateTimePicker2.Visible = true;
            dateTimePicker5.Visible = true;
            dateTimePicker6.Visible = true;
            dateTimePicker8.Visible = true;
            dateTimePicker9.Visible = true;
            comboBox1.Visible = true;
            comboBox2.Visible = true;
            comboBox3.Visible = true;
            formato();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            dataGridView1.Refresh();
            dataGridView2.Rows.Clear();
            dataGridView2.Refresh();
            dataGridView4.Rows.Clear();
            dataGridView4.Refresh();
            consultaVendor.cargarDataGrid(dataGridView1, dataGridView2, dataGridView4);
            dataGridView1.Columns[1].Width = 135;
            dataGridView1.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight;
            dataGridView1.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight;
            dataGridView1.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight;
            dataGridView1.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight;
            dataGridView1.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight;
            dataGridView1.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight;
            dataGridView1.Columns[8].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight;
            dataGridView1.Columns[9].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight;
            dataGridView1.Columns[10].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight;
            dataGridView1.Columns[11].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight;
            dataGridView1.Columns[12].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight;
            dataGridView1.Columns[13].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight;

            dataGridView2.Columns[1].Width = 135;
            dataGridView2.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight;
            dataGridView2.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight;
            dataGridView2.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight;
            dataGridView2.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight;
            dataGridView2.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight;
            dataGridView2.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight;
            dataGridView2.Columns[8].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight;
            dataGridView2.Columns[9].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight;
            dataGridView2.Columns[10].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight;
            dataGridView2.Columns[11].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight;
            dataGridView2.Columns[12].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight;
            dataGridView2.Columns[13].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight;

            dataGridView4.Columns[1].Width = 135;
            dataGridView4.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight;
            dataGridView4.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight;
            dataGridView4.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight;
            dataGridView4.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight;
            dataGridView4.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight;
            dataGridView4.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight;
            dataGridView4.Columns[8].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight;
            dataGridView4.Columns[9].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight;
            dataGridView4.Columns[10].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight;
            dataGridView4.Columns[11].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight;
            dataGridView4.Columns[12].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight;
            dataGridView4.Columns[13].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight;

            SumaColunas();
            btnBuscar1.Visible = true;
            btnBuscar2.Visible = true;
            btnBuscar3.Visible = true;
            btnBuscar4.Visible = true;
            btnBuscar5.Visible = true;
            btnBuscar6.Visible = true;
            btnBuscar7.Visible = true;
            btnBuscar8.Visible = true;
            btnBuscar9.Visible = true;
            dateTimePicker1.Visible = true;
            dateTimePicker2.Visible = true;
            dateTimePicker5.Visible = true;
            dateTimePicker6.Visible = true;
            dateTimePicker8.Visible = true;
            dateTimePicker9.Visible = true;
            comboBox1.Visible = true;
            comboBox2.Visible = true;
            comboBox3.Visible = true;
            formato();
        }

        private void formato() 
        {
            dataGridView1.Columns[2].DefaultCellStyle.Format = "n2";
            dataGridView1.Columns[3].DefaultCellStyle.Format = "n2";
            dataGridView1.Columns[4].DefaultCellStyle.Format = "n2";
            dataGridView1.Columns[5].DefaultCellStyle.Format = "n2";
            dataGridView1.Columns[6].DefaultCellStyle.Format = "n2";
            dataGridView1.Columns[7].DefaultCellStyle.Format = "n2";
            dataGridView1.Columns[8].DefaultCellStyle.Format = "n2";
            dataGridView1.Columns[9].DefaultCellStyle.Format = "n2";
            dataGridView1.Columns[10].DefaultCellStyle.Format = "n2";
            dataGridView1.Columns[11].DefaultCellStyle.Format = "n2";
            dataGridView1.Columns[12].DefaultCellStyle.Format = "n2";
            dataGridView1.Columns[13].DefaultCellStyle.Format = "n2";

            dataGridView2.Columns[2].DefaultCellStyle.Format = "n2";
            dataGridView2.Columns[3].DefaultCellStyle.Format = "n2";
            dataGridView2.Columns[4].DefaultCellStyle.Format = "n2";
            dataGridView2.Columns[5].DefaultCellStyle.Format = "n2";
            dataGridView2.Columns[6].DefaultCellStyle.Format = "n2";
            dataGridView2.Columns[7].DefaultCellStyle.Format = "n2";
            dataGridView2.Columns[8].DefaultCellStyle.Format = "n2";
            dataGridView2.Columns[9].DefaultCellStyle.Format = "n2";
            dataGridView2.Columns[10].DefaultCellStyle.Format = "n2";
            dataGridView2.Columns[11].DefaultCellStyle.Format = "n2";
            dataGridView2.Columns[12].DefaultCellStyle.Format = "n2";
            dataGridView2.Columns[13].DefaultCellStyle.Format = "n2";

           
            dataGridView4.Columns[3].DefaultCellStyle.Format = "n2";
            dataGridView4.Columns[4].DefaultCellStyle.Format = "n2";
            dataGridView4.Columns[5].DefaultCellStyle.Format = "n2";
            dataGridView4.Columns[6].DefaultCellStyle.Format = "n2";
            dataGridView4.Columns[7].DefaultCellStyle.Format = "n2";
            dataGridView4.Columns[8].DefaultCellStyle.Format = "n2";
            dataGridView4.Columns[9].DefaultCellStyle.Format = "n2";
            dataGridView4.Columns[10].DefaultCellStyle.Format = "n2";
            dataGridView4.Columns[11].DefaultCellStyle.Format = "n2";
            dataGridView4.Columns[12].DefaultCellStyle.Format = "n2";
            dataGridView4.Columns[13].DefaultCellStyle.Format = "n2";
        
        
        }
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void consultaPorDia() 
        {
        string fechaSelec;
        fechaSelec=dateTimePicker1.Text;
        consultaVendor.cargarDataGridDia(dataGridView1, dataGridView2, dataGridView4,fechaSelec);
        }
        public void consultaPorAño() 
        {
            string fechaSelec2;
            fechaSelec2 = comboBox1.Text+"-01-01";
            consultaVendor.cargarDataGridAño(dataGridView1, dataGridView2, dataGridView4, fechaSelec2);
        }
        public void consultaPorMes() 
        {
            string fechaSelec1;
            fechaSelec1 = dateTimePicker2.Value.ToString("yyyy-MM-dd");
            consultaVendor.cargarDataGridMes(dataGridView1, dataGridView2, dataGridView4, fechaSelec1);
        }
        private void btnBuscar1_Click(object sender, EventArgs e)
        {
            consultaPorDia();
            colorPorColunaDia();
            formato();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            dateTimePicker6.Value = dateTimePicker1.Value;
            dateTimePicker9.Value = dateTimePicker1.Value;
        }

        private void dateTimePicker6_ValueChanged(object sender, EventArgs e)
        {
            dateTimePicker1.Value = dateTimePicker6.Value;
            dateTimePicker9.Value = dateTimePicker6.Value;
        }

        private void dateTimePicker9_ValueChanged(object sender, EventArgs e)
        {
            dateTimePicker1.Value = dateTimePicker9.Value;
            dateTimePicker6.Value = dateTimePicker9.Value;
        }

        private void dateTimePicker8_ValueChanged(object sender, EventArgs e)
        {
            dateTimePicker2.Value = dateTimePicker8.Value;
            dateTimePicker5.Value = dateTimePicker8.Value;
        }

        private void dateTimePicker5_ValueChanged(object sender, EventArgs e)
        {
            dateTimePicker8.Value = dateTimePicker5.Value;
            dateTimePicker2.Value = dateTimePicker5.Value;
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            dateTimePicker8.Value = dateTimePicker2.Value;
            dateTimePicker5.Value = dateTimePicker2.Value;
        }

        private void btnBuscar4_Click(object sender, EventArgs e)
        {
            consultaPorDia();
            colorPorColunaMes();
            formato();
        }

        private void btnBuscar7_Click(object sender, EventArgs e)
        {
            consultaPorDia();
            colorPorColunaDia();
            formato();
        }

        private void btnBuscar8_Click(object sender, EventArgs e)
        {
            consultaPorMes();
            colorPorColunaMes();
            formato();
        }

        private void btnBuscar2_Click(object sender, EventArgs e)
        {
            consultaPorMes();
            colorPorColunaMes();
            formato();
        }

        private void btnBuscar5_Click(object sender, EventArgs e)
        {
            consultaPorMes();
            colorPorColunaMes();
            formato();
        }

        private void btnBuscar6_Click(object sender, EventArgs e)
        {
            consultaPorAño();
            colorPorColunaAño();
            formato();
            dataGridView4.Sort(dataGridView4.Columns[0], ListSortDirection.Descending);
        }

        private void btnBuscar9_Click(object sender, EventArgs e)
        {
            consultaPorAño();
            colorPorColunaAño();
            formato();
        }
        private void colorPorColunaAño()
        {
            for (int i = 0; i < dataGridView4.Rows.Count; i++)
            {
                dataGridView4.Rows[i].Cells[9].Style.BackColor = Color.Yellow;
                dataGridView4.Rows[i].Cells[8].Style.BackColor = Color.Yellow;
            }
            for (int i = 0; i < dataGridView2.Rows.Count; i++)
            {
                dataGridView2.Rows[i].Cells[9].Style.BackColor = Color.Yellow;
                dataGridView2.Rows[i].Cells[8].Style.BackColor = Color.Yellow;
            }
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                dataGridView1.Rows[i].Cells[9].Style.BackColor = Color.Yellow;
                dataGridView1.Rows[i].Cells[8].Style.BackColor = Color.Yellow;
            }
        }
        private void colorPorColunaMes()
        {
            for (int i = 0; i < dataGridView4.Rows.Count; i++)
            {
                dataGridView4.Rows[i].Cells[7].Style.BackColor = Color.Yellow;
                dataGridView4.Rows[i].Cells[6].Style.BackColor = Color.Yellow;
            }
            for (int i = 0; i < dataGridView2.Rows.Count; i++)
            {
                dataGridView2.Rows[i].Cells[7].Style.BackColor = Color.Yellow;
                dataGridView2.Rows[i].Cells[6].Style.BackColor = Color.Yellow;
            }
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                dataGridView1.Rows[i].Cells[7].Style.BackColor = Color.Yellow;
                dataGridView1.Rows[i].Cells[6].Style.BackColor = Color.Yellow;
            }
        }
        private void colorPorColunaDia()
        {
            for (int i = 0; i < dataGridView4.Rows.Count; i++)
            {
                dataGridView4.Rows[i].Cells[3].Style.BackColor = Color.Yellow;
                dataGridView4.Rows[i].Cells[2].Style.BackColor = Color.Yellow;
            }
            for (int i = 0; i < dataGridView2.Rows.Count; i++)
            {
                dataGridView2.Rows[i].Cells[3].Style.BackColor = Color.Yellow;
                dataGridView2.Rows[i].Cells[2].Style.BackColor = Color.Yellow;
            }
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                dataGridView1.Rows[i].Cells[3].Style.BackColor = Color.Yellow;
                dataGridView1.Rows[i].Cells[2].Style.BackColor = Color.Yellow;
            }
        }

        private void btnBuscar3_Click(object sender, EventArgs e)
        {
            consultaPorAño();
            colorPorColunaAño();
            formato();
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox1.Text = comboBox3.Text;
            comboBox2.Text = comboBox3.Text;
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox2.Text = comboBox2.Text;
            comboBox3.Text = comboBox2.Text;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox3.Text = comboBox1.Text;
            comboBox2.Text = comboBox1.Text;
        }

        private void lbxCosultaVentaXVendedor_Shown(object sender, EventArgs e)
        {
            crearColunas();
        }


    }
}
