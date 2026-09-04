namespace lebrun.formularios.tesoreria
{
    partial class frmRetIvaCli
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnImportar = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.txtCodigo = new System.Windows.Forms.TextBox();
            this.txtControlFiscal = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.lblNombreCliente = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.fechaRecibido = new System.Windows.Forms.DateTimePicker();
            this.label15 = new System.Windows.Forms.Label();
            this.fecha = new System.Windows.Forms.DateTimePicker();
            this.label14 = new System.Windows.Forms.Label();
            this.txtMontoRet = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.cmbPorcRet = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txtNumRetenc = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnAceptar = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.button2 = new System.Windows.Forms.Button();
            this.txtFecha = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtEstado = new System.Windows.Forms.TextBox();
            this.txtIva = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtBaseImponible = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtExento = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtNeto = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtNumDoc = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtTipoDoc = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label17 = new System.Windows.Forms.Label();
            this.button4 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.lblMontoRetenc = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.btnAuxiliar = new System.Windows.Forms.Button();
            this.txtAuxiliar = new System.Windows.Forms.TextBox();
            this.lblDescripcionCuenta = new System.Windows.Forms.Label();
            this.button5 = new System.Windows.Forms.Button();
            this.txtCuenta = new System.Windows.Forms.TextBox();
            this.btnReimprimir = new System.Windows.Forms.Button();
            this.txtComprobante = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnImportar);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.txtCodigo);
            this.groupBox1.Controls.Add(this.txtControlFiscal);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.lblNombreCliente);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(6, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(762, 64);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Datos";
            // 
            // btnImportar
            // 
            this.btnImportar.Image = global::lebrun.Properties.Resources.Undo_24x24;
            this.btnImportar.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnImportar.Location = new System.Drawing.Point(655, 20);
            this.btnImportar.Name = "btnImportar";
            this.btnImportar.Size = new System.Drawing.Size(88, 32);
            this.btnImportar.TabIndex = 20;
            this.btnImportar.Text = "    Procesar";
            this.btnImportar.UseVisualStyleBackColor = true;
            this.btnImportar.Click += new System.EventHandler(this.btnImportar_Click);
            // 
            // button1
            // 
            this.button1.Image = global::lebrun.Properties.Resources.Search_16x16;
            this.button1.Location = new System.Drawing.Point(181, 26);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(26, 23);
            this.button1.TabIndex = 19;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtCodigo
            // 
            this.txtCodigo.Location = new System.Drawing.Point(94, 28);
            this.txtCodigo.Name = "txtCodigo";
            this.txtCodigo.Size = new System.Drawing.Size(81, 20);
            this.txtCodigo.TabIndex = 7;
            this.txtCodigo.TextChanged += new System.EventHandler(this.txtCodigo_TextChanged);
            this.txtCodigo.Leave += new System.EventHandler(this.txtCodigo_Leave);
            this.txtCodigo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCodigo_KeyPress);
            // 
            // txtControlFiscal
            // 
            this.txtControlFiscal.Location = new System.Drawing.Point(533, 28);
            this.txtControlFiscal.Name = "txtControlFiscal";
            this.txtControlFiscal.Size = new System.Drawing.Size(100, 20);
            this.txtControlFiscal.TabIndex = 18;
            this.txtControlFiscal.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtControlFiscal_KeyPress);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(453, 31);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(74, 13);
            this.label5.TabIndex = 6;
            this.label5.Text = "Numero Fiscal";
            // 
            // lblNombreCliente
            // 
            this.lblNombreCliente.AutoSize = true;
            this.lblNombreCliente.Location = new System.Drawing.Point(225, 30);
            this.lblNombreCliente.Name = "lblNombreCliente";
            this.lblNombreCliente.Size = new System.Drawing.Size(16, 13);
            this.lblNombreCliente.TabIndex = 1;
            this.lblNombreCliente.Text = "...";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Codigo Cliente";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.fechaRecibido);
            this.groupBox2.Controls.Add(this.label15);
            this.groupBox2.Controls.Add(this.fecha);
            this.groupBox2.Controls.Add(this.label14);
            this.groupBox2.Controls.Add(this.txtMontoRet);
            this.groupBox2.Controls.Add(this.label13);
            this.groupBox2.Controls.Add(this.cmbPorcRet);
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Controls.Add(this.txtNumRetenc);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Location = new System.Drawing.Point(5, 199);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(477, 100);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Retencion";
            // 
            // fechaRecibido
            // 
            this.fechaRecibido.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.fechaRecibido.Location = new System.Drawing.Point(334, 63);
            this.fechaRecibido.Name = "fechaRecibido";
            this.fechaRecibido.Size = new System.Drawing.Size(92, 20);
            this.fechaRecibido.TabIndex = 9;
            this.fechaRecibido.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.fechaRecibido_KeyPress);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(243, 67);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(82, 13);
            this.label15.TabIndex = 8;
            this.label15.Text = "Fecha Recibido";
            // 
            // fecha
            // 
            this.fecha.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.fecha.Location = new System.Drawing.Point(118, 63);
            this.fecha.Name = "fecha";
            this.fecha.Size = new System.Drawing.Size(90, 20);
            this.fecha.TabIndex = 7;
            this.fecha.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.fecha_KeyPress);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(15, 67);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(89, 13);
            this.label14.TabIndex = 6;
            this.label14.Text = "Fecha Retencion";
            // 
            // txtMontoRet
            // 
            this.txtMontoRet.Location = new System.Drawing.Point(388, 26);
            this.txtMontoRet.Name = "txtMontoRet";
            this.txtMontoRet.Size = new System.Drawing.Size(82, 20);
            this.txtMontoRet.TabIndex = 5;
            this.txtMontoRet.Text = "0,00";
            this.txtMontoRet.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtMontoRet.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtMontoRet_KeyPress);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(322, 29);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(56, 13);
            this.label13.TabIndex = 4;
            this.label13.Text = "Retencion";
            // 
            // cmbPorcRet
            // 
            this.cmbPorcRet.FormattingEnabled = true;
            this.cmbPorcRet.Items.AddRange(new object[] {
            "",
            "75",
            "100"});
            this.cmbPorcRet.Location = new System.Drawing.Point(267, 26);
            this.cmbPorcRet.Name = "cmbPorcRet";
            this.cmbPorcRet.Size = new System.Drawing.Size(49, 21);
            this.cmbPorcRet.TabIndex = 3;
            this.cmbPorcRet.SelectedIndexChanged += new System.EventHandler(this.cmbPorcRet_SelectedIndexChanged);
            this.cmbPorcRet.Leave += new System.EventHandler(this.cmbPorcRet_Leave);
            this.cmbPorcRet.ValueMemberChanged += new System.EventHandler(this.cmbPorcRet_ValueMemberChanged);
            this.cmbPorcRet.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cmbPorcRet_KeyPress);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(228, 29);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(35, 13);
            this.label12.TabIndex = 2;
            this.label12.Text = "% Ret";
            // 
            // txtNumRetenc
            // 
            this.txtNumRetenc.BackColor = System.Drawing.Color.White;
            this.txtNumRetenc.Location = new System.Drawing.Point(118, 26);
            this.txtNumRetenc.MaxLength = 14;
            this.txtNumRetenc.Name = "txtNumRetenc";
            this.txtNumRetenc.Size = new System.Drawing.Size(104, 20);
            this.txtNumRetenc.TabIndex = 1;
            this.txtNumRetenc.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtNumRetenc_KeyPress);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(15, 29);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(96, 13);
            this.label11.TabIndex = 0;
            this.label11.Text = "Numero Retencion";
            // 
            // btnCancelar
            // 
            this.btnCancelar.Image = global::lebrun.Properties.Resources.LogOut_24x24;
            this.btnCancelar.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnCancelar.Location = new System.Drawing.Point(676, 312);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(92, 33);
            this.btnCancelar.TabIndex = 3;
            this.btnCancelar.Text = "   Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnAceptar
            // 
            this.btnAceptar.Image = global::lebrun.Properties.Resources.Check_24x24;
            this.btnAceptar.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnAceptar.Location = new System.Drawing.Point(539, 312);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(92, 33);
            this.btnAceptar.TabIndex = 2;
            this.btnAceptar.Text = "Aceptar";
            this.btnAceptar.UseVisualStyleBackColor = true;
            this.btnAceptar.Click += new System.EventHandler(this.btnAceptar_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.button2);
            this.groupBox3.Controls.Add(this.txtFecha);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.txtEstado);
            this.groupBox3.Controls.Add(this.txtIva);
            this.groupBox3.Controls.Add(this.label10);
            this.groupBox3.Controls.Add(this.txtBaseImponible);
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Controls.Add(this.txtExento);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.txtNeto);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.txtNumDoc);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.txtTipoDoc);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Location = new System.Drawing.Point(5, 81);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(763, 112);
            this.groupBox3.TabIndex = 4;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Informacion Documento";
            // 
            // button2
            // 
            this.button2.Image = global::lebrun.Properties.Resources._24x24_1372886216_go_back;
            this.button2.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.button2.Location = new System.Drawing.Point(656, 7);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(88, 32);
            this.button2.TabIndex = 39;
            this.button2.Text = "    Limpiar";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // txtFecha
            // 
            this.txtFecha.BackColor = System.Drawing.Color.White;
            this.txtFecha.Location = new System.Drawing.Point(252, 49);
            this.txtFecha.Name = "txtFecha";
            this.txtFecha.ReadOnly = true;
            this.txtFecha.Size = new System.Drawing.Size(100, 20);
            this.txtFecha.TabIndex = 38;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(194, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 13);
            this.label2.TabIndex = 37;
            this.label2.Text = "Fecha";
            // 
            // txtEstado
            // 
            this.txtEstado.BackColor = System.Drawing.Color.White;
            this.txtEstado.Location = new System.Drawing.Point(84, 49);
            this.txtEstado.Name = "txtEstado";
            this.txtEstado.ReadOnly = true;
            this.txtEstado.Size = new System.Drawing.Size(100, 20);
            this.txtEstado.TabIndex = 36;
            // 
            // txtIva
            // 
            this.txtIva.BackColor = System.Drawing.Color.White;
            this.txtIva.Location = new System.Drawing.Point(577, 79);
            this.txtIva.Name = "txtIva";
            this.txtIva.ReadOnly = true;
            this.txtIva.Size = new System.Drawing.Size(100, 20);
            this.txtIva.TabIndex = 35;
            this.txtIva.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(549, 82);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(22, 13);
            this.label10.TabIndex = 34;
            this.label10.Text = "Iva";
            // 
            // txtBaseImponible
            // 
            this.txtBaseImponible.BackColor = System.Drawing.Color.White;
            this.txtBaseImponible.Location = new System.Drawing.Point(443, 79);
            this.txtBaseImponible.Name = "txtBaseImponible";
            this.txtBaseImponible.ReadOnly = true;
            this.txtBaseImponible.Size = new System.Drawing.Size(100, 20);
            this.txtBaseImponible.TabIndex = 33;
            this.txtBaseImponible.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(357, 82);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(79, 13);
            this.label9.TabIndex = 32;
            this.label9.Text = "Base Imponible";
            // 
            // txtExento
            // 
            this.txtExento.BackColor = System.Drawing.Color.White;
            this.txtExento.Location = new System.Drawing.Point(252, 79);
            this.txtExento.Name = "txtExento";
            this.txtExento.ReadOnly = true;
            this.txtExento.Size = new System.Drawing.Size(100, 20);
            this.txtExento.TabIndex = 31;
            this.txtExento.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(194, 82);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(40, 13);
            this.label8.TabIndex = 30;
            this.label8.Text = "Exento";
            // 
            // txtNeto
            // 
            this.txtNeto.BackColor = System.Drawing.Color.White;
            this.txtNeto.Location = new System.Drawing.Point(84, 79);
            this.txtNeto.Name = "txtNeto";
            this.txtNeto.ReadOnly = true;
            this.txtNeto.Size = new System.Drawing.Size(100, 20);
            this.txtNeto.TabIndex = 29;
            this.txtNeto.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(16, 82);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(30, 13);
            this.label7.TabIndex = 28;
            this.label7.Text = "Neto";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(16, 52);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(40, 13);
            this.label6.TabIndex = 27;
            this.label6.Text = "Estado";
            // 
            // txtNumDoc
            // 
            this.txtNumDoc.BackColor = System.Drawing.Color.White;
            this.txtNumDoc.Location = new System.Drawing.Point(252, 19);
            this.txtNumDoc.Name = "txtNumDoc";
            this.txtNumDoc.ReadOnly = true;
            this.txtNumDoc.Size = new System.Drawing.Size(100, 20);
            this.txtNumDoc.TabIndex = 26;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(194, 22);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(44, 13);
            this.label4.TabIndex = 25;
            this.label4.Text = "Numero";
            // 
            // txtTipoDoc
            // 
            this.txtTipoDoc.BackColor = System.Drawing.Color.White;
            this.txtTipoDoc.Location = new System.Drawing.Point(84, 19);
            this.txtTipoDoc.Name = "txtTipoDoc";
            this.txtTipoDoc.ReadOnly = true;
            this.txtTipoDoc.Size = new System.Drawing.Size(100, 20);
            this.txtTipoDoc.TabIndex = 24;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 13);
            this.label3.TabIndex = 23;
            this.label3.Text = "Documento";
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.label17);
            this.panel1.Controls.Add(this.button4);
            this.panel1.Controls.Add(this.button3);
            this.panel1.Controls.Add(this.lblMontoRetenc);
            this.panel1.Controls.Add(this.label16);
            this.panel1.Location = new System.Drawing.Point(249, 81);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(268, 118);
            this.panel1.TabIndex = 21;
            this.panel1.Visible = false;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(67, 61);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(122, 13);
            this.label17.TabIndex = 4;
            this.label17.Text = "¿DESEA CONTINUAR?";
            // 
            // button4
            // 
            this.button4.Image = global::lebrun.Properties.Resources.Cancel_16x16;
            this.button4.Location = new System.Drawing.Point(157, 81);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 30);
            this.button4.TabIndex = 3;
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button3
            // 
            this.button3.Image = global::lebrun.Properties.Resources.Check_16x16;
            this.button3.Location = new System.Drawing.Point(30, 81);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 30);
            this.button3.TabIndex = 2;
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // lblMontoRetenc
            // 
            this.lblMontoRetenc.AutoSize = true;
            this.lblMontoRetenc.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMontoRetenc.Location = new System.Drawing.Point(112, 35);
            this.lblMontoRetenc.Name = "lblMontoRetenc";
            this.lblMontoRetenc.Size = new System.Drawing.Size(20, 16);
            this.lblMontoRetenc.TabIndex = 1;
            this.lblMontoRetenc.Text = "...";
            this.lblMontoRetenc.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(20, 12);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(228, 13);
            this.label16.TabIndex = 0;
            this.label16.Text = "EL MONTO DE LA RETENCION ES DE";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.btnAuxiliar);
            this.groupBox4.Controls.Add(this.txtAuxiliar);
            this.groupBox4.Controls.Add(this.lblDescripcionCuenta);
            this.groupBox4.Controls.Add(this.button5);
            this.groupBox4.Controls.Add(this.txtCuenta);
            this.groupBox4.Enabled = false;
            this.groupBox4.Location = new System.Drawing.Point(486, 199);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(281, 100);
            this.groupBox4.TabIndex = 22;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Movimiento Contable";
            // 
            // btnAuxiliar
            // 
            this.btnAuxiliar.Enabled = false;
            this.btnAuxiliar.Image = global::lebrun.Properties.Resources.Search_16x16;
            this.btnAuxiliar.Location = new System.Drawing.Point(217, 26);
            this.btnAuxiliar.Name = "btnAuxiliar";
            this.btnAuxiliar.Size = new System.Drawing.Size(26, 23);
            this.btnAuxiliar.TabIndex = 4;
            this.btnAuxiliar.UseVisualStyleBackColor = true;
            this.btnAuxiliar.Click += new System.EventHandler(this.button6_Click);
            // 
            // txtAuxiliar
            // 
            this.txtAuxiliar.Enabled = false;
            this.txtAuxiliar.Location = new System.Drawing.Point(151, 27);
            this.txtAuxiliar.Name = "txtAuxiliar";
            this.txtAuxiliar.Size = new System.Drawing.Size(60, 20);
            this.txtAuxiliar.TabIndex = 3;
            this.txtAuxiliar.Leave += new System.EventHandler(this.txtAuxiliar_Leave);
            this.txtAuxiliar.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtAuxiliar_KeyPress);
            // 
            // lblDescripcionCuenta
            // 
            this.lblDescripcionCuenta.AutoSize = true;
            this.lblDescripcionCuenta.Location = new System.Drawing.Point(19, 63);
            this.lblDescripcionCuenta.Name = "lblDescripcionCuenta";
            this.lblDescripcionCuenta.Size = new System.Drawing.Size(16, 13);
            this.lblDescripcionCuenta.TabIndex = 2;
            this.lblDescripcionCuenta.Text = "...";
            // 
            // button5
            // 
            this.button5.Image = global::lebrun.Properties.Resources.Search_16x16;
            this.button5.Location = new System.Drawing.Point(119, 26);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(26, 23);
            this.button5.TabIndex = 1;
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // txtCuenta
            // 
            this.txtCuenta.Location = new System.Drawing.Point(13, 27);
            this.txtCuenta.Name = "txtCuenta";
            this.txtCuenta.Size = new System.Drawing.Size(100, 20);
            this.txtCuenta.TabIndex = 0;
            this.txtCuenta.Leave += new System.EventHandler(this.txtCuenta_Leave);
            this.txtCuenta.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCuenta_KeyPress);
            // 
            // btnReimprimir
            // 
            this.btnReimprimir.Image = global::lebrun.Properties.Resources.Print_24x24;
            this.btnReimprimir.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnReimprimir.Location = new System.Drawing.Point(17, 312);
            this.btnReimprimir.Name = "btnReimprimir";
            this.btnReimprimir.Size = new System.Drawing.Size(92, 33);
            this.btnReimprimir.TabIndex = 23;
            this.btnReimprimir.Text = "      Reimprimir";
            this.btnReimprimir.UseVisualStyleBackColor = true;
            this.btnReimprimir.Click += new System.EventHandler(this.btnReimprimir_Click);
            // 
            // txtComprobante
            // 
            this.txtComprobante.Location = new System.Drawing.Point(123, 319);
            this.txtComprobante.Name = "txtComprobante";
            this.txtComprobante.Size = new System.Drawing.Size(145, 20);
            this.txtComprobante.TabIndex = 24;
            // 
            // frmRetIvaCli
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(780, 358);
            this.Controls.Add(this.txtComprobante);
            this.Controls.Add(this.btnReimprimir);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.btnAceptar);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "frmRetIvaCli";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Retencion Iva Clientes";
            this.Load += new System.EventHandler(this.frmRetIvaCli_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtControlFiscal;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox cmbPorcRet;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtNumRetenc;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.DateTimePicker fechaRecibido;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.DateTimePicker fecha;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txtMontoRet;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnAceptar;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox txtFecha;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtEstado;
        private System.Windows.Forms.TextBox txtIva;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtBaseImponible;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtExento;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtNeto;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtNumDoc;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtTipoDoc;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnImportar;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label label16;
        public System.Windows.Forms.Label lblMontoRetenc;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button button5;
        public System.Windows.Forms.Label lblNombreCliente;
        public System.Windows.Forms.TextBox txtCodigo;
        public System.Windows.Forms.TextBox txtCuenta;
        public System.Windows.Forms.Label lblDescripcionCuenta;
        private System.Windows.Forms.Button btnAuxiliar;
        public System.Windows.Forms.TextBox txtAuxiliar;
        private System.Windows.Forms.Button btnReimprimir;
        private System.Windows.Forms.TextBox txtComprobante;
    }
}