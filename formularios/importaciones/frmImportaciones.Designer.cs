namespace lebrun.formularios.importaciones
{
	partial class frmImportaciones
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnOtrosDatos = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.btnOtros = new System.Windows.Forms.Button();
            this.btnFacOrigen = new System.Windows.Forms.Button();
            this.btnMsj = new System.Windows.Forms.Button();
            this.btnFacNacional = new System.Windows.Forms.Button();
            this.btnProductos = new System.Windows.Forms.Button();
            this.groupImportar = new System.Windows.Forms.GroupBox();
            this.txtNumeroOrden = new System.Windows.Forms.TextBox();
            this.btnImportar = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btnConfirmar = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnAceptar = new System.Windows.Forms.Button();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.btnSalir = new System.Windows.Forms.Button();
            this.panel2.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupImportar.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.ForeColor = System.Drawing.Color.Black;
            this.panel1.Location = new System.Drawing.Point(180, 21);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(818, 435);
            this.panel1.TabIndex = 0;
            
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.Control;
            this.panel2.Controls.Add(this.groupBox2);
            this.panel2.Controls.Add(this.groupImportar);
            this.panel2.Location = new System.Drawing.Point(5, 21);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(169, 479);
            this.panel2.TabIndex = 19;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnOtrosDatos);
            this.groupBox2.Controls.Add(this.button1);
            this.groupBox2.Controls.Add(this.btnOtros);
            this.groupBox2.Controls.Add(this.btnFacOrigen);
            this.groupBox2.Controls.Add(this.btnMsj);
            this.groupBox2.Controls.Add(this.btnFacNacional);
            this.groupBox2.Controls.Add(this.btnProductos);
            this.groupBox2.Location = new System.Drawing.Point(8, 174);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(147, 300);
            this.groupBox2.TabIndex = 26;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Opciones";
            // 
            // btnOtrosDatos
            // 
            this.btnOtrosDatos.Location = new System.Drawing.Point(6, 173);
            this.btnOtrosDatos.Name = "btnOtrosDatos";
            this.btnOtrosDatos.Size = new System.Drawing.Size(135, 37);
            this.btnOtrosDatos.TabIndex = 31;
            this.btnOtrosDatos.Text = "Otros Datos";
            this.btnOtrosDatos.UseVisualStyleBackColor = true;
            this.btnOtrosDatos.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(6, 253);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(135, 37);
            this.button1.TabIndex = 30;
            this.button1.Text = "Proyecciones";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            this.button1.MouseLeave += new System.EventHandler(this.btnConfirmar_MouseLeave);
            this.button1.Click += new System.EventHandler(this.button1_Click);
            this.button1.MouseEnter += new System.EventHandler(this.btnConfirmar_MouseEnter);
            // 
            // btnOtros
            // 
            this.btnOtros.Location = new System.Drawing.Point(6, 133);
            this.btnOtros.Name = "btnOtros";
            this.btnOtros.Size = new System.Drawing.Size(135, 37);
            this.btnOtros.TabIndex = 29;
            this.btnOtros.Text = "Otros Gastos";
            this.btnOtros.UseVisualStyleBackColor = true;
            this.btnOtros.MouseLeave += new System.EventHandler(this.btnConfirmar_MouseLeave);
            this.btnOtros.Click += new System.EventHandler(this.btnOtros_Click);
            this.btnOtros.MouseEnter += new System.EventHandler(this.btnConfirmar_MouseEnter);
            // 
            // btnFacOrigen
            // 
            this.btnFacOrigen.Location = new System.Drawing.Point(6, 53);
            this.btnFacOrigen.Name = "btnFacOrigen";
            this.btnFacOrigen.Size = new System.Drawing.Size(135, 37);
            this.btnFacOrigen.TabIndex = 28;
            this.btnFacOrigen.Text = "Factura Importacion";
            this.btnFacOrigen.UseVisualStyleBackColor = true;
            this.btnFacOrigen.MouseLeave += new System.EventHandler(this.btnConfirmar_MouseLeave);
            this.btnFacOrigen.Click += new System.EventHandler(this.btnFacOrigen_Click);
            this.btnFacOrigen.MouseEnter += new System.EventHandler(this.btnConfirmar_MouseEnter);
            // 
            // btnMsj
            // 
            this.btnMsj.Location = new System.Drawing.Point(6, 213);
            this.btnMsj.Name = "btnMsj";
            this.btnMsj.Size = new System.Drawing.Size(135, 37);
            this.btnMsj.TabIndex = 27;
            this.btnMsj.Text = "Calcular";
            this.btnMsj.UseVisualStyleBackColor = true;
            this.btnMsj.MouseLeave += new System.EventHandler(this.btnConfirmar_MouseLeave);
            this.btnMsj.Click += new System.EventHandler(this.btnMsj_Click);
            this.btnMsj.MouseEnter += new System.EventHandler(this.btnConfirmar_MouseEnter);
            // 
            // btnFacNacional
            // 
            this.btnFacNacional.Location = new System.Drawing.Point(6, 93);
            this.btnFacNacional.Name = "btnFacNacional";
            this.btnFacNacional.Size = new System.Drawing.Size(135, 37);
            this.btnFacNacional.TabIndex = 26;
            this.btnFacNacional.Text = "Factura Nacional";
            this.btnFacNacional.UseVisualStyleBackColor = true;
            this.btnFacNacional.MouseLeave += new System.EventHandler(this.btnConfirmar_MouseLeave);
            this.btnFacNacional.Click += new System.EventHandler(this.btnGastos_Click_1);
            this.btnFacNacional.MouseEnter += new System.EventHandler(this.btnConfirmar_MouseEnter);
            // 
            // btnProductos
            // 
            this.btnProductos.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnProductos.Location = new System.Drawing.Point(6, 13);
            this.btnProductos.Name = "btnProductos";
            this.btnProductos.Size = new System.Drawing.Size(135, 37);
            this.btnProductos.TabIndex = 25;
            this.btnProductos.Text = "Articulos";
            this.btnProductos.UseVisualStyleBackColor = true;
            this.btnProductos.MouseLeave += new System.EventHandler(this.btnConfirmar_MouseLeave);
            this.btnProductos.Click += new System.EventHandler(this.btnProductos_Click_1);
            this.btnProductos.MouseEnter += new System.EventHandler(this.btnConfirmar_MouseEnter);
            // 
            // groupImportar
            // 
            this.groupImportar.Controls.Add(this.txtNumeroOrden);
            this.groupImportar.Controls.Add(this.btnImportar);
            this.groupImportar.Controls.Add(this.label1);
            this.groupImportar.Controls.Add(this.btnConfirmar);
            this.groupImportar.Location = new System.Drawing.Point(5, 12);
            this.groupImportar.Name = "groupImportar";
            this.groupImportar.Size = new System.Drawing.Size(150, 156);
            this.groupImportar.TabIndex = 25;
            this.groupImportar.TabStop = false;
            this.groupImportar.Text = "Importar";
            // 
            // txtNumeroOrden
            // 
            this.txtNumeroOrden.Location = new System.Drawing.Point(6, 38);
            this.txtNumeroOrden.Name = "txtNumeroOrden";
            this.txtNumeroOrden.Size = new System.Drawing.Size(138, 20);
            this.txtNumeroOrden.TabIndex = 33;
            // 
            // btnImportar
            // 
            this.btnImportar.Location = new System.Drawing.Point(74, 60);
            this.btnImportar.Name = "btnImportar";
            this.btnImportar.Size = new System.Drawing.Size(70, 35);
            this.btnImportar.TabIndex = 32;
            this.btnImportar.Text = "Buscar";
            this.btnImportar.UseVisualStyleBackColor = true;
            this.btnImportar.Click += new System.EventHandler(this.btnImportar_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 13);
            this.label1.TabIndex = 30;
            this.label1.Text = "Orden:";
            // 
            // btnConfirmar
            // 
            this.btnConfirmar.Location = new System.Drawing.Point(74, 101);
            this.btnConfirmar.Name = "btnConfirmar";
            this.btnConfirmar.Size = new System.Drawing.Size(70, 35);
            this.btnConfirmar.TabIndex = 29;
            this.btnConfirmar.Text = "Importar";
            this.btnConfirmar.UseVisualStyleBackColor = true;
            this.btnConfirmar.MouseLeave += new System.EventHandler(this.btnConfirmar_MouseLeave);
            this.btnConfirmar.Click += new System.EventHandler(this.btnConfirmar_Click);
            this.btnConfirmar.MouseEnter += new System.EventHandler(this.btnConfirmar_MouseEnter);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnAceptar);
            this.groupBox3.Controls.Add(this.btnGuardar);
            this.groupBox3.Controls.Add(this.btnSalir);
            this.groupBox3.Location = new System.Drawing.Point(718, 462);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(280, 57);
            this.groupBox3.TabIndex = 20;
            this.groupBox3.TabStop = false;
            // 
            // btnAceptar
            // 
            this.btnAceptar.Location = new System.Drawing.Point(17, 14);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(70, 35);
            this.btnAceptar.TabIndex = 21;
            this.btnAceptar.Text = "Cierre Definitivo";
            this.btnAceptar.UseVisualStyleBackColor = true;
            this.btnAceptar.Click += new System.EventHandler(this.btnAceptar_Click_1);
            // 
            // btnGuardar
            // 
            this.btnGuardar.Location = new System.Drawing.Point(107, 14);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(70, 35);
            this.btnGuardar.TabIndex = 20;
            this.btnGuardar.Text = "Guardar Temporal";
            this.btnGuardar.UseVisualStyleBackColor = true;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // btnSalir
            // 
            this.btnSalir.Location = new System.Drawing.Point(194, 14);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(70, 35);
            this.btnSalir.TabIndex = 19;
            this.btnSalir.Text = "Salir";
            this.btnSalir.UseVisualStyleBackColor = true;
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click_1);
            // 
            // frmImportaciones
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(106)))), ((int)(((byte)(197)))));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(1010, 522);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.ForeColor = System.Drawing.Color.Black;
            this.Name = "frmImportaciones";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Importaciones";
            this.Load += new System.EventHandler(this.frmImportaciones_Load);
            this.panel2.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupImportar.ResumeLayout(false);
            this.groupImportar.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);

		}

		#endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnSalir;
        public System.Windows.Forms.GroupBox groupImportar;
        public System.Windows.Forms.TextBox txtNumeroOrden;
        public System.Windows.Forms.Button btnGuardar;
        public System.Windows.Forms.Button btnAceptar;
        public System.Windows.Forms.Button btnImportar;
        public System.Windows.Forms.Button btnConfirmar;
        public System.Windows.Forms.Panel panel1;
        public System.Windows.Forms.Button btnFacOrigen;
        public System.Windows.Forms.Button btnMsj;
        public System.Windows.Forms.Button btnFacNacional;
        public System.Windows.Forms.Button btnProductos;
        public System.Windows.Forms.Button btnOtros;
        public System.Windows.Forms.Button button1;
        public System.Windows.Forms.Button btnOtrosDatos;



    }
}