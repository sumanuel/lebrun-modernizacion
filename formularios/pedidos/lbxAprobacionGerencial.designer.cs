namespace lebrun.formularios.pedidos
{
    partial class lbxAprobacionGerencial
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cmdModificar = new System.Windows.Forms.Button();
            this.cmdAgregar = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.cmdSalir = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.@id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.@status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.@descripcion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.@monto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(250)))));
            this.groupBox1.Controls.Add(this.cmdModificar);
            this.groupBox1.Controls.Add(this.cmdAgregar);
            this.groupBox1.Controls.Add(this.dataGridView1);
            this.groupBox1.Controls.Add(this.cmdSalir);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(535, 330);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // cmdModificar
            // 
            this.cmdModificar.Image = global::lebrun.Properties.Resources._24x24_1372886216_go_back;
            this.cmdModificar.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.cmdModificar.Location = new System.Drawing.Point(327, 272);
            this.cmdModificar.Name = "cmdModificar";
            this.cmdModificar.Size = new System.Drawing.Size(82, 36);
            this.cmdModificar.TabIndex = 86;
            this.cmdModificar.Text = "Modificar";
            this.cmdModificar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmdModificar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.cmdModificar.UseVisualStyleBackColor = true;
            this.cmdModificar.Click += new System.EventHandler(this.cmdModificar_Click);
            // 
            // cmdAgregar
            // 
            this.cmdAgregar.Enabled = false;
            this.cmdAgregar.Image = global::lebrun.Properties.Resources.Add_24x24;
            this.cmdAgregar.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.cmdAgregar.Location = new System.Drawing.Point(228, 272);
            this.cmdAgregar.Name = "cmdAgregar";
            this.cmdAgregar.Size = new System.Drawing.Size(82, 36);
            this.cmdAgregar.TabIndex = 85;
            this.cmdAgregar.Text = "Agregar";
            this.cmdAgregar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmdAgregar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.cmdAgregar.UseVisualStyleBackColor = true;
            this.cmdAgregar.Visible = false;
            this.cmdAgregar.Click += new System.EventHandler(this.cmdAgregar_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.@id,
            this.@status,
            this.@descripcion,
            this.@monto});
            this.dataGridView1.Location = new System.Drawing.Point(18, 70);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.Size = new System.Drawing.Size(497, 183);
            this.dataGridView1.TabIndex = 84;
            // 
            // cmdSalir
            // 
            this.cmdSalir.Image = global::lebrun.Properties.Resources.LogOut_24x24;
            this.cmdSalir.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.cmdSalir.Location = new System.Drawing.Point(433, 272);
            this.cmdSalir.Name = "cmdSalir";
            this.cmdSalir.Size = new System.Drawing.Size(82, 36);
            this.cmdSalir.TabIndex = 82;
            this.cmdSalir.Text = "Salir";
            this.cmdSalir.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmdSalir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.cmdSalir.UseVisualStyleBackColor = true;
            this.cmdSalir.Click += new System.EventHandler(this.cmdSalir_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(150, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(214, 24);
            this.label1.TabIndex = 1;
            this.label1.Text = "Aprobación Gerencial";
            // 
            // @id
            // 
            this.@id.DataPropertyName = "@id";
            this.@id.HeaderText = "@id";
            this.@id.Name = "@id";
            this.@id.ReadOnly = true;
            this.@id.Visible = false;
            // 
            // @status
            // 
            this.@status.DataPropertyName = "@status";
            this.@status.FillWeight = 37.61609F;
            this.@status.HeaderText = "Status";
            this.@status.MaxInputLength = 1;
            this.@status.Name = "@status";
            this.@status.ReadOnly = true;
            this.@status.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.@status.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // @descripcion
            // 
            this.@descripcion.DataPropertyName = "@descripcion";
            this.@descripcion.FillWeight = 201.4702F;
            this.@descripcion.HeaderText = "Descripcion";
            this.@descripcion.Name = "@descripcion";
            this.@descripcion.ReadOnly = true;
            // 
            // @monto
            // 
            this.@monto.DataPropertyName = "@monto";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopRight;
            this.@monto.DefaultCellStyle = dataGridViewCellStyle1;
            this.@monto.FillWeight = 60.9137F;
            this.@monto.HeaderText = "Monto";
            this.@monto.Name = "@monto";
            this.@monto.ReadOnly = true;
            // 
            // lbxAprobacionGerencial
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(106)))), ((int)(((byte)(197)))));
            this.ClientSize = new System.Drawing.Size(561, 358);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "lbxAprobacionGerencial";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "lbxAprobacionGerencial";
            this.Load += new System.EventHandler(this.lbxAprobacionGerencial_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button cmdSalir;
        private System.Windows.Forms.DataGridViewTextBoxColumn @id;
        private System.Windows.Forms.DataGridViewTextBoxColumn @status;
        private System.Windows.Forms.DataGridViewTextBoxColumn @descripcion;
        private System.Windows.Forms.DataGridViewTextBoxColumn @monto;
        private System.Windows.Forms.Button cmdModificar;
        private System.Windows.Forms.Button cmdAgregar;
        internal System.Windows.Forms.DataGridView dataGridView1;
    }
}