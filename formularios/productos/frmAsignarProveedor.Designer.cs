namespace lebrun.formularios.productos
{
    partial class frmAsignarProveedor
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
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.txtDesProv = new System.Windows.Forms.TextBox();
            this.cmdMoneda = new System.Windows.Forms.ComboBox();
            this.cmdPdO = new System.Windows.Forms.ComboBox();
            this.label27 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.button5 = new System.Windows.Forms.Button();
            this.label25 = new System.Windows.Forms.Label();
            this.txtCodProv = new System.Windows.Forms.TextBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtDesProd = new System.Windows.Forms.TextBox();
            this.txtCodProd = new System.Windows.Forms.TextBox();
            this.btnLimpiar = new System.Windows.Forms.Button();
            this.BtnRegistra = new System.Windows.Forms.Button();
            this.groupBox4.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.groupBox6);
            this.groupBox4.Controls.Add(this.groupBox5);
            this.groupBox4.Location = new System.Drawing.Point(6, 1);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(715, 228);
            this.groupBox4.TabIndex = 203;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Asignado Proveedor ";
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.txtDesProv);
            this.groupBox6.Controls.Add(this.cmdMoneda);
            this.groupBox6.Controls.Add(this.cmdPdO);
            this.groupBox6.Controls.Add(this.label27);
            this.groupBox6.Controls.Add(this.label24);
            this.groupBox6.Controls.Add(this.button5);
            this.groupBox6.Controls.Add(this.label25);
            this.groupBox6.Controls.Add(this.txtCodProv);
            this.groupBox6.Location = new System.Drawing.Point(6, 108);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(702, 107);
            this.groupBox6.TabIndex = 1;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Proveedor";
            // 
            // txtDesProv
            // 
            this.txtDesProv.BackColor = System.Drawing.Color.DimGray;
            this.txtDesProv.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDesProv.ForeColor = System.Drawing.SystemColors.Window;
            this.txtDesProv.Location = new System.Drawing.Point(366, 19);
            this.txtDesProv.Name = "txtDesProv";
            this.txtDesProv.Size = new System.Drawing.Size(323, 21);
            this.txtDesProv.TabIndex = 207;
            // 
            // cmdMoneda
            // 
            this.cmdMoneda.FormattingEnabled = true;
            this.cmdMoneda.Location = new System.Drawing.Point(116, 73);
            this.cmdMoneda.Name = "cmdMoneda";
            this.cmdMoneda.Size = new System.Drawing.Size(275, 21);
            this.cmdMoneda.TabIndex = 206;
            this.cmdMoneda.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cmdMoneda_KeyPress);
            // 
            // cmdPdO
            // 
            this.cmdPdO.FormattingEnabled = true;
            this.cmdPdO.Location = new System.Drawing.Point(117, 46);
            this.cmdPdO.Name = "cmdPdO";
            this.cmdPdO.Size = new System.Drawing.Size(275, 21);
            this.cmdPdO.TabIndex = 205;
            this.cmdPdO.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cmdPdO_KeyPress);
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(56, 77);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(49, 13);
            this.label27.TabIndex = 204;
            this.label27.Text = "Moneda:";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(28, 49);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(81, 13);
            this.label24.TabIndex = 5;
            this.label24.Text = "Pais De Origen:";
            // 
            // button5
            // 
            this.button5.Image = global::lebrun.Properties.Resources.Search_16x16;
            this.button5.Location = new System.Drawing.Point(331, 17);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(29, 23);
            this.button5.TabIndex = 203;
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(9, 22);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(102, 13);
            this.label25.TabIndex = 4;
            this.label25.Text = "Proveedor Principal:";
            // 
            // txtCodProv
            // 
            this.txtCodProv.BackColor = System.Drawing.Color.DimGray;
            this.txtCodProv.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCodProv.ForeColor = System.Drawing.SystemColors.Window;
            this.txtCodProv.Location = new System.Drawing.Point(117, 19);
            this.txtCodProv.Name = "txtCodProv";
            this.txtCodProv.Size = new System.Drawing.Size(208, 21);
            this.txtCodProv.TabIndex = 2;
            this.txtCodProv.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCodProv_KeyPress);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.label6);
            this.groupBox5.Controls.Add(this.label5);
            this.groupBox5.Controls.Add(this.txtDesProd);
            this.groupBox5.Controls.Add(this.txtCodProd);
            this.groupBox5.Location = new System.Drawing.Point(6, 19);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(702, 83);
            this.groupBox5.TabIndex = 0;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Producto";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(39, 58);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 13);
            this.label6.TabIndex = 3;
            this.label6.Text = "Producto:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(7, 24);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(89, 13);
            this.label5.TabIndex = 2;
            this.label5.Text = "Codigo Producto:";
            // 
            // txtDesProd
            // 
            this.txtDesProd.BackColor = System.Drawing.Color.DimGray;
            this.txtDesProd.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDesProd.ForeColor = System.Drawing.SystemColors.Window;
            this.txtDesProd.Location = new System.Drawing.Point(102, 55);
            this.txtDesProd.Name = "txtDesProd";
            this.txtDesProd.Size = new System.Drawing.Size(592, 21);
            this.txtDesProd.TabIndex = 1;
            // 
            // txtCodProd
            // 
            this.txtCodProd.BackColor = System.Drawing.Color.DimGray;
            this.txtCodProd.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCodProd.ForeColor = System.Drawing.SystemColors.Window;
            this.txtCodProd.Location = new System.Drawing.Point(102, 19);
            this.txtCodProd.Name = "txtCodProd";
            this.txtCodProd.Size = new System.Drawing.Size(592, 21);
            this.txtCodProd.TabIndex = 0;
            // 
            // btnLimpiar
            // 
            this.btnLimpiar.ForeColor = System.Drawing.SystemColors.WindowText;
            this.btnLimpiar.Image = global::lebrun.Properties.Resources.icono_limpieza;
            this.btnLimpiar.Location = new System.Drawing.Point(387, 247);
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.Size = new System.Drawing.Size(55, 47);
            this.btnLimpiar.TabIndex = 206;
            this.btnLimpiar.UseVisualStyleBackColor = true;
            this.btnLimpiar.Click += new System.EventHandler(this.btnLimpiar_Click);
            // 
            // BtnRegistra
            // 
            this.BtnRegistra.Image = global::lebrun.Properties.Resources.Add_24x24;
            this.BtnRegistra.Location = new System.Drawing.Point(323, 247);
            this.BtnRegistra.Name = "BtnRegistra";
            this.BtnRegistra.Size = new System.Drawing.Size(58, 47);
            this.BtnRegistra.TabIndex = 205;
            this.BtnRegistra.UseVisualStyleBackColor = true;
            this.BtnRegistra.Click += new System.EventHandler(this.BtnRegistra_Click);
            // 
            // frmAsignarProveedor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(726, 305);
            this.Controls.Add(this.btnLimpiar);
            this.Controls.Add(this.BtnRegistra);
            this.Controls.Add(this.groupBox4);
            this.Name = "frmAsignarProveedor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Asignado Proveedor";
            this.Load += new System.EventHandler(this.frmAsignarProveedor_Load);
            this.Shown += new System.EventHandler(this.frmAsignarProveedor_Shown);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmAsignarProveedor_FormClosed);
            this.groupBox4.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.ComboBox cmdMoneda;
        private System.Windows.Forms.ComboBox cmdPdO;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnLimpiar;
        private System.Windows.Forms.Button BtnRegistra;
        public System.Windows.Forms.TextBox txtDesProd;
        public System.Windows.Forms.TextBox txtCodProd;
        public System.Windows.Forms.TextBox txtDesProv;
        public System.Windows.Forms.TextBox txtCodProv;

    }
}