namespace lebrun.formularios.contabilidad
{
    partial class frmPlanCuentas
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
            this.cboGrupo = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cboSubGrupo = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cboSubCuenta = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtNombreCuenta = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.rbtEliminar = new System.Windows.Forms.RadioButton();
            this.rbtModificar = new System.Windows.Forms.RadioButton();
            this.cmdCancelarSupervicion = new System.Windows.Forms.Button();
            this.cmdAceptarSupervicion = new System.Windows.Forms.Button();
            this.txtClave = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtAuxiliar = new System.Windows.Forms.TextBox();
            this.btnModificar = new System.Windows.Forms.Button();
            this.chkAuxiliar = new System.Windows.Forms.CheckBox();
            this.label27 = new System.Windows.Forms.Label();
            this.txtInicialEjercicio = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.cboCuenta = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.cmdSalir = new System.Windows.Forms.Button();
            this.cmdAgregar = new System.Windows.Forms.Button();
            this.chkImprimeBalance = new System.Windows.Forms.CheckBox();
            this.chckImprimeDiario = new System.Windows.Forms.CheckBox();
            this.label24 = new System.Windows.Forms.Label();
            this.txtSiglas = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // cboGrupo
            // 
            this.cboGrupo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboGrupo.FormattingEnabled = true;
            this.cboGrupo.Location = new System.Drawing.Point(72, 49);
            this.cboGrupo.Name = "cboGrupo";
            this.cboGrupo.Size = new System.Drawing.Size(49, 21);
            this.cboGrupo.TabIndex = 0;
            this.cboGrupo.SelectedIndexChanged += new System.EventHandler(this.cboGrupo_SelectedIndexChanged);
            this.cboGrupo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cboGrupo_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 52);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Grupo:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(149, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Sub Grupo:";
            // 
            // cboSubGrupo
            // 
            this.cboSubGrupo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSubGrupo.FormattingEnabled = true;
            this.cboSubGrupo.Location = new System.Drawing.Point(216, 49);
            this.cboSubGrupo.Name = "cboSubGrupo";
            this.cboSubGrupo.Size = new System.Drawing.Size(57, 21);
            this.cboSubGrupo.TabIndex = 1;
            this.cboSubGrupo.SelectedIndexChanged += new System.EventHandler(this.cboSubGrupo_SelectedIndexChanged);
            this.cboSubGrupo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cboSubGrupo_KeyPress);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(18, 99);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(66, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Sub Cuenta:";
            // 
            // cboSubCuenta
            // 
            this.cboSubCuenta.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSubCuenta.FormattingEnabled = true;
            this.cboSubCuenta.Location = new System.Drawing.Point(90, 96);
            this.cboSubCuenta.Name = "cboSubCuenta";
            this.cboSubCuenta.Size = new System.Drawing.Size(69, 21);
            this.cboSubCuenta.TabIndex = 3;
            this.cboSubCuenta.SelectedIndexChanged += new System.EventHandler(this.cboSubCuenta_SelectedIndexChanged);
            this.cboSubCuenta.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cboSubCuenta_KeyPress);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(182, 100);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(55, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Auxiliar 1 :";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(18, 141);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(109, 13);
            this.label6.TabIndex = 10;
            this.label6.Text = "Nombre de la cuenta:";
            // 
            // txtNombreCuenta
            // 
            this.txtNombreCuenta.Location = new System.Drawing.Point(133, 138);
            this.txtNombreCuenta.Name = "txtNombreCuenta";
            this.txtNombreCuenta.Size = new System.Drawing.Size(228, 20);
            this.txtNombreCuenta.TabIndex = 6;
            this.txtNombreCuenta.TextChanged += new System.EventHandler(this.txtNombreCuenta_TextChanged);
            this.txtNombreCuenta.Validated += new System.EventHandler(this.txtNombreCuenta_Validated);
            this.txtNombreCuenta.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.MyTextBox_KeyPress);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.panel1);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.dataGridView1);
            this.groupBox1.Controls.Add(this.txtAuxiliar);
            this.groupBox1.Controls.Add(this.btnModificar);
            this.groupBox1.Controls.Add(this.chkAuxiliar);
            this.groupBox1.Controls.Add(this.label27);
            this.groupBox1.Controls.Add(this.txtInicialEjercicio);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.cboCuenta);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.cmdSalir);
            this.groupBox1.Controls.Add(this.cmdAgregar);
            this.groupBox1.Controls.Add(this.chkImprimeBalance);
            this.groupBox1.Controls.Add(this.chckImprimeDiario);
            this.groupBox1.Controls.Add(this.label24);
            this.groupBox1.Controls.Add(this.txtSiglas);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.txtNombreCuenta);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.cboSubCuenta);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.cboSubGrupo);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.cboGrupo);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(574, 435);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Datos Plan de Cuentas";
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
            this.panel1.Location = new System.Drawing.Point(185, 179);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(212, 98);
            this.panel1.TabIndex = 67;
            this.panel1.Visible = false;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
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
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.Color.Red;
            this.label11.Location = new System.Drawing.Point(55, 215);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(465, 24);
            this.label11.TabIndex = 39;
            this.label11.Text = "Indique las compañias que no Procesaran esta cuenta";
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
            this.dataGridView1.Location = new System.Drawing.Point(21, 251);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(531, 139);
            this.dataGridView1.TabIndex = 38;
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
            // txtAuxiliar
            // 
            this.txtAuxiliar.Enabled = false;
            this.txtAuxiliar.Location = new System.Drawing.Point(242, 97);
            this.txtAuxiliar.MaxLength = 5;
            this.txtAuxiliar.Name = "txtAuxiliar";
            this.txtAuxiliar.Size = new System.Drawing.Size(83, 20);
            this.txtAuxiliar.TabIndex = 4;
            this.txtAuxiliar.Leave += new System.EventHandler(this.txtAuxiliar_Leave);
            this.txtAuxiliar.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtAuxiliar_KeyPress);
            // 
            // btnModificar
            // 
            this.btnModificar.Location = new System.Drawing.Point(164, 408);
            this.btnModificar.Name = "btnModificar";
            this.btnModificar.Size = new System.Drawing.Size(72, 21);
            this.btnModificar.TabIndex = 16;
            this.btnModificar.Text = "Modificar";
            this.btnModificar.UseVisualStyleBackColor = true;
            this.btnModificar.Click += new System.EventHandler(this.btnModificar_Click);
            // 
            // chkAuxiliar
            // 
            this.chkAuxiliar.AutoSize = true;
            this.chkAuxiliar.Enabled = false;
            this.chkAuxiliar.Location = new System.Drawing.Point(455, 179);
            this.chkAuxiliar.Name = "chkAuxiliar";
            this.chkAuxiliar.Size = new System.Drawing.Size(97, 17);
            this.chkAuxiliar.TabIndex = 11;
            this.chkAuxiliar.Text = "Maneja Auxiliar";
            this.chkAuxiliar.UseVisualStyleBackColor = true;
            this.chkAuxiliar.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.MyTextBox_KeyPress);
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label27.ForeColor = System.Drawing.Color.Red;
            this.label27.Location = new System.Drawing.Point(129, 16);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(324, 24);
            this.label27.TabIndex = 34;
            this.label27.Text = "Los campos en Rojo son Obligatorios";
            // 
            // txtInicialEjercicio
            // 
            this.txtInicialEjercicio.Enabled = false;
            this.txtInicialEjercicio.Location = new System.Drawing.Point(134, 179);
            this.txtInicialEjercicio.Name = "txtInicialEjercicio";
            this.txtInicialEjercicio.Size = new System.Drawing.Size(89, 20);
            this.txtInicialEjercicio.TabIndex = 8;
            this.txtInicialEjercicio.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtInicialEjercicio.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtInicialEjercicio_KeyPress);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(303, 52);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(44, 13);
            this.label10.TabIndex = 33;
            this.label10.Text = "Cuenta:";
            // 
            // cboCuenta
            // 
            this.cboCuenta.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCuenta.FormattingEnabled = true;
            this.cboCuenta.Location = new System.Drawing.Point(353, 49);
            this.cboCuenta.Name = "cboCuenta";
            this.cboCuenta.Size = new System.Drawing.Size(57, 21);
            this.cboCuenta.TabIndex = 2;
            this.cboCuenta.SelectedIndexChanged += new System.EventHandler(this.cboCuenta_SelectedIndexChanged);
            this.cboCuenta.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cboCuenta_KeyPress);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(436, 95);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(0, 20);
            this.label9.TabIndex = 31;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(364, 100);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(66, 13);
            this.label8.TabIndex = 30;
            this.label8.Text = "Vista Previa:";
            // 
            // cmdSalir
            // 
            this.cmdSalir.Location = new System.Drawing.Point(289, 408);
            this.cmdSalir.Name = "cmdSalir";
            this.cmdSalir.Size = new System.Drawing.Size(72, 21);
            this.cmdSalir.TabIndex = 15;
            this.cmdSalir.Text = "Salir";
            this.cmdSalir.UseVisualStyleBackColor = true;
            this.cmdSalir.Click += new System.EventHandler(this.cmdSalir_Click);
            // 
            // cmdAgregar
            // 
            this.cmdAgregar.Location = new System.Drawing.Point(201, 408);
            this.cmdAgregar.Name = "cmdAgregar";
            this.cmdAgregar.Size = new System.Drawing.Size(72, 21);
            this.cmdAgregar.TabIndex = 13;
            this.cmdAgregar.Text = "Agregar";
            this.cmdAgregar.UseVisualStyleBackColor = true;
            this.cmdAgregar.Click += new System.EventHandler(this.cmdAgregar_Click);
            // 
            // chkImprimeBalance
            // 
            this.chkImprimeBalance.AutoSize = true;
            this.chkImprimeBalance.Enabled = false;
            this.chkImprimeBalance.Location = new System.Drawing.Point(345, 179);
            this.chkImprimeBalance.Name = "chkImprimeBalance";
            this.chkImprimeBalance.Size = new System.Drawing.Size(104, 17);
            this.chkImprimeBalance.TabIndex = 10;
            this.chkImprimeBalance.Text = "Imprime Balance";
            this.chkImprimeBalance.UseVisualStyleBackColor = true;
            this.chkImprimeBalance.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.MyTextBox_KeyPress);
            // 
            // chckImprimeDiario
            // 
            this.chckImprimeDiario.AutoSize = true;
            this.chckImprimeDiario.Location = new System.Drawing.Point(242, 180);
            this.chckImprimeDiario.Name = "chckImprimeDiario";
            this.chckImprimeDiario.Size = new System.Drawing.Size(97, 17);
            this.chckImprimeDiario.TabIndex = 9;
            this.chckImprimeDiario.Text = "Imprime Diarios";
            this.chckImprimeDiario.UseVisualStyleBackColor = true;
            this.chckImprimeDiario.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.MyTextBox_KeyPress);
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(18, 181);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(110, 13);
            this.label24.TabIndex = 25;
            this.label24.Text = "Saldo Inicial Ejercicio:";
            // 
            // txtSiglas
            // 
            this.txtSiglas.Location = new System.Drawing.Point(436, 138);
            this.txtSiglas.MaxLength = 4;
            this.txtSiglas.Name = "txtSiglas";
            this.txtSiglas.Size = new System.Drawing.Size(57, 20);
            this.txtSiglas.TabIndex = 7;
            this.txtSiglas.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.MyTextBox_KeyPress);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(392, 141);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(38, 13);
            this.label7.TabIndex = 12;
            this.label7.Text = "Siglas:";
            // 
            // frmPlanCuentas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(598, 459);
            this.Controls.Add(this.groupBox1);
            this.Name = "frmPlanCuentas";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Lebrun - Plan de Cuentas";
            this.Load += new System.EventHandler(this.frmPlanCuentas_Load);
            this.Activated += new System.EventHandler(this.frmPlanCuentas_Activated);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmPlanCuentas_FormClosed);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmPlanCuentas_FormClosing);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cboGrupo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cboSubCuenta;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtNombreCuenta;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtSiglas;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.CheckBox chkImprimeBalance;
        private System.Windows.Forms.CheckBox chckImprimeDiario;
        private System.Windows.Forms.Button cmdAgregar;
        private System.Windows.Forms.Button cmdSalir;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox cboCuenta;
        private System.Windows.Forms.TextBox txtInicialEjercicio;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Button btnModificar;
        private System.Windows.Forms.ComboBox cboSubGrupo;
        public System.Windows.Forms.CheckBox chkAuxiliar;
        private System.Windows.Forms.TextBox txtAuxiliar;
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