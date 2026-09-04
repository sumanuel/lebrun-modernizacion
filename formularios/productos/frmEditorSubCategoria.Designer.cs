namespace lebrun.formularios.productos
{
    partial class frmEditorSubCategoria
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
            this.txtDescripCategoria = new System.Windows.Forms.TextBox();
            this.txtCodigoCategoria = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtComCompraSubCategoria = new System.Windows.Forms.TextBox();
            this.txtComVentasSubCategoria = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtDescripSubCategoria = new System.Windows.Forms.TextBox();
            this.txtCodigoSubCategoria = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.BtnRegistra = new System.Windows.Forms.Button();
            this.btnActulizar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.ttMensaje = new System.Windows.Forms.ToolTip(this.components);
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtDescripCategoria);
            this.groupBox1.Controls.Add(this.txtCodigoCategoria);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(405, 50);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Datos Grupos";
            // 
            // txtDescripCategoria
            // 
            this.txtDescripCategoria.Location = new System.Drawing.Point(226, 19);
            this.txtDescripCategoria.Name = "txtDescripCategoria";
            this.txtDescripCategoria.Size = new System.Drawing.Size(173, 20);
            this.txtDescripCategoria.TabIndex = 3;
            this.txtDescripCategoria.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtCodigoCategoria
            // 
            this.txtCodigoCategoria.Location = new System.Drawing.Point(51, 19);
            this.txtCodigoCategoria.Name = "txtCodigoCategoria";
            this.txtCodigoCategoria.Size = new System.Drawing.Size(106, 20);
            this.txtCodigoCategoria.TabIndex = 2;
            this.txtCodigoCategoria.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(163, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Descripcion";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Codigo";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtComCompraSubCategoria);
            this.groupBox2.Controls.Add(this.txtComVentasSubCategoria);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.txtDescripSubCategoria);
            this.groupBox2.Controls.Add(this.txtCodigoSubCategoria);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Location = new System.Drawing.Point(12, 68);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(405, 164);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Datos Sub-Grupos";
            // 
            // txtComCompraSubCategoria
            // 
            this.txtComCompraSubCategoria.Location = new System.Drawing.Point(131, 121);
            this.txtComCompraSubCategoria.Name = "txtComCompraSubCategoria";
            this.txtComCompraSubCategoria.Size = new System.Drawing.Size(92, 20);
            this.txtComCompraSubCategoria.TabIndex = 8;
            this.txtComCompraSubCategoria.Text = "0.00";
            this.txtComCompraSubCategoria.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtComCompraSubCategoria.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtComCompraSubCategoria_KeyPress);
            // 
            // txtComVentasSubCategoria
            // 
            this.txtComVentasSubCategoria.Location = new System.Drawing.Point(131, 88);
            this.txtComVentasSubCategoria.Name = "txtComVentasSubCategoria";
            this.txtComVentasSubCategoria.Size = new System.Drawing.Size(92, 20);
            this.txtComVentasSubCategoria.TabIndex = 9;
            this.txtComVentasSubCategoria.Text = "0.00";
            this.txtComVentasSubCategoria.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtComVentasSubCategoria.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtComVentasSubCategoria_KeyPress);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(37, 128);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(88, 13);
            this.label5.TabIndex = 6;
            this.label5.Text = "Comision Compra";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(45, 95);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(80, 13);
            this.label6.TabIndex = 7;
            this.label6.Text = "Comision Venta";
            // 
            // txtDescripSubCategoria
            // 
            this.txtDescripSubCategoria.Location = new System.Drawing.Point(131, 58);
            this.txtDescripSubCategoria.Name = "txtDescripSubCategoria";
            this.txtDescripSubCategoria.Size = new System.Drawing.Size(268, 20);
            this.txtDescripSubCategoria.TabIndex = 5;
            this.txtDescripSubCategoria.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDescripSubCategoria_KeyPress);
            // 
            // txtCodigoSubCategoria
            // 
            this.txtCodigoSubCategoria.Location = new System.Drawing.Point(131, 23);
            this.txtCodigoSubCategoria.Name = "txtCodigoSubCategoria";
            this.txtCodigoSubCategoria.Size = new System.Drawing.Size(92, 20);
            this.txtCodigoSubCategoria.TabIndex = 5;
            this.txtCodigoSubCategoria.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtCodigoSubCategoria.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCodigoSubCategoria_KeyPress);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(0, 61);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(133, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Descripcion Sub-Categoria";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(1)))));
            this.label3.Location = new System.Drawing.Point(15, 26);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(110, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Codigo Sub-Categoria";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.BtnRegistra);
            this.groupBox3.Controls.Add(this.btnActulizar);
            this.groupBox3.Controls.Add(this.btnCancelar);
            this.groupBox3.Location = new System.Drawing.Point(12, 238);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(405, 100);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            // 
            // BtnRegistra
            // 
            this.BtnRegistra.Image = global::lebrun.Properties.Resources.Add_24x24;
            this.BtnRegistra.Location = new System.Drawing.Point(143, 28);
            this.BtnRegistra.Name = "BtnRegistra";
            this.BtnRegistra.Size = new System.Drawing.Size(64, 56);
            this.BtnRegistra.TabIndex = 15;
            this.BtnRegistra.UseVisualStyleBackColor = true;
            this.BtnRegistra.Click += new System.EventHandler(this.BtnRegistra_Click);
            // 
            // btnActulizar
            // 
            this.btnActulizar.Image = global::lebrun.Properties.Resources.Refresh_24x24;
            this.btnActulizar.Location = new System.Drawing.Point(143, 28);
            this.btnActulizar.Name = "btnActulizar";
            this.btnActulizar.Size = new System.Drawing.Size(64, 56);
            this.btnActulizar.TabIndex = 13;
            this.btnActulizar.UseVisualStyleBackColor = true;
            this.btnActulizar.Click += new System.EventHandler(this.btnActulizar_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.ForeColor = System.Drawing.SystemColors.WindowText;
            this.btnCancelar.Image = global::lebrun.Properties.Resources.Cancel_24x24;
            this.btnCancelar.Location = new System.Drawing.Point(215, 28);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(64, 56);
            this.btnCancelar.TabIndex = 14;
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // frmEditorSubCategoria
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(426, 351);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.Name = "frmEditorSubCategoria";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Editor Sub-Categoria";
            this.Load += new System.EventHandler(this.frmEditorSubCategoria_Load);
            this.Shown += new System.EventHandler(this.frmEditorSubCategoria_Shown);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmEditorSubCategoria_FormClosed);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button BtnRegistra;
        private System.Windows.Forms.Button btnActulizar;
        private System.Windows.Forms.Button btnCancelar;
        public System.Windows.Forms.TextBox txtComCompraSubCategoria;
        public System.Windows.Forms.TextBox txtComVentasSubCategoria;
        public System.Windows.Forms.TextBox txtDescripSubCategoria;
        public System.Windows.Forms.TextBox txtCodigoSubCategoria;
        public System.Windows.Forms.TextBox txtCodigoCategoria;
        public System.Windows.Forms.TextBox txtDescripCategoria;
        private System.Windows.Forms.ToolTip ttMensaje;
    }
}