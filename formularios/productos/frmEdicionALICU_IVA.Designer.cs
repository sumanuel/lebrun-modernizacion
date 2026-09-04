namespace lebrun.formularios.productos
{
    partial class frmEdicionALICU_IVA
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.BtnRegistra = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnActulizar = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtTipoIVA = new System.Windows.Forms.TextBox();
            this.txtVenta = new System.Windows.Forms.TextBox();
            this.txtCompra = new System.Windows.Forms.TextBox();
            this.txtZonaLibre = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.ttMensaje = new System.Windows.Forms.ToolTip(this.components);
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // BtnRegistra
            // 
            this.BtnRegistra.Image = global::lebrun.Properties.Resources.Add_24x24;
            this.BtnRegistra.Location = new System.Drawing.Point(26, 19);
            this.BtnRegistra.Name = "BtnRegistra";
            this.BtnRegistra.Size = new System.Drawing.Size(64, 56);
            this.BtnRegistra.TabIndex = 7;
            this.BtnRegistra.UseVisualStyleBackColor = true;
            this.BtnRegistra.Click += new System.EventHandler(this.BtnRegistra_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.ForeColor = System.Drawing.SystemColors.WindowText;
            this.btnCancelar.Image = global::lebrun.Properties.Resources.Cancel_24x24;
            this.btnCancelar.Location = new System.Drawing.Point(105, 19);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(64, 56);
            this.btnCancelar.TabIndex = 6;
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.button2_Click);
            // 
            // btnActulizar
            // 
            this.btnActulizar.Image = global::lebrun.Properties.Resources.Refresh_24x24;
            this.btnActulizar.Location = new System.Drawing.Point(26, 19);
            this.btnActulizar.Name = "btnActulizar";
            this.btnActulizar.Size = new System.Drawing.Size(64, 56);
            this.btnActulizar.TabIndex = 5;
            this.btnActulizar.UseVisualStyleBackColor = true;
            this.btnActulizar.Click += new System.EventHandler(this.btnActulizar_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Firebrick;
            this.label1.Location = new System.Drawing.Point(18, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Tipo De IVA";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 75);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "% De Venta";
            // 
            // txtTipoIVA
            // 
            this.txtTipoIVA.Location = new System.Drawing.Point(89, 29);
            this.txtTipoIVA.MaxLength = 2;
            this.txtTipoIVA.Name = "txtTipoIVA";
            this.txtTipoIVA.Size = new System.Drawing.Size(71, 20);
            this.txtTipoIVA.TabIndex = 10;
            this.txtTipoIVA.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtTipoIVA.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtTipoIVA_KeyPress);
            // 
            // txtVenta
            // 
            this.txtVenta.Location = new System.Drawing.Point(89, 72);
            this.txtVenta.Name = "txtVenta";
            this.txtVenta.Size = new System.Drawing.Size(71, 20);
            this.txtVenta.TabIndex = 11;
            this.txtVenta.Text = "0.00";
            this.txtVenta.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtVenta.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtVenta_KeyPress);
            // 
            // txtCompra
            // 
            this.txtCompra.Location = new System.Drawing.Point(89, 98);
            this.txtCompra.Name = "txtCompra";
            this.txtCompra.Size = new System.Drawing.Size(71, 20);
            this.txtCompra.TabIndex = 12;
            this.txtCompra.Text = "0.00";
            this.txtCompra.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtCompra.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCompra_KeyPress);
            // 
            // txtZonaLibre
            // 
            this.txtZonaLibre.Location = new System.Drawing.Point(89, 124);
            this.txtZonaLibre.Name = "txtZonaLibre";
            this.txtZonaLibre.Size = new System.Drawing.Size(71, 20);
            this.txtZonaLibre.TabIndex = 13;
            this.txtZonaLibre.Text = "0.00";
            this.txtZonaLibre.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtZonaLibre.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtZonaLibre_KeyPress);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtTipoIVA);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtZonaLibre);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtVenta);
            this.groupBox1.Controls.Add(this.txtCompra);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(189, 157);
            this.groupBox1.TabIndex = 14;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Datos IVA";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(18, 127);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(69, 13);
            this.label4.TabIndex = 15;
            this.label4.Text = "% Zona Libre";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(18, 101);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(54, 13);
            this.label3.TabIndex = 14;
            this.label3.Text = "% Compra";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnActulizar);
            this.groupBox2.Controls.Add(this.BtnRegistra);
            this.groupBox2.Controls.Add(this.btnCancelar);
            this.groupBox2.Location = new System.Drawing.Point(12, 175);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(189, 84);
            this.groupBox2.TabIndex = 15;
            this.groupBox2.TabStop = false;
            // 
            // frmEdicionALICU_IVA
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(216, 271);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.Name = "frmEdicionALICU_IVA";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Editor De Iva";
            this.Load += new System.EventHandler(this.frmEdicionALICU_IVA_Load);
            this.Shown += new System.EventHandler(this.frmEdicionALICU_IVA_Shown);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.frmEdicionALICU_IVA_KeyPress);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmEdicionALICU_IVA_FormClosing);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button BtnRegistra;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnActulizar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox2;
        public System.Windows.Forms.TextBox txtTipoIVA;
        public System.Windows.Forms.TextBox txtVenta;
        public System.Windows.Forms.TextBox txtCompra;
        public System.Windows.Forms.TextBox txtZonaLibre;
        private System.Windows.Forms.ToolTip ttMensaje;
    }
}