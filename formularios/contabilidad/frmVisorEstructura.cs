using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.IO;
using Microsoft.Office.Interop.Excel;

namespace lebrun.formularios.contabilidad
{
    public partial class frmVisorEstructura : Form
    {
        public frmVisorEstructura()
        {
            InitializeComponent();
        }

        private void frmVisorEstructura_Load(object sender, EventArgs e)
        {
            webBrowser1.Navigate(System.Windows.Forms.Application.StartupPath + "\\estructuraCostos.html");
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {   
            webBrowser1.ShowPrintPreviewDialog();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            SaveFileDialog save = new SaveFileDialog();
            save.Filter = "Libro de Microsoft Office Excel |*.xls";
            save.Title = "Guardar Como";
            save.ShowDialog();
            
            if (save.FileName != "") {
                Microsoft.Office.Interop.Excel.Application excelApp = new Microsoft.Office.Interop.Excel.Application();
                Microsoft.Office.Interop.Excel.Workbook excelWorkBook = excelApp.Workbooks.Add(System.Reflection.Missing.Value);
                excelApp.Visible = false;
                Microsoft.Office.Interop.Excel.Workbook excelWorkBook2 = excelApp.Workbooks.Open(System.Windows.Forms.Application.StartupPath + "\\estructuraCostos.html", System.Reflection.Missing.Value,
                    System.Reflection.Missing.Value, System.Reflection.Missing.Value, System.Reflection.Missing.Value, System.Reflection.Missing.Value, System.Reflection.Missing.Value, System.Reflection.Missing.Value,
                    System.Reflection.Missing.Value, System.Reflection.Missing.Value, System.Reflection.Missing.Value, System.Reflection.Missing.Value, System.Reflection.Missing.Value, System.Reflection.Missing.Value,
                    System.Reflection.Missing.Value);
                excelWorkBook2.SaveAs(save.FileName, Microsoft.Office.Interop.Excel.XlFileFormat.xlXMLSpreadsheet, System.Reflection.Missing.Value, System.Reflection.Missing.Value, System.Reflection.Missing.Value, System.Reflection.Missing.Value,
                    Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange, Microsoft.Office.Interop.Excel.XlSaveConflictResolution.xlUserResolution, System.Reflection.Missing.Value, System.Reflection.Missing.Value, System.Reflection.Missing.Value, System.Reflection.Missing.Value);
                excelApp.Quit();
            }
        }
      
    }
}
