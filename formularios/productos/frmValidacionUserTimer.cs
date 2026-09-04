using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using lebrun.clasesData;
using MySql.Data.MySqlClient;
namespace lebrun.formularios.productos
{
    public partial class frmValidacionUserTimer : Form
    
    {
       

        public frmValidacionUserTimer()
        {
            InitializeComponent();
            timer1.Start();
            timer2.Start();
        }
        int i = 10;
        private void timer1_Tick(object sender, EventArgs e)
        {
           
            do
            {
                i--;
                label1.Text = (Convert.ToUInt32(i)).ToString();

            }
            while (i == 1);
            if (i == 0)
            {
                Close();
             
            }
        }
        private int num = 0;
        private void timer2_Tick(object sender, EventArgs e)
        {
            num = num + 1;
            if (num % 2 == 0)
            {
                pictureBox1.Visible = true;
                pictureBox2.Visible = false;
            }
            else
            {
                pictureBox1.Visible = false;
                pictureBox2.Visible = true;
            }
        }

        private void frmValidacionUserTimer_Load(object sender, EventArgs e)
        {
        
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

      

    }
}
