namespace lebrun.formularios.complementos
{
    partial class lbxTarifaRetenc
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
            this.gridTarifaRetenc = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.gridTarifaRetenc)).BeginInit();
            this.SuspendLayout();
            // 
            // gridTarifaRetenc
            // 
            this.gridTarifaRetenc.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridTarifaRetenc.Location = new System.Drawing.Point(12, 12);
            this.gridTarifaRetenc.Name = "gridTarifaRetenc";
            this.gridTarifaRetenc.ReadOnly = true;
            this.gridTarifaRetenc.Size = new System.Drawing.Size(647, 242);
            this.gridTarifaRetenc.TabIndex = 0;
            this.gridTarifaRetenc.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridTarifaRetenc_CellDoubleClick);
            // 
            // lbxTarifaRetenc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(671, 266);
            this.Controls.Add(this.gridTarifaRetenc);
            this.Name = "lbxTarifaRetenc";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Tarifa Retencion";
            this.Load += new System.EventHandler(this.lbxTarifaRetenc_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridTarifaRetenc)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView gridTarifaRetenc;
    }
}