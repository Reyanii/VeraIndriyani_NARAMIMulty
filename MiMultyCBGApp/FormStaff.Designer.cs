namespace MiMultyCBGApp
{
    partial class FormStaff
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.DataGridView dgvBarangStaff;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnInputPenjualan;
        private System.Windows.Forms.Label lblInfo;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.dgvBarangStaff = new System.Windows.Forms.DataGridView();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnInputPenjualan = new System.Windows.Forms.Button();
            this.lblInfo = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBarangStaff)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvBarangStaff
            // 
            this.dgvBarangStaff.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvBarangStaff.Location = new System.Drawing.Point(12, 49);
            this.dgvBarangStaff.Name = "dgvBarangStaff";
            this.dgvBarangStaff.Size = new System.Drawing.Size(760, 300);
            this.dgvBarangStaff.TabIndex = 0;
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(12, 365);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(120, 40);
            this.btnRefresh.TabIndex = 1;
            this.btnRefresh.Text = "Refresh Data";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnInputPenjualan
            // 
            this.btnInputPenjualan.Location = new System.Drawing.Point(148, 365);
            this.btnInputPenjualan.Name = "btnInputPenjualan";
            this.btnInputPenjualan.Size = new System.Drawing.Size(120, 40);
            this.btnInputPenjualan.TabIndex = 2;
            this.btnInputPenjualan.Text = "Input Penjualan";
            this.btnInputPenjualan.UseVisualStyleBackColor = true;
            this.btnInputPenjualan.Click += new System.EventHandler(this.btnInputPenjualan_Click);
            // 
            // lblInfo
            // 
            this.lblInfo.AutoSize = true;
            this.lblInfo.Location = new System.Drawing.Point(12, 18);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(56, 13);
            this.lblInfo.TabIndex = 3;
            this.lblInfo.Text = "Login Staff";
            // 
            // FormStaff
            // 
            this.ClientSize = new System.Drawing.Size(784, 421);
            this.Controls.Add(this.lblInfo);
            this.Controls.Add(this.btnInputPenjualan);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.dgvBarangStaff);
            this.Name = "FormStaff";
            this.Text = "Dashboard Staff";
            ((System.ComponentModel.ISupportInitialize)(this.dgvBarangStaff)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
