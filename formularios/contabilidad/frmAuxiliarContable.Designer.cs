namespace lebrun.formularios.contabilidad
{
    partial class frmAuxiliarContable
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
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.txtNombreAuxiliar = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtCodigoAuxiliar = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.chkManejaDocumentos = new System.Windows.Forms.CheckBox();
            this.cmdSalir = new System.Windows.Forms.Button();
            this.cmdAgregar = new System.Windows.Forms.Button();
            this.label27 = new System.Windows.Forms.Label();
            this.cmdModificar = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txtMontoApertura = new System.Windows.Forms.TextBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label11 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.rbtEliminar = new System.Windows.Forms.RadioButton();
            this.rbtModificar = new System.Windows.Forms.RadioButton();
            this.cmdCancelarSupervicion = new System.Windows.Forms.Button();
            this.cmdAceptarSupervicion = new System.Windows.Forms.Button();
            this.txtClave = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(461, 7);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(53, 20);
            this.label9.TabIndex = 33;
            this.label9.Text = "zaaaa";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(346, 12);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(109, 13);
            this.label8.TabIndex = 32;
            this.label8.Text = "Código Plan Cuentas:";
            // 
            // txtNombreAuxiliar
            // 
            this.txtNombreAuxiliar.Location = new System.Drawing.Point(412, 69);
            this.txtNombreAuxiliar.Name = "txtNombreAuxiliar";
            this.txtNombreAuxiliar.Size = new System.Drawing.Size(164, 20);
            this.txtNombreAuxiliar.TabIndex = 1;
            this.txtNombreAuxiliar.TextChanged += new System.EventHandler(this.txtNombreAuxiliar_TextChanged);
            this.txtNombreAuxiliar.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtNombreAuxiliar_KeyPress);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(297, 72);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(100, 13);
            this.label6.TabIndex = 35;
            this.label6.Text = "Nombre del Auxiliar:";
            // 
            // txtCodigoAuxiliar
            // 
            this.txtCodigoAuxiliar.Location = new System.Drawing.Point(127, 69);
            this.txtCodigoAuxiliar.MaxLength = 5;
            this.txtCodigoAuxiliar.Name = "txtCodigoAuxiliar";
            this.txtCodigoAuxiliar.Size = new System.Drawing.Size(164, 20);
            this.txtCodigoAuxiliar.TabIndex = 0;
            this.txtCodigoAuxiliar.TextChanged += new System.EventHandler(this.txtCodigoAuxiliar_TextChanged);
            this.txtCodigoAuxiliar.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCodigoAuxiliar_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 72);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 13);
            this.label1.TabIndex = 37;
            this.label1.Text = "Código del Auxiliar:";
            // 
            // chkManejaDocumentos
            // 
            this.chkManejaDocumentos.AutoSize = true;
            this.chkManejaDocumentos.Location = new System.Drawing.Point(300, 117);
            this.chkManejaDocumentos.Name = "chkManejaDocumentos";
            this.chkManejaDocumentos.Size = new System.Drawing.Size(124, 17);
            this.chkManejaDocumentos.TabIndex = 8;
            this.chkManejaDocumentos.Text = "Maneja Documentos";
            this.chkManejaDocumentos.UseVisualStyleBackColor = true;
            this.chkManejaDocumentos.Visible = false;
            this.chkManejaDocumentos.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.chkManejaDocumentos_KeyPress);
            // 
            // cmdSalir
            // 
            this.cmdSalir.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdSalir.Location = new System.Drawing.Point(308, 351);
            this.cmdSalir.Name = "cmdSalir";
            this.cmdSalir.Size = new System.Drawing.Size(72, 21);
            this.cmdSalir.TabIndex = 6;
            this.cmdSalir.Text = "Salir";
            this.cmdSalir.UseVisualStyleBackColor = true;
            this.cmdSalir.Click += new System.EventHandler(this.cmdSalir_Click);
            // 
            // cmdAgregar
            // 
            this.cmdAgregar.Location = new System.Drawing.Point(230, 350);
            this.cmdAgregar.Name = "cmdAgregar";
            this.cmdAgregar.Size = new System.Drawing.Size(72, 21);
            this.cmdAgregar.TabIndex = 4;
            this.cmdAgregar.Text = "Agregar";
            this.cmdAgregar.UseVisualStyleBackColor = true;
            this.cmdAgregar.Click += new System.EventHandler(this.cmdAgregar_Click);
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label27.ForeColor = System.Drawing.Color.Red;
            this.label27.Location = new System.Drawing.Point(123, 29);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(324, 24);
            this.label27.TabIndex = 41;
            this.label27.Text = "Los campos en Rojo son Obligatorios";
            // 
            // cmdModificar
            // 
            this.cmdModificar.Location = new System.Drawing.Point(187, 351);
            this.cmdModificar.Name = "cmdModificar";
            this.cmdModificar.Size = new System.Drawing.Size(72, 21);
            this.cmdModificar.TabIndex = 5;
            this.cmdModificar.Text = "Modificar";
            this.cmdModificar.UseVisualStyleBackColor = true;
            this.cmdModificar.Click += new System.EventHandler(this.cmdModificar_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 117);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 13);
            this.label2.TabIndex = 43;
            this.label2.Text = "Monto Apertura:";
            // 
            // txtMontoApertura
            // 
            this.txtMontoApertura.Location = new System.Drawing.Point(127, 114);
            this.txtMontoApertura.Name = "txtMontoApertura";
            this.txtMontoApertura.Size = new System.Drawing.Size(164, 20);
            this.txtMontoApertura.TabIndex = 2;
            this.txtMontoApertura.Text = "0";
            this.txtMontoApertura.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtMontoApertura_KeyPress);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToOrderColumns = true;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2});
            this.dataGridView1.Location = new System.Drawing.Point(15, 185);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(564, 139);
            this.dataGridView1.TabIndex = 45;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Status";
            this.Column1.Name = "Column1";
            this.Column1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Column1.Width = 244;
            // 
            // Column2
            // 
            this.Column2.DataPropertyName = "empre_nombre";
            this.Column2.HeaderText = "Nombre Compañia";
            this.Column2.Name = "Column2";
            this.Column2.Width = 244;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.Color.Red;
            this.label11.Location = new System.Drawing.Point(54, 149);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(470, 24);
            this.label11.TabIndex = 46;
            this.label11.Text = "Indique las compañias que no Procesaran esta Auxiliar";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Highlight;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.rbtEliminar);
            this.panel1.Controls.Add(this.rbtModificar);
            this.panel1.Controls.Add(this.cmdCancelarSupervicion);
            this.panel1.Controls.Add(this.cmdAceptarSupervicion);
            this.panel1.Controls.Add(this.txtClave);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Location = new System.Drawing.Point(196, 149);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(212, 98);
            this.panel1.TabIndex = 68;
            this.panel1.Visible = false;
            // 
            // rbtEliminar
            // 
            this.rbtEliminar.AutoSize = true;
            this.rbtEliminar.Location = new System.Drawing.Point(-22, 33);
            this.rbtEliminar.Name = "rbtEliminar";
            this.rbtEliminar.Size = new System.Drawing.Size(73, 17);
            this.rbtEliminar.TabIndex = 72;
            this.rbtEliminar.TabStop = true;
            this.rbtEliminar.Text = "rbtEliminar";
            this.rbtEliminar.UseVisualStyleBackColor = true;
            this.rbtEliminar.Visible = false;
            // 
            // rbtModificar
            // 
            this.rbtModificar.AutoSize = true;
            this.rbtModificar.Location = new System.Drawing.Point(171, 33);
            this.rbtModificar.Name = "rbtModificar";
            this.rbtModificar.Size = new System.Drawing.Size(80, 17);
            this.rbtModificar.TabIndex = 69;
            this.rbtModificar.TabStop = true;
            this.rbtModificar.Text = "rbtModificar";
            this.rbtModificar.UseVisualStyleBackColor = true;
            this.rbtModificar.Visible = false;
            // 
            // cmdCancelarSupervicion
            // 
            this.cmdCancelarSupervicion.Location = new System.Drawing.Point(108, 69);
            this.cmdCancelarSupervicion.Name = "cmdCancelarSupervicion";
            this.cmdCancelarSupervicion.Size = new System.Drawing.Size(77, 21);
            this.cmdCancelarSupervicion.TabIndex = 68;
            this.cmdCancelarSupervicion.Text = "Cancelar";
            this.cmdCancelarSupervicion.UseVisualStyleBackColor = true;
            this.cmdCancelarSupervicion.Click += new System.EventHandler(this.cmdCancelarSupervicion_Click);
            // 
            // cmdAceptarSupervicion
            // 
            this.cmdAceptarSupervicion.Location = new System.Drawing.Point(25, 69);
            this.cmdAceptarSupervicion.Name = "cmdAceptarSupervicion";
            this.cmdAceptarSupervicion.Size = new System.Drawing.Size(77, 21);
            this.cmdAceptarSupervicion.TabIndex = 67;
            this.cmdAceptarSupervicion.Text = "Aceptar";
            this.cmdAceptarSupervicion.UseVisualStyleBackColor = true;
            this.cmdAceptarSupervicion.Click += new System.EventHandler(this.cmdAceptarSupervicion_Click);
            // 
            // txtClave
            // 
            this.txtClave.Location = new System.Drawing.Point(55, 43);
            this.txtClave.MaxLength = 12;
            this.txtClave.Name = "txtClave";
            this.txtClave.PasswordChar = '*';
            this.txtClave.Size = new System.Drawing.Size(100, 20);
            this.txtClave.TabIndex = 67;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(42, 14);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(134, 16);
            this.label5.TabIndex = 66;
            this.label5.Text = "Clave Supervisión";
            // 
            // frmAuxiliarContable
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cmdSalir;
            this.ClientSize = new System.Drawing.Size(591, 395);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.txtMontoApertura);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmdModificar);
            this.Controls.Add(this.label27);
            this.Controls.Add(this.cmdSalir);
            this.Controls.Add(this.cmdAgregar);
            this.Controls.Add(this.chkManejaDocumentos);
            this.Controls.Add(this.txtCodigoAuxiliar);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtNombreAuxiliar);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "frmAuxiliarContable";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Lebrun - Auxiliar Contable";
            this.Load += new System.EventHandler(this.frmAuxiliarContable_Load);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmAuxiliarContable_FormClosed);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmAuxiliarContable_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtNombreAuxiliar;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtCodigoAuxiliar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chkManejaDocumentos;
        private System.Windows.Forms.Button cmdSalir;
        private System.Windows.Forms.Button cmdAgregar;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Button cmdModificar;
        public System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtMontoApertura;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton rbtEliminar;
        private System.Windows.Forms.RadioButton rbtModificar;
        private System.Windows.Forms.Button cmdCancelarSupervicion;
        private System.Windows.Forms.Button cmdAceptarSupervicion;
        private System.Windows.Forms.TextBox txtClave;
        private System.Windows.Forms.Label label5;
    }
}