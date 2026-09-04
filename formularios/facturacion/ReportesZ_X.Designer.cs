namespace lebrun.formularios.facturacion
{
    partial class ReportesZ_X
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReportesZ_X));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.IPuerto = new AxMSCommLib.AxMSComm();
            this.FISCAL = new AxTM_FISCAL.AxTM950();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.IPuerto)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FISCAL)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.SystemColors.Window;
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(13, 7);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(245, 101);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // button2
            // 
            this.button2.Image = global::lebrun.Properties.Resources.LogOut_16x16;
            this.button2.Location = new System.Drawing.Point(126, 53);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(77, 29);
            this.button2.TabIndex = 158;
            this.button2.Text = "Salir";
            this.button2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Image = global::lebrun.Properties.Resources.Add_16x16;
            this.button1.Location = new System.Drawing.Point(43, 53);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(77, 29);
            this.button1.TabIndex = 157;
            this.button1.Text = "Aceptar";
            this.button1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(54, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 18);
            this.label1.TabIndex = 0;
            this.label1.Text = "Realizar ";
            // 
            // IPuerto
            // 
            this.IPuerto.Enabled = true;
            this.IPuerto.Location = new System.Drawing.Point(264, 105);
            this.IPuerto.Name = "IPuerto";
            this.IPuerto.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("IPuerto.OcxState")));
            this.IPuerto.Size = new System.Drawing.Size(38, 38);
            this.IPuerto.TabIndex = 167;
            // 
            // FISCAL
            // 
            this.FISCAL.Enabled = true;
            this.FISCAL.Location = new System.Drawing.Point(221, 118);
            this.FISCAL.Name = "FISCAL";
            this.FISCAL.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("FISCAL.OcxState")));
            this.FISCAL.Size = new System.Drawing.Size(32, 32);
            this.FISCAL.TabIndex = 168;
            // 
            // ReportesZ_X
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(106)))), ((int)(((byte)(197)))));
            this.ClientSize = new System.Drawing.Size(272, 125);
            this.ControlBox = false;
            this.Controls.Add(this.FISCAL);
            this.Controls.Add(this.IPuerto);
            this.Controls.Add(this.groupBox1);
            this.Name = "ReportesZ_X";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Lebrun -";
            this.Load += new System.EventHandler(this.ReportesZ_X_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.IPuerto)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FISCAL)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private AxMSCommLib.AxMSComm IPuerto;
        private AxTM_FISCAL.AxTM950 FISCAL;
    }
}