namespace lebrun.formularios.productos
{
    partial class frmEdicionArancel
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtDescricionArancel = new System.Windows.Forms.TextBox();
            this.txtCodigoArancel = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtImpuesto = new System.Windows.Forms.TextBox();
            this.txtKg = new System.Windows.Forms.TextBox();
            this.txtProcentajeArancel = new System.Windows.Forms.TextBox();
            this.btnActulizar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.BtnRegistra = new System.Windows.Forms.Button();
            this.ttmensaje = new System.Windows.Forms.ToolTip(this.components);
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtDescricionArancel);
            this.groupBox1.Controls.Add(this.txtCodigoArancel);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(328, 86);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Informacion Arancel";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label2.Location = new System.Drawing.Point(6, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Descripcion";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label1.Location = new System.Drawing.Point(6, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Codigo";
            // 
            // txtDescricionArancel
            // 
            this.txtDescricionArancel.Location = new System.Drawing.Point(70, 53);
            this.txtDescricionArancel.Name = "txtDescricionArancel";
            this.txtDescricionArancel.Size = new System.Drawing.Size(252, 20);
            this.txtDescricionArancel.TabIndex = 1;
            this.txtDescricionArancel.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDescricionArancel_KeyPress);
            // 
            // txtCodigoArancel
            // 
            this.txtCodigoArancel.Location = new System.Drawing.Point(70, 26);
            this.txtCodigoArancel.Name = "txtCodigoArancel";
            this.txtCodigoArancel.Size = new System.Drawing.Size(111, 20);
            this.txtCodigoArancel.TabIndex = 0;
            this.txtCodigoArancel.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtCodigoArancel.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCodigoArancel_KeyPress);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.txtImpuesto);
            this.groupBox2.Controls.Add(this.txtKg);
            this.groupBox2.Controls.Add(this.txtProcentajeArancel);
            this.groupBox2.Location = new System.Drawing.Point(12, 104);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(328, 91);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Detalles Arancel";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(6, 25);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Porcentaje";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(106, 59);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(20, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "Kg";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(174, 25);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(50, 13);
            this.label5.TabIndex = 6;
            this.label5.Text = "Impuesto";
            // 
            // txtImpuesto
            // 
            this.txtImpuesto.Location = new System.Drawing.Point(230, 22);
            this.txtImpuesto.Name = "txtImpuesto";
            this.txtImpuesto.Size = new System.Drawing.Size(51, 20);
            this.txtImpuesto.TabIndex = 6;
            this.txtImpuesto.Text = "0.00";
            this.txtImpuesto.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtImpuesto.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtImpuesto_KeyPress);
            // 
            // txtKg
            // 
            this.txtKg.Location = new System.Drawing.Point(132, 56);
            this.txtKg.Name = "txtKg";
            this.txtKg.Size = new System.Drawing.Size(49, 20);
            this.txtKg.TabIndex = 5;
            this.txtKg.Text = "0";
            this.txtKg.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtKg.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtKg_KeyPress);
            // 
            // txtProcentajeArancel
            // 
            this.txtProcentajeArancel.Location = new System.Drawing.Point(70, 22);
            this.txtProcentajeArancel.Name = "txtProcentajeArancel";
            this.txtProcentajeArancel.Size = new System.Drawing.Size(57, 20);
            this.txtProcentajeArancel.TabIndex = 4;
            this.txtProcentajeArancel.Text = "0";
            this.txtProcentajeArancel.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtProcentajeArancel.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtProcentajeArancel_KeyPress);
            // 
            // btnActulizar
            // 
            this.btnActulizar.Image = global::lebrun.Properties.Resources.Refresh_24x24;
            this.btnActulizar.Location = new System.Drawing.Point(105, 201);
            this.btnActulizar.Name = "btnActulizar";
            this.btnActulizar.Size = new System.Drawing.Size(64, 56);
            this.btnActulizar.TabIndex = 2;
            this.btnActulizar.UseVisualStyleBackColor = true;
            this.btnActulizar.Click += new System.EventHandler(this.btnActulizar_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.ForeColor = System.Drawing.SystemColors.WindowText;
            this.btnCancelar.Image = global::lebrun.Properties.Resources.Cancel_24x24;
            this.btnCancelar.Location = new System.Drawing.Point(175, 201);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(64, 56);
            this.btnCancelar.TabIndex = 3;
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.button2_Click);
            // 
            // BtnRegistra
            // 
            this.BtnRegistra.Image = global::lebrun.Properties.Resources.Add_24x24;
            this.BtnRegistra.Location = new System.Drawing.Point(105, 201);
            this.BtnRegistra.Name = "BtnRegistra";
            this.BtnRegistra.Size = new System.Drawing.Size(64, 56);
            this.BtnRegistra.TabIndex = 4;
            this.BtnRegistra.UseVisualStyleBackColor = true;
            this.BtnRegistra.Click += new System.EventHandler(this.BtnRegistra_Click);
            // 
            // frmEdicionArancel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(342, 269);
            this.Controls.Add(this.BtnRegistra);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnActulizar);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.Name = "frmEdicionArancel";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Agregar Arancel";
            this.Load += new System.EventHandler(this.frmEdicionArancel_Load);
            this.Shown += new System.EventHandler(this.frmEdicionArancel_Shown);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmEdicionArancel_FormClosed);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.frmEdicionArancel_KeyPress);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnActulizar;
        private System.Windows.Forms.Button btnCancelar;
        public System.Windows.Forms.TextBox txtCodigoArancel;
        public System.Windows.Forms.TextBox txtDescricionArancel;
        public System.Windows.Forms.TextBox txtImpuesto;
        public System.Windows.Forms.TextBox txtKg;
        public System.Windows.Forms.TextBox txtProcentajeArancel;
        private System.Windows.Forms.Button BtnRegistra;
        private System.Windows.Forms.ToolTip ttmensaje;
    }
}