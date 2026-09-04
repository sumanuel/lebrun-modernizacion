namespace lebrun.formularios.productos
{
    partial class frmEditorCategoria
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
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtDescricionCategoria = new System.Windows.Forms.TextBox();
            this.txtCodigoCategoria = new System.Windows.Forms.TextBox();
            this.txtComVenta = new System.Windows.Forms.TextBox();
            this.txtComCompra = new System.Windows.Forms.TextBox();
            this.BtnRegistra = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnActulizar = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.ttMesajes = new System.Windows.Forms.ToolTip(this.components);
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(9, 57);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Descripcion";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label1.Location = new System.Drawing.Point(9, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Codigo";
            // 
            // txtDescricionCategoria
            // 
            this.txtDescricionCategoria.Location = new System.Drawing.Point(103, 54);
            this.txtDescricionCategoria.Name = "txtDescricionCategoria";
            this.txtDescricionCategoria.Size = new System.Drawing.Size(161, 20);
            this.txtDescricionCategoria.TabIndex = 5;
            this.txtDescricionCategoria.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDescricionCategoria_KeyPress);
            // 
            // txtCodigoCategoria
            // 
            this.txtCodigoCategoria.Location = new System.Drawing.Point(103, 28);
            this.txtCodigoCategoria.Name = "txtCodigoCategoria";
            this.txtCodigoCategoria.Size = new System.Drawing.Size(111, 20);
            this.txtCodigoCategoria.TabIndex = 4;
            this.txtCodigoCategoria.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtCodigoCategoria.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCodigoCategoria_KeyPress);
            // 
            // txtComVenta
            // 
            this.txtComVenta.Location = new System.Drawing.Point(103, 80);
            this.txtComVenta.Name = "txtComVenta";
            this.txtComVenta.Size = new System.Drawing.Size(75, 20);
            this.txtComVenta.TabIndex = 8;
            this.txtComVenta.Text = "0.00";
            this.txtComVenta.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtComVenta.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtComVenta_KeyPress);
            // 
            // txtComCompra
            // 
            this.txtComCompra.Location = new System.Drawing.Point(103, 106);
            this.txtComCompra.Name = "txtComCompra";
            this.txtComCompra.Size = new System.Drawing.Size(75, 20);
            this.txtComCompra.TabIndex = 9;
            this.txtComCompra.Text = "0.00";
            this.txtComCompra.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtComCompra.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtComCompra_KeyPress);
            // 
            // BtnRegistra
            // 
            this.BtnRegistra.Image = global::lebrun.Properties.Resources.Add_24x24;
            this.BtnRegistra.Location = new System.Drawing.Point(69, 29);
            this.BtnRegistra.Name = "BtnRegistra";
            this.BtnRegistra.Size = new System.Drawing.Size(64, 56);
            this.BtnRegistra.TabIndex = 12;
            this.BtnRegistra.UseVisualStyleBackColor = true;
            this.BtnRegistra.Click += new System.EventHandler(this.BtnRegistra_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.ForeColor = System.Drawing.SystemColors.WindowText;
            this.btnCancelar.Image = global::lebrun.Properties.Resources.Cancel_24x24;
            this.btnCancelar.Location = new System.Drawing.Point(139, 29);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(64, 56);
            this.btnCancelar.TabIndex = 11;
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnActulizar
            // 
            this.btnActulizar.Image = global::lebrun.Properties.Resources.Refresh_24x24;
            this.btnActulizar.Location = new System.Drawing.Point(69, 29);
            this.btnActulizar.Name = "btnActulizar";
            this.btnActulizar.Size = new System.Drawing.Size(64, 56);
            this.btnActulizar.TabIndex = 10;
            this.btnActulizar.UseVisualStyleBackColor = true;
            this.btnActulizar.Click += new System.EventHandler(this.btnActulizar_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.BtnRegistra);
            this.groupBox1.Controls.Add(this.btnActulizar);
            this.groupBox1.Controls.Add(this.btnCancelar);
            this.groupBox1.Location = new System.Drawing.Point(12, 154);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(270, 100);
            this.groupBox1.TabIndex = 13;
            this.groupBox1.TabStop = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.txtCodigoCategoria);
            this.groupBox2.Controls.Add(this.txtComCompra);
            this.groupBox2.Controls.Add(this.txtDescricionCategoria);
            this.groupBox2.Controls.Add(this.txtComVenta);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Location = new System.Drawing.Point(12, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(270, 136);
            this.groupBox2.TabIndex = 14;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Editor Categoria";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(9, 109);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(88, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "Comision Compra";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(9, 83);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Comision Venta";
            // 
            // frmEditorCategoria
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(289, 266);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.Name = "frmEditorCategoria";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Editor Categoria";
            this.Load += new System.EventHandler(this.frmEditorCategoria_Load);
            this.Shown += new System.EventHandler(this.frmEditorCategoria_Shown);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmEditorCategoria_FormClosed);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.TextBox txtDescricionCategoria;
        public System.Windows.Forms.TextBox txtCodigoCategoria;
        public System.Windows.Forms.TextBox txtComVenta;
        public System.Windows.Forms.TextBox txtComCompra;
        private System.Windows.Forms.Button BtnRegistra;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnActulizar;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ToolTip ttMesajes;
    }
}