namespace lebrun.formularios.contabilidad
{
    partial class lbxComprobanteContable
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
            this.cmdModificar = new System.Windows.Forms.Button();
            this.cmdNuevo = new System.Windows.Forms.Button();
            this.cmdElimnar = new System.Windows.Forms.Button();
            this.cmdBuscar = new System.Windows.Forms.Button();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.cmdSalir = new System.Windows.Forms.Button();
            this.cmdVer = new System.Windows.Forms.Button();
            this.cmdLimpiar = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.rbtEliminar = new System.Windows.Forms.RadioButton();
            this.rbtModificar = new System.Windows.Forms.RadioButton();
            this.cmdCancelarSupervicion = new System.Windows.Forms.Button();
            this.cmdAceptarSupervicion = new System.Windows.Forms.Button();
            this.txtClave = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmdModificar
            // 
            this.cmdModificar.Location = new System.Drawing.Point(398, 34);
            this.cmdModificar.Name = "cmdModificar";
            this.cmdModificar.Size = new System.Drawing.Size(77, 21);
            this.cmdModificar.TabIndex = 50;
            this.cmdModificar.Text = "Modificar";
            this.cmdModificar.UseVisualStyleBackColor = true;
            this.cmdModificar.Click += new System.EventHandler(this.cmdModificar_Click);
            // 
            // cmdNuevo
            // 
            this.cmdNuevo.Location = new System.Drawing.Point(320, 34);
            this.cmdNuevo.Name = "cmdNuevo";
            this.cmdNuevo.Size = new System.Drawing.Size(72, 21);
            this.cmdNuevo.TabIndex = 49;
            this.cmdNuevo.Text = "Nuevo";
            this.cmdNuevo.UseVisualStyleBackColor = true;
            this.cmdNuevo.Click += new System.EventHandler(this.cmdNuevo_Click);
            // 
            // cmdElimnar
            // 
            this.cmdElimnar.Location = new System.Drawing.Point(646, 34);
            this.cmdElimnar.Name = "cmdElimnar";
            this.cmdElimnar.Size = new System.Drawing.Size(77, 21);
            this.cmdElimnar.TabIndex = 51;
            this.cmdElimnar.Text = "Eliminar";
            this.cmdElimnar.UseVisualStyleBackColor = true;
            // 
            // cmdBuscar
            // 
            this.cmdBuscar.Location = new System.Drawing.Point(237, 34);
            this.cmdBuscar.Name = "cmdBuscar";
            this.cmdBuscar.Size = new System.Drawing.Size(77, 21);
            this.cmdBuscar.TabIndex = 48;
            this.cmdBuscar.Text = "Buscar";
            this.cmdBuscar.UseVisualStyleBackColor = true;
            this.cmdBuscar.Click += new System.EventHandler(this.cmdBuscar_Click);
            // 
            // textBox5
            // 
            this.textBox5.Location = new System.Drawing.Point(15, 35);
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new System.Drawing.Size(152, 20);
            this.textBox5.TabIndex = 52;
            this.textBox5.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dataGridView1.Location = new System.Drawing.Point(15, 74);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(725, 216);
            this.dataGridView1.TabIndex = 54;
            // 
            // cmdSalir
            // 
            this.cmdSalir.Location = new System.Drawing.Point(657, 296);
            this.cmdSalir.Name = "cmdSalir";
            this.cmdSalir.Size = new System.Drawing.Size(80, 21);
            this.cmdSalir.TabIndex = 55;
            this.cmdSalir.Text = "Salir";
            this.cmdSalir.UseVisualStyleBackColor = true;
            this.cmdSalir.Click += new System.EventHandler(this.cmdSalir_Click);
            // 
            // cmdVer
            // 
            this.cmdVer.Location = new System.Drawing.Point(485, 34);
            this.cmdVer.Name = "cmdVer";
            this.cmdVer.Size = new System.Drawing.Size(77, 21);
            this.cmdVer.TabIndex = 56;
            this.cmdVer.Text = "Ver";
            this.cmdVer.UseVisualStyleBackColor = true;
            this.cmdVer.Click += new System.EventHandler(this.cmdVer_Click);
            // 
            // cmdLimpiar
            // 
            this.cmdLimpiar.Location = new System.Drawing.Point(568, 34);
            this.cmdLimpiar.Name = "cmdLimpiar";
            this.cmdLimpiar.Size = new System.Drawing.Size(72, 21);
            this.cmdLimpiar.TabIndex = 57;
            this.cmdLimpiar.Text = "Limpiar";
            this.cmdLimpiar.UseVisualStyleBackColor = true;
            this.cmdLimpiar.Click += new System.EventHandler(this.cmdLimpiar_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Menu;
            this.panel1.Controls.Add(this.rbtEliminar);
            this.panel1.Controls.Add(this.rbtModificar);
            this.panel1.Controls.Add(this.cmdCancelarSupervicion);
            this.panel1.Controls.Add(this.cmdAceptarSupervicion);
            this.panel1.Controls.Add(this.txtClave);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(278, 120);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(212, 98);
            this.panel1.TabIndex = 66;
            this.panel1.Visible = false;
            // 
            // rbtEliminar
            // 
            this.rbtEliminar.AutoSize = true;
            this.rbtEliminar.Location = new System.Drawing.Point(25, 43);
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
            this.rbtModificar.Location = new System.Drawing.Point(168, 37);
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
            this.txtClave.Name = "txtClave";
            this.txtClave.PasswordChar = '*';
            this.txtClave.Size = new System.Drawing.Size(100, 20);
            this.txtClave.TabIndex = 67;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(42, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(134, 16);
            this.label1.TabIndex = 66;
            this.label1.Text = "Clave Supervisión";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(12, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(101, 16);
            this.label3.TabIndex = 74;
            this.label3.Text = "Comprobante";
            // 
            // lbxComprobanteContable
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(749, 336);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.cmdLimpiar);
            this.Controls.Add(this.cmdVer);
            this.Controls.Add(this.cmdSalir);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.textBox5);
            this.Controls.Add(this.cmdModificar);
            this.Controls.Add(this.cmdNuevo);
            this.Controls.Add(this.cmdElimnar);
            this.Controls.Add(this.cmdBuscar);
            this.Name = "lbxComprobanteContable";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Lebrun - Lbx Comprobante Contable";
            this.Load += new System.EventHandler(this.lbxComprobanteContable_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cmdModificar;
        private System.Windows.Forms.Button cmdNuevo;
        private System.Windows.Forms.Button cmdElimnar;
        private System.Windows.Forms.Button cmdBuscar;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button cmdSalir;
        private System.Windows.Forms.Button cmdVer;
        private System.Windows.Forms.Button cmdLimpiar;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button cmdCancelarSupervicion;
        private System.Windows.Forms.Button cmdAceptarSupervicion;
        private System.Windows.Forms.TextBox txtClave;
        private System.Windows.Forms.RadioButton rbtModificar;
        private System.Windows.Forms.RadioButton rbtEliminar;
        private System.Windows.Forms.Label label3;
    }
}