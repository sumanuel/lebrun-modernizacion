namespace lebrun.formularios.transporte
{
    partial class lbxTransporte
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.cmdLimpiar = new System.Windows.Forms.Button();
            this.cmdSalir = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.cmdModificar = new System.Windows.Forms.Button();
            this.cmdAgregar = new System.Windows.Forms.Button();
            this.cmdBuscar = new System.Windows.Forms.Button();
            this.txtBuscar = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.cmdSalirDatos = new System.Windows.Forms.Button();
            this.cmdModificarTrans = new System.Windows.Forms.Button();
            this.cmdAcepAgre = new System.Windows.Forms.Button();
            this.label27 = new System.Windows.Forms.Label();
            this.txtObservaciones = new System.Windows.Forms.TextBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtProveedor = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtDescripcion = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtCodigoTrans = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(12, 5);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(829, 388);
            this.tabControl1.TabIndex = 39;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.dataGridView1);
            this.tabPage1.Controls.Add(this.cmdLimpiar);
            this.tabPage1.Controls.Add(this.cmdSalir);
            this.tabPage1.Controls.Add(this.button4);
            this.tabPage1.Controls.Add(this.cmdModificar);
            this.tabPage1.Controls.Add(this.cmdAgregar);
            this.tabPage1.Controls.Add(this.cmdBuscar);
            this.tabPage1.Controls.Add(this.txtBuscar);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(821, 362);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Busqueda De Transporte";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dataGridView1.Location = new System.Drawing.Point(9, 50);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(796, 295);
            this.dataGridView1.TabIndex = 47;
            // 
            // cmdLimpiar
            // 
            this.cmdLimpiar.Image = global::lebrun.Properties.Resources.Refresh_24x24;
            this.cmdLimpiar.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.cmdLimpiar.Location = new System.Drawing.Point(574, 6);
            this.cmdLimpiar.Name = "cmdLimpiar";
            this.cmdLimpiar.Size = new System.Drawing.Size(82, 36);
            this.cmdLimpiar.TabIndex = 45;
            this.cmdLimpiar.Text = "Limpiar";
            this.cmdLimpiar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmdLimpiar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.cmdLimpiar.UseVisualStyleBackColor = true;
            this.cmdLimpiar.Click += new System.EventHandler(this.cmdLimpiar_Click);
            // 
            // cmdSalir
            // 
            this.cmdSalir.Image = global::lebrun.Properties.Resources.LogOut_24x24;
            this.cmdSalir.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.cmdSalir.Location = new System.Drawing.Point(662, 6);
            this.cmdSalir.Name = "cmdSalir";
            this.cmdSalir.Size = new System.Drawing.Size(82, 36);
            this.cmdSalir.TabIndex = 46;
            this.cmdSalir.Text = "Salir";
            this.cmdSalir.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmdSalir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.cmdSalir.UseVisualStyleBackColor = true;
            this.cmdSalir.Click += new System.EventHandler(this.cmdSalir_Click);
            // 
            // button4
            // 
            this.button4.Image = global::lebrun.Properties.Resources.Delete_24x24;
            this.button4.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.button4.Location = new System.Drawing.Point(484, 6);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(82, 36);
            this.button4.TabIndex = 44;
            this.button4.Text = "Eliminar";
            this.button4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button4.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button4.UseVisualStyleBackColor = true;
            // 
            // cmdModificar
            // 
            this.cmdModificar.Image = global::lebrun.Properties.Resources.Edit_24x24;
            this.cmdModificar.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.cmdModificar.Location = new System.Drawing.Point(396, 6);
            this.cmdModificar.Name = "cmdModificar";
            this.cmdModificar.Size = new System.Drawing.Size(82, 36);
            this.cmdModificar.TabIndex = 43;
            this.cmdModificar.Text = "Modificar";
            this.cmdModificar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmdModificar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.cmdModificar.UseVisualStyleBackColor = true;
            this.cmdModificar.Click += new System.EventHandler(this.cmdModificar_Click);
            // 
            // cmdAgregar
            // 
            this.cmdAgregar.Image = global::lebrun.Properties.Resources.Add_24x24;
            this.cmdAgregar.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.cmdAgregar.Location = new System.Drawing.Point(306, 6);
            this.cmdAgregar.Name = "cmdAgregar";
            this.cmdAgregar.Size = new System.Drawing.Size(82, 36);
            this.cmdAgregar.TabIndex = 42;
            this.cmdAgregar.Text = "Agregar";
            this.cmdAgregar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmdAgregar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.cmdAgregar.UseVisualStyleBackColor = true;
            this.cmdAgregar.Click += new System.EventHandler(this.cmdAgregar_Click);
            // 
            // cmdBuscar
            // 
            this.cmdBuscar.Image = global::lebrun.Properties.Resources.Search_24x24;
            this.cmdBuscar.Location = new System.Drawing.Point(240, 3);
            this.cmdBuscar.Name = "cmdBuscar";
            this.cmdBuscar.Size = new System.Drawing.Size(45, 36);
            this.cmdBuscar.TabIndex = 40;
            this.cmdBuscar.UseVisualStyleBackColor = true;
            this.cmdBuscar.Click += new System.EventHandler(this.cmdBuscar_Click);
            // 
            // txtBuscar
            // 
            this.txtBuscar.Location = new System.Drawing.Point(9, 19);
            this.txtBuscar.Name = "txtBuscar";
            this.txtBuscar.Size = new System.Drawing.Size(215, 20);
            this.txtBuscar.TabIndex = 39;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(174, 13);
            this.label1.TabIndex = 41;
            this.label1.Text = "Código ,Nombre, Código Proveedor";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.cmdSalirDatos);
            this.tabPage2.Controls.Add(this.cmdModificarTrans);
            this.tabPage2.Controls.Add(this.cmdAcepAgre);
            this.tabPage2.Controls.Add(this.label27);
            this.tabPage2.Controls.Add(this.txtObservaciones);
            this.tabPage2.Controls.Add(this.comboBox1);
            this.tabPage2.Controls.Add(this.label4);
            this.tabPage2.Controls.Add(this.txtProveedor);
            this.tabPage2.Controls.Add(this.label3);
            this.tabPage2.Controls.Add(this.txtDescripcion);
            this.tabPage2.Controls.Add(this.label2);
            this.tabPage2.Controls.Add(this.txtCodigoTrans);
            this.tabPage2.Controls.Add(this.label9);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(821, 362);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Datos Transporte";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // cmdSalirDatos
            // 
            this.cmdSalirDatos.Image = global::lebrun.Properties.Resources.LogOut_24x24;
            this.cmdSalirDatos.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.cmdSalirDatos.Location = new System.Drawing.Point(731, 309);
            this.cmdSalirDatos.Name = "cmdSalirDatos";
            this.cmdSalirDatos.Size = new System.Drawing.Size(82, 36);
            this.cmdSalirDatos.TabIndex = 192;
            this.cmdSalirDatos.Text = "Salir";
            this.cmdSalirDatos.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmdSalirDatos.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.cmdSalirDatos.UseVisualStyleBackColor = true;
            this.cmdSalirDatos.Click += new System.EventHandler(this.cmdSalirDatos_Click);
            // 
            // cmdModificarTrans
            // 
            this.cmdModificarTrans.Image = global::lebrun.Properties.Resources.Edit_24x24;
            this.cmdModificarTrans.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.cmdModificarTrans.Location = new System.Drawing.Point(643, 309);
            this.cmdModificarTrans.Name = "cmdModificarTrans";
            this.cmdModificarTrans.Size = new System.Drawing.Size(82, 36);
            this.cmdModificarTrans.TabIndex = 191;
            this.cmdModificarTrans.Text = "Modificar";
            this.cmdModificarTrans.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmdModificarTrans.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.cmdModificarTrans.UseVisualStyleBackColor = true;
            this.cmdModificarTrans.Click += new System.EventHandler(this.cmdModificarTrans_Click);
            // 
            // cmdAcepAgre
            // 
            this.cmdAcepAgre.Image = global::lebrun.Properties.Resources.Add_24x24;
            this.cmdAcepAgre.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.cmdAcepAgre.Location = new System.Drawing.Point(555, 309);
            this.cmdAcepAgre.Name = "cmdAcepAgre";
            this.cmdAcepAgre.Size = new System.Drawing.Size(82, 36);
            this.cmdAcepAgre.TabIndex = 190;
            this.cmdAcepAgre.Text = "Agregar";
            this.cmdAcepAgre.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmdAcepAgre.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.cmdAcepAgre.UseVisualStyleBackColor = true;
            this.cmdAcepAgre.Click += new System.EventHandler(this.cmdAcepAgre_Click);
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(6, 190);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(78, 13);
            this.label27.TabIndex = 189;
            this.label27.Text = "Observaciones";
            // 
            // txtObservaciones
            // 
            this.txtObservaciones.Location = new System.Drawing.Point(53, 215);
            this.txtObservaciones.MaxLength = 150;
            this.txtObservaciones.Multiline = true;
            this.txtObservaciones.Name = "txtObservaciones";
            this.txtObservaciones.Size = new System.Drawing.Size(639, 61);
            this.txtObservaciones.TabIndex = 188;
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "Activo",
            "Inactivo",
            "Suspendido"});
            this.comboBox1.Location = new System.Drawing.Point(69, 122);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(135, 21);
            this.comboBox1.TabIndex = 126;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label4.Location = new System.Drawing.Point(6, 125);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(45, 13);
            this.label4.TabIndex = 125;
            this.label4.Text = "Estatus:";
            // 
            // txtProveedor
            // 
            this.txtProveedor.Location = new System.Drawing.Point(69, 84);
            this.txtProveedor.MaxLength = 12;
            this.txtProveedor.Name = "txtProveedor";
            this.txtProveedor.Size = new System.Drawing.Size(136, 20);
            this.txtProveedor.TabIndex = 122;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label3.Location = new System.Drawing.Point(6, 87);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 13);
            this.label3.TabIndex = 123;
            this.label3.Text = "Proveedor: ";
            // 
            // txtDescripcion
            // 
            this.txtDescripcion.Location = new System.Drawing.Point(69, 49);
            this.txtDescripcion.MaxLength = 60;
            this.txtDescripcion.Name = "txtDescripcion";
            this.txtDescripcion.Size = new System.Drawing.Size(586, 20);
            this.txtDescripcion.TabIndex = 120;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label2.Location = new System.Drawing.Point(6, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 13);
            this.label2.TabIndex = 121;
            this.label2.Text = "Descripción: ";
            // 
            // txtCodigoTrans
            // 
            this.txtCodigoTrans.Location = new System.Drawing.Point(69, 11);
            this.txtCodigoTrans.MaxLength = 6;
            this.txtCodigoTrans.Name = "txtCodigoTrans";
            this.txtCodigoTrans.Size = new System.Drawing.Size(136, 20);
            this.txtCodigoTrans.TabIndex = 118;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label9.Location = new System.Drawing.Point(6, 14);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(46, 13);
            this.label9.TabIndex = 119;
            this.label9.Text = "Código: ";
            // 
            // lbxTransporte
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(853, 418);
            this.Controls.Add(this.tabControl1);
            this.MaximizeBox = false;
            this.Name = "lbxTransporte";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Lebrun - lbxTransporte";
            this.Load += new System.EventHandler(this.lbxTransporte_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button cmdLimpiar;
        private System.Windows.Forms.Button cmdSalir;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button cmdModificar;
        private System.Windows.Forms.Button cmdAgregar;
        private System.Windows.Forms.Button cmdBuscar;
        private System.Windows.Forms.TextBox txtBuscar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TextBox txtCodigoTrans;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtDescripcion;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtProveedor;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.TextBox txtObservaciones;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Button cmdSalirDatos;
        private System.Windows.Forms.Button cmdModificarTrans;
        private System.Windows.Forms.Button cmdAcepAgre;

    }
}