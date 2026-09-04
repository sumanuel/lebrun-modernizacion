namespace lebrun.formularios.contabilidad
{
    partial class frmComprobanteContable
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.txtCredito = new System.Windows.Forms.TextBox();
            this.txtDebito = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.button4 = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.colItem = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPlanC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAuxiliar = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDocumento = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colReferencia = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDescripcion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colConcepto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDebito = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCreditos = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCodPlanC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colFechaTrans = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colutMod = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ultUsuariomod = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cmdEliminarItem = new System.Windows.Forms.Button();
            this.cmdModificarItem = new System.Windows.Forms.Button();
            this.cmdAgregarItem = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.dateTimePicker3 = new System.Windows.Forms.DateTimePicker();
            this.label9 = new System.Windows.Forms.Label();
            this.dateTimePicker4 = new System.Windows.Forms.DateTimePicker();
            this.label10 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.txtDescripcion = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cmdBuscarPlanC = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.txtReferencia = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtMontoAsiento = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtConcepto = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.rbtCredito = new System.Windows.Forms.RadioButton();
            this.rbtDebito = new System.Windows.Forms.RadioButton();
            this.label16 = new System.Windows.Forms.Label();
            this.txtNroDocumento = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtNombreAuxiliar = new System.Windows.Forms.TextBox();
            this.txtNombreCuenta = new System.Windows.Forms.TextBox();
            this.btAuxiliar = new System.Windows.Forms.Button();
            this.txtAuxiliar = new System.Windows.Forms.TextBox();
            this.txtPlanCuentas = new System.Windows.Forms.TextBox();
            this.cmdGuardar = new System.Windows.Forms.Button();
            this.cmdCierreDefinitivo = new System.Windows.Forms.Button();
            this.btGuardarModificar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtCredito
            // 
            this.txtCredito.BackColor = System.Drawing.SystemColors.HotTrack;
            this.txtCredito.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCredito.ForeColor = System.Drawing.Color.White;
            this.txtCredito.Location = new System.Drawing.Point(684, 543);
            this.txtCredito.Name = "txtCredito";
            this.txtCredito.ReadOnly = true;
            this.txtCredito.Size = new System.Drawing.Size(152, 26);
            this.txtCredito.TabIndex = 48;
            this.txtCredito.Text = "0";
            this.txtCredito.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtDebito
            // 
            this.txtDebito.BackColor = System.Drawing.SystemColors.HotTrack;
            this.txtDebito.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDebito.ForeColor = System.Drawing.SystemColors.Window;
            this.txtDebito.Location = new System.Drawing.Point(523, 543);
            this.txtDebito.Name = "txtDebito";
            this.txtDebito.ReadOnly = true;
            this.txtDebito.Size = new System.Drawing.Size(152, 26);
            this.txtDebito.TabIndex = 53;
            this.txtDebito.Text = "0";
            this.txtDebito.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(681, 527);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(43, 13);
            this.label7.TabIndex = 48;
            this.label7.Text = "Credito:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(520, 527);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(41, 13);
            this.label8.TabIndex = 54;
            this.label8.Text = "Debito:";
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(772, 575);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(72, 21);
            this.button4.TabIndex = 6;
            this.button4.Text = "Salir";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colItem,
            this.colPlanC,
            this.colAuxiliar,
            this.colDocumento,
            this.colReferencia,
            this.colDescripcion,
            this.colConcepto,
            this.colDebito,
            this.colCreditos,
            this.colCodPlanC,
            this.colFechaTrans,
            this.colutMod,
            this.ultUsuariomod});
            this.dataGridView1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dataGridView1.Location = new System.Drawing.Point(22, 291);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.Size = new System.Drawing.Size(814, 233);
            this.dataGridView1.TabIndex = 55;
            // 
            // colItem
            // 
            this.colItem.HeaderText = "#";
            this.colItem.Name = "colItem";
            this.colItem.ReadOnly = true;
            this.colItem.Width = 20;
            // 
            // colPlanC
            // 
            this.colPlanC.HeaderText = "Plan de Cuentas";
            this.colPlanC.Name = "colPlanC";
            this.colPlanC.ReadOnly = true;
            // 
            // colAuxiliar
            // 
            this.colAuxiliar.HeaderText = "Auxiliar";
            this.colAuxiliar.Name = "colAuxiliar";
            this.colAuxiliar.ReadOnly = true;
            // 
            // colDocumento
            // 
            this.colDocumento.HeaderText = "Docum";
            this.colDocumento.Name = "colDocumento";
            this.colDocumento.ReadOnly = true;
            // 
            // colReferencia
            // 
            this.colReferencia.HeaderText = "Referencia";
            this.colReferencia.Name = "colReferencia";
            this.colReferencia.ReadOnly = true;
            // 
            // colDescripcion
            // 
            this.colDescripcion.HeaderText = "Descripcion";
            this.colDescripcion.Name = "colDescripcion";
            this.colDescripcion.ReadOnly = true;
            this.colDescripcion.Visible = false;
            // 
            // colConcepto
            // 
            this.colConcepto.HeaderText = "Concepto";
            this.colConcepto.Name = "colConcepto";
            this.colConcepto.ReadOnly = true;
            // 
            // colDebito
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.BottomRight;
            this.colDebito.DefaultCellStyle = dataGridViewCellStyle1;
            this.colDebito.HeaderText = "Debito";
            this.colDebito.Name = "colDebito";
            this.colDebito.ReadOnly = true;
            // 
            // colCreditos
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.BottomRight;
            this.colCreditos.DefaultCellStyle = dataGridViewCellStyle2;
            this.colCreditos.HeaderText = "Credito";
            this.colCreditos.Name = "colCreditos";
            this.colCreditos.ReadOnly = true;
            // 
            // colCodPlanC
            // 
            this.colCodPlanC.HeaderText = "codigo planCuentas";
            this.colCodPlanC.Name = "colCodPlanC";
            this.colCodPlanC.ReadOnly = true;
            this.colCodPlanC.Visible = false;
            // 
            // colFechaTrans
            // 
            this.colFechaTrans.HeaderText = "fecha Transaccion";
            this.colFechaTrans.Name = "colFechaTrans";
            this.colFechaTrans.ReadOnly = true;
            this.colFechaTrans.Visible = false;
            // 
            // colutMod
            // 
            this.colutMod.HeaderText = "ultimaModificacion";
            this.colutMod.Name = "colutMod";
            this.colutMod.ReadOnly = true;
            this.colutMod.Visible = false;
            // 
            // ultUsuariomod
            // 
            this.ultUsuariomod.HeaderText = "UltimoUsuarioModificacion";
            this.ultUsuariomod.Name = "ultUsuariomod";
            this.ultUsuariomod.ReadOnly = true;
            this.ultUsuariomod.Visible = false;
            // 
            // cmdEliminarItem
            // 
            this.cmdEliminarItem.Location = new System.Drawing.Point(759, 264);
            this.cmdEliminarItem.Name = "cmdEliminarItem";
            this.cmdEliminarItem.Size = new System.Drawing.Size(77, 21);
            this.cmdEliminarItem.TabIndex = 3;
            this.cmdEliminarItem.Text = "Eliminar";
            this.cmdEliminarItem.UseVisualStyleBackColor = true;
            this.cmdEliminarItem.Click += new System.EventHandler(this.cmdEliminarItem_Click);
            // 
            // cmdModificarItem
            // 
            this.cmdModificarItem.Location = new System.Drawing.Point(681, 264);
            this.cmdModificarItem.Name = "cmdModificarItem";
            this.cmdModificarItem.Size = new System.Drawing.Size(72, 21);
            this.cmdModificarItem.TabIndex = 2;
            this.cmdModificarItem.Text = "Modificar";
            this.cmdModificarItem.UseVisualStyleBackColor = true;
            this.cmdModificarItem.Click += new System.EventHandler(this.cmdModificarItem_Click);
            // 
            // cmdAgregarItem
            // 
            this.cmdAgregarItem.Location = new System.Drawing.Point(598, 264);
            this.cmdAgregarItem.Name = "cmdAgregarItem";
            this.cmdAgregarItem.Size = new System.Drawing.Size(77, 21);
            this.cmdAgregarItem.TabIndex = 1;
            this.cmdAgregarItem.Text = "Agregar";
            this.cmdAgregarItem.UseVisualStyleBackColor = true;
            this.cmdAgregarItem.Click += new System.EventHandler(this.cmdAgregarItem_Click);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.BackColor = System.Drawing.SystemColors.Highlight;
            this.label11.Location = new System.Drawing.Point(588, 27);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(65, 13);
            this.label11.TabIndex = 68;
            this.label11.Text = "Contabilidad";
            // 
            // dateTimePicker3
            // 
            this.dateTimePicker3.Enabled = false;
            this.dateTimePicker3.Location = new System.Drawing.Point(397, 57);
            this.dateTimePicker3.Name = "dateTimePicker3";
            this.dateTimePicker3.Size = new System.Drawing.Size(194, 20);
            this.dateTimePicker3.TabIndex = 3;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(320, 59);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(68, 13);
            this.label9.TabIndex = 67;
            this.label9.Text = "Fecha Carga";
            // 
            // dateTimePicker4
            // 
            this.dateTimePicker4.Location = new System.Drawing.Point(97, 57);
            this.dateTimePicker4.Name = "dateTimePicker4";
            this.dateTimePicker4.Size = new System.Drawing.Size(200, 20);
            this.dateTimePicker4.TabIndex = 2;
            this.dateTimePicker4.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dateTimePicker4_KeyPress);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(20, 59);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(70, 13);
            this.label10.TabIndex = 66;
            this.label10.Text = "Fecha Comp.";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(515, 27);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(53, 13);
            this.label12.TabIndex = 65;
            this.label12.Text = "Id. Origen";
            // 
            // txtDescripcion
            // 
            this.txtDescripcion.Location = new System.Drawing.Point(345, 24);
            this.txtDescripcion.MaxLength = 100;
            this.txtDescripcion.Name = "txtDescripcion";
            this.txtDescripcion.Size = new System.Drawing.Size(152, 20);
            this.txtDescripcion.TabIndex = 1;
            this.txtDescripcion.Leave += new System.EventHandler(this.txtDescripcion_Leave);
            this.txtDescripcion.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.MyTextBox_KeyPress);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(269, 27);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(63, 13);
            this.label13.TabIndex = 64;
            this.label13.Text = "Descripción";
            // 
            // textBox5
            // 
            this.textBox5.Enabled = false;
            this.textBox5.Location = new System.Drawing.Point(96, 24);
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new System.Drawing.Size(152, 20);
            this.textBox5.TabIndex = 0;
            this.textBox5.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(20, 28);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(70, 13);
            this.label14.TabIndex = 63;
            this.label14.Text = "Comprobante";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(11, 27);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(38, 13);
            this.label3.TabIndex = 72;
            this.label3.Text = "Plan C";
            // 
            // cmdBuscarPlanC
            // 
            this.cmdBuscarPlanC.Location = new System.Drawing.Point(213, 22);
            this.cmdBuscarPlanC.Name = "cmdBuscarPlanC";
            this.cmdBuscarPlanC.Size = new System.Drawing.Size(72, 21);
            this.cmdBuscarPlanC.TabIndex = 73;
            this.cmdBuscarPlanC.Text = "Buscar";
            this.cmdBuscarPlanC.UseVisualStyleBackColor = true;
            this.cmdBuscarPlanC.Click += new System.EventHandler(this.cmdBuscarPlanC_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(14, 57);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(40, 13);
            this.label4.TabIndex = 75;
            this.label4.Text = "Auxiliar";
            // 
            // txtReferencia
            // 
            this.txtReferencia.Location = new System.Drawing.Point(85, 89);
            this.txtReferencia.MaxLength = 20;
            this.txtReferencia.Name = "txtReferencia";
            this.txtReferencia.Size = new System.Drawing.Size(152, 20);
            this.txtReferencia.TabIndex = 3;
            this.txtReferencia.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.MyTextBox_KeyPress);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(20, 92);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(59, 13);
            this.label5.TabIndex = 77;
            this.label5.Text = "Referencia";
            // 
            // txtMontoAsiento
            // 
            this.txtMontoAsiento.Location = new System.Drawing.Point(639, 118);
            this.txtMontoAsiento.Name = "txtMontoAsiento";
            this.txtMontoAsiento.Size = new System.Drawing.Size(160, 20);
            this.txtMontoAsiento.TabIndex = 7;
            this.txtMontoAsiento.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtNroDocumento_KeyPress);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(559, 57);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(82, 13);
            this.label6.TabIndex = 79;
            this.label6.Text = "Nro Documento";
            // 
            // txtConcepto
            // 
            this.txtConcepto.Location = new System.Drawing.Point(323, 89);
            this.txtConcepto.MaxLength = 100;
            this.txtConcepto.Name = "txtConcepto";
            this.txtConcepto.Size = new System.Drawing.Size(233, 20);
            this.txtConcepto.TabIndex = 4;
            this.txtConcepto.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.MyTextBox_KeyPress);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(264, 92);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(53, 13);
            this.label15.TabIndex = 81;
            this.label15.Text = "Concepto";
            // 
            // rbtCredito
            // 
            this.rbtCredito.AutoSize = true;
            this.rbtCredito.Location = new System.Drawing.Point(743, 90);
            this.rbtCredito.Name = "rbtCredito";
            this.rbtCredito.Size = new System.Drawing.Size(58, 17);
            this.rbtCredito.TabIndex = 6;
            this.rbtCredito.TabStop = true;
            this.rbtCredito.Text = "Crédito";
            this.rbtCredito.UseVisualStyleBackColor = true;
            this.rbtCredito.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.rbtCredito_KeyPress);
            this.rbtCredito.CheckedChanged += new System.EventHandler(this.rbtCredito_CheckedChanged);
            // 
            // rbtDebito
            // 
            this.rbtDebito.AutoSize = true;
            this.rbtDebito.Location = new System.Drawing.Point(668, 90);
            this.rbtDebito.Name = "rbtDebito";
            this.rbtDebito.Size = new System.Drawing.Size(56, 17);
            this.rbtDebito.TabIndex = 5;
            this.rbtDebito.TabStop = true;
            this.rbtDebito.Text = "Débito";
            this.rbtDebito.UseVisualStyleBackColor = true;
            this.rbtDebito.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.rbtDebito_KeyPress);
            this.rbtDebito.Enter += new System.EventHandler(this.rbtDebito_Enter);
            this.rbtDebito.CheckedChanged += new System.EventHandler(this.rbtDebito_CheckedChanged);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(559, 92);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(100, 13);
            this.label16.TabIndex = 84;
            this.label16.Text = "Tipo de Movimiento";
            // 
            // txtNroDocumento
            // 
            this.txtNroDocumento.Enabled = false;
            this.txtNroDocumento.Location = new System.Drawing.Point(647, 54);
            this.txtNroDocumento.Name = "txtNroDocumento";
            this.txtNroDocumento.Size = new System.Drawing.Size(152, 20);
            this.txtNroDocumento.TabIndex = 2;
            this.txtNroDocumento.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.MyTextBox_KeyPress);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(553, 121);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(75, 13);
            this.label17.TabIndex = 86;
            this.label17.Text = "Monto Asiento";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.dateTimePicker3);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.dateTimePicker4);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.txtDescripcion);
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.textBox5);
            this.groupBox1.Controls.Add(this.label14);
            this.groupBox1.Location = new System.Drawing.Point(22, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(811, 92);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Cabecera";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtNombreAuxiliar);
            this.groupBox2.Controls.Add(this.txtNombreCuenta);
            this.groupBox2.Controls.Add(this.btAuxiliar);
            this.groupBox2.Controls.Add(this.txtAuxiliar);
            this.groupBox2.Controls.Add(this.txtPlanCuentas);
            this.groupBox2.Controls.Add(this.txtNroDocumento);
            this.groupBox2.Controls.Add(this.label17);
            this.groupBox2.Controls.Add(this.label16);
            this.groupBox2.Controls.Add(this.rbtDebito);
            this.groupBox2.Controls.Add(this.rbtCredito);
            this.groupBox2.Controls.Add(this.txtConcepto);
            this.groupBox2.Controls.Add(this.label15);
            this.groupBox2.Controls.Add(this.txtMontoAsiento);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.txtReferencia);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.cmdBuscarPlanC);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Location = new System.Drawing.Point(22, 110);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(811, 148);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Detalle";
            // 
            // txtNombreAuxiliar
            // 
            this.txtNombreAuxiliar.BackColor = System.Drawing.SystemColors.Desktop;
            this.txtNombreAuxiliar.ForeColor = System.Drawing.SystemColors.Window;
            this.txtNombreAuxiliar.Location = new System.Drawing.Point(229, 54);
            this.txtNombreAuxiliar.Name = "txtNombreAuxiliar";
            this.txtNombreAuxiliar.ReadOnly = true;
            this.txtNombreAuxiliar.Size = new System.Drawing.Size(324, 20);
            this.txtNombreAuxiliar.TabIndex = 91;
            this.txtNombreAuxiliar.Text = "Nombre Auxiliar";
            // 
            // txtNombreCuenta
            // 
            this.txtNombreCuenta.BackColor = System.Drawing.SystemColors.Desktop;
            this.txtNombreCuenta.ForeColor = System.Drawing.SystemColors.Window;
            this.txtNombreCuenta.Location = new System.Drawing.Point(300, 22);
            this.txtNombreCuenta.Name = "txtNombreCuenta";
            this.txtNombreCuenta.ReadOnly = true;
            this.txtNombreCuenta.Size = new System.Drawing.Size(390, 20);
            this.txtNombreCuenta.TabIndex = 90;
            this.txtNombreCuenta.Text = "Nombre Plan Cuenta";
            // 
            // btAuxiliar
            // 
            this.btAuxiliar.Enabled = false;
            this.btAuxiliar.Location = new System.Drawing.Point(135, 54);
            this.btAuxiliar.Name = "btAuxiliar";
            this.btAuxiliar.Size = new System.Drawing.Size(72, 21);
            this.btAuxiliar.TabIndex = 88;
            this.btAuxiliar.Text = "Buscar Aux";
            this.btAuxiliar.UseVisualStyleBackColor = true;
            this.btAuxiliar.Click += new System.EventHandler(this.btAuxiliar_Click);
            // 
            // txtAuxiliar
            // 
            this.txtAuxiliar.Enabled = false;
            this.txtAuxiliar.Location = new System.Drawing.Point(55, 54);
            this.txtAuxiliar.MaxLength = 5;
            this.txtAuxiliar.Name = "txtAuxiliar";
            this.txtAuxiliar.Size = new System.Drawing.Size(70, 20);
            this.txtAuxiliar.TabIndex = 87;
            this.txtAuxiliar.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtAuxiliar_KeyPress);
            // 
            // txtPlanCuentas
            // 
            this.txtPlanCuentas.Location = new System.Drawing.Point(55, 23);
            this.txtPlanCuentas.Name = "txtPlanCuentas";
            this.txtPlanCuentas.Size = new System.Drawing.Size(152, 20);
            this.txtPlanCuentas.TabIndex = 0;
            this.txtPlanCuentas.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPlanCuentas_KeyPress);
            // 
            // cmdGuardar
            // 
            this.cmdGuardar.Location = new System.Drawing.Point(566, 576);
            this.cmdGuardar.Name = "cmdGuardar";
            this.cmdGuardar.Size = new System.Drawing.Size(72, 21);
            this.cmdGuardar.TabIndex = 4;
            this.cmdGuardar.Text = "Guardar";
            this.cmdGuardar.UseVisualStyleBackColor = true;
            this.cmdGuardar.Click += new System.EventHandler(this.cmdGuardar_Click);
            // 
            // cmdCierreDefinitivo
            // 
            this.cmdCierreDefinitivo.Location = new System.Drawing.Point(656, 575);
            this.cmdCierreDefinitivo.Name = "cmdCierreDefinitivo";
            this.cmdCierreDefinitivo.Size = new System.Drawing.Size(97, 21);
            this.cmdCierreDefinitivo.TabIndex = 5;
            this.cmdCierreDefinitivo.Text = "Cerrar Definitivo";
            this.cmdCierreDefinitivo.UseVisualStyleBackColor = true;
            this.cmdCierreDefinitivo.Click += new System.EventHandler(this.cmdCierreDefinitivo_Click);
            // 
            // btGuardarModificar
            // 
            this.btGuardarModificar.Location = new System.Drawing.Point(523, 576);
            this.btGuardarModificar.Name = "btGuardarModificar";
            this.btGuardarModificar.Size = new System.Drawing.Size(72, 21);
            this.btGuardarModificar.TabIndex = 56;
            this.btGuardarModificar.Text = "Guardar";
            this.btGuardarModificar.UseVisualStyleBackColor = true;
            this.btGuardarModificar.Visible = false;
            this.btGuardarModificar.Click += new System.EventHandler(this.btGuardarModificar_Click);
            // 
            // frmComprobanteContable
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(862, 608);
            this.Controls.Add(this.btGuardarModificar);
            this.Controls.Add(this.cmdCierreDefinitivo);
            this.Controls.Add(this.cmdGuardar);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.cmdEliminarItem);
            this.Controls.Add(this.cmdModificarItem);
            this.Controls.Add(this.cmdAgregarItem);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtDebito);
            this.Controls.Add(this.txtCredito);
            this.Name = "frmComprobanteContable";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.ComprobanteContable_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtCredito;
        private System.Windows.Forms.TextBox txtDebito;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button cmdEliminarItem;
        private System.Windows.Forms.Button cmdModificarItem;
        private System.Windows.Forms.Button cmdAgregarItem;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.DateTimePicker dateTimePicker3;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.DateTimePicker dateTimePicker4;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtDescripcion;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button cmdBuscarPlanC;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtReferencia;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtMontoAsiento;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtConcepto;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.RadioButton rbtCredito;
        private System.Windows.Forms.RadioButton rbtDebito;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox txtNroDocumento;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        public System.Windows.Forms.TextBox txtPlanCuentas;
        private System.Windows.Forms.Button cmdGuardar;
        private System.Windows.Forms.Button cmdCierreDefinitivo;
        private System.Windows.Forms.Button btGuardarModificar;
        private System.Windows.Forms.DataGridViewTextBoxColumn colItem;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPlanC;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAuxiliar;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDocumento;
        private System.Windows.Forms.DataGridViewTextBoxColumn colReferencia;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDescripcion;
        private System.Windows.Forms.DataGridViewTextBoxColumn colConcepto;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDebito;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCreditos;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCodPlanC;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFechaTrans;
        private System.Windows.Forms.DataGridViewTextBoxColumn colutMod;
        private System.Windows.Forms.DataGridViewTextBoxColumn ultUsuariomod;
        private System.Windows.Forms.Button btAuxiliar;
        internal System.Windows.Forms.TextBox txtAuxiliar;
        internal System.Windows.Forms.TextBox txtNombreCuenta;
        internal System.Windows.Forms.TextBox txtNombreAuxiliar;

    }
}