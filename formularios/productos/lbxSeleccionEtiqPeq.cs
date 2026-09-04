using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using lebrun.clasesData;
using lebrun.clases.complementos;
using lebrun.clases.producto;
using System.IO;
using lebrun.formularios.facturacion;
using lebrun;
using MySql.Data.MySqlClient;

namespace lebrun.formularios.productos
{
    public partial class lbxSeleccionEtiqPeq : Form

    {
        Principal SistemaUs;// llamado al la informacion de la pataklla principal
        ConexionBD databaseConection;// llamada a la ventana conexion a la base de datos
        

        public lbxSeleccionEtiqPeq()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
                        //consulta verficacion de usuario
            SistemaUs = (Principal)this.MdiParent;
            string updatesql;
            DataTable dt;
            DataTable dt1;
            DataTable dt2;
            string consultasql = "SELECT status,ip_estacion FROM admserietiqpeq WHERE status='1'AND usuario='" + SistemaUs.usuarioActual.Id + "';";
            string consultasqlm = "SELECT status FROM admserietiqmed WHERE status='1'AND usuario='" + SistemaUs.usuarioActual.Id + "';";
            string consultasqlg = "SELECT status FROM admserietiqgrd WHERE status='1'AND usuario='" + SistemaUs.usuarioActual.Id + "';";
            dt = databaseConection.fDataTable(consultasql);
            dt1 = databaseConection.fDataTable(consultasqlm);
            dt2 = databaseConection.fDataTable(consultasqlg);
           
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["status"].ToString().Equals("1"))
                {
                    Close();
                    frmValidacionUserTimer timer = new frmValidacionUserTimer();
                    timer.Show();
                }
            }
            else if (dt1.Rows.Count > 0)
            {
                if (dt1.Rows[0]["status"].ToString().Equals("1"))
                {

                    Close();
                    frmValidacionUserTimer timer = new frmValidacionUserTimer();
                    timer.Show();
                }
            }
            else if (dt2.Rows.Count > 0)
            {
                if (dt2.Rows[0]["status"].ToString().Equals("1"))
                {
                    Close();
                    frmValidacionUserTimer timer = new frmValidacionUserTimer();
                    timer.Show();
                }
            }
            else
            {
                updatesql = "UPDATE admserietiqpeq SET usuario='" + SistemaUs.usuarioActual.Id + "',usuario_sistema='" + SistemaUs.usuarioActual.Id + "',status='1' WHERE status='0' AND ip_estacion='" + SistemaUs.usuarioActual.IpPc + "';";
                databaseConection.fDataTable(updatesql);
                Form1 form = new Form1();
                form.opcion = "0";
                form.ip=SistemaUs.usuarioActual.IpPc;
                form.id = SistemaUs.usuarioActual.Id;
                form.Show();
                this.Close();
               
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //consulta verficacion de usuario
            frmEtiqPeqDesg desg = new frmEtiqPeqDesg();
            SistemaUs = (Principal)this.MdiParent;
            string updatesql;
            DataTable dt;
            DataTable dt1;
            DataTable dt2;
            string consultasql = "SELECT status,ip_estacion FROM admserietiqpeq WHERE status='1'AND usuario='" + SistemaUs.usuarioActual.Id + "';";
            string consultasqlm = "SELECT status FROM admserietiqmed WHERE status='1'AND usuario='" + SistemaUs.usuarioActual.Id + "';";
            string consultasqlg = "SELECT status FROM admserietiqgrd WHERE status='1'AND usuario='" + SistemaUs.usuarioActual.Id + "';";
            dt = databaseConection.fDataTable(consultasql);
            dt1 = databaseConection.fDataTable(consultasqlm);
            dt2 = databaseConection.fDataTable(consultasqlg);

            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["status"].ToString().Equals("1"))
                {
                    desg.opcion = "1";
                    Close();
                    frmValidacionUserTimer timer = new frmValidacionUserTimer();
                    timer.Show();
                }
            }
            else if (dt1.Rows.Count > 0)
            {
                if (dt1.Rows[0]["status"].ToString().Equals("1"))
                {
                    Close();
                    frmValidacionUserTimer timer = new frmValidacionUserTimer();
                    timer.Show();
                }
            }
            else if (dt2.Rows.Count > 0)
            {
                if (dt2.Rows[0]["status"].ToString().Equals("1"))
                {
                    Close();
                    frmValidacionUserTimer timer = new frmValidacionUserTimer();
                    timer.Show();
                }
            }
            else
            {
                updatesql = "UPDATE admserietiqpeq SET usuario='" + SistemaUs.usuarioActual.Id + "',usuario_sistema='" + SistemaUs.usuarioActual.Id + "',status='1' WHERE status='0' AND ip_estacion='" + SistemaUs.usuarioActual.IpPc + "';";
                databaseConection.fDataTable(updatesql);
                desg.opcion = "0";
                desg.ip=SistemaUs.usuarioActual.IpPc;
                desg.id = SistemaUs.usuarioActual.Id;
                desg.Show();
                this.Close();
            }
        }

        private void lbxSeleccionEtiqPeq_Load(object sender, EventArgs e)
        {
            databaseConection = new ConexionBD();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            frmEtiqPeqCodg Codg = new frmEtiqPeqCodg();
            SistemaUs = (Principal)this.MdiParent;
            string updatesql;
            DataTable dt;
            DataTable dt1;
            DataTable dt2;
            string consultasql = "SELECT status,ip_estacion FROM admserietiqpeq WHERE status='1'AND usuario='" + SistemaUs.usuarioActual.Id + "';";
            string consultasqlm = "SELECT status FROM admserietiqmed WHERE status='1'AND usuario='" + SistemaUs.usuarioActual.Id + "';";
            string consultasqlg = "SELECT status FROM admserietiqgrd WHERE status='1'AND usuario='" + SistemaUs.usuarioActual.Id + "';";
            dt = databaseConection.fDataTable(consultasql);
            dt1 = databaseConection.fDataTable(consultasqlm);
            dt2 = databaseConection.fDataTable(consultasqlg);

            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["status"].ToString().Equals("1"))
                {
                    Codg.opcion = "1";
                    Close();
                    frmValidacionUserTimer timer = new frmValidacionUserTimer();
                    timer.Show();
                }
            }
            else if (dt1.Rows.Count > 0)
            {
                if (dt1.Rows[0]["status"].ToString().Equals("1"))
                {
                    Close();
                    frmValidacionUserTimer timer = new frmValidacionUserTimer();
                    timer.Show();
                }
            }
            else if (dt2.Rows.Count > 0)
            {
                if (dt2.Rows[0]["status"].ToString().Equals("1"))
                {
                    Close();
                    frmValidacionUserTimer timer = new frmValidacionUserTimer();
                    timer.Show();
                }
            }
            else
            {
                updatesql = "UPDATE admserietiqpeq SET usuario='" + SistemaUs.usuarioActual.Id + "',usuario_sistema='" + SistemaUs.usuarioActual.Id + "',status='1' WHERE status='0' AND ip_estacion='" + SistemaUs.usuarioActual.IpPc + "';";
                databaseConection.fDataTable(updatesql);
                Codg.opcion = "0";
                Codg.id = SistemaUs.usuarioActual.Id;
                Codg.ip = SistemaUs.usuarioActual.IpPc;
                Codg.Show();
                this.Close();
            }

        }
    }
}
