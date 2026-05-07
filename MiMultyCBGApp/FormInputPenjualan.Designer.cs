namespace MiMultyCBGApp
{
    partial class FormInputPenjualan
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TextBox txtIDBarang;
        private System.Windows.Forms.TextBox txtIDCabang;
        private System.Windows.Forms.TextBox txtJumlah;
        private System.Windows.Forms.Label lblNamaBarang;
        private System.Windows.Forms.Button btnCekNama;
        private System.Windows.Forms.Button btnSimpan;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;

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
            this.txtIDBarang = new System.Windows.Forms.TextBox();
            this.txtIDCabang = new System.Windows.Forms.TextBox();
            this.txtJumlah = new System.Windows.Forms.TextBox();
            this.lblNamaBarang = new System.Windows.Forms.Label();
            this.btnCekNama = new System.Windows.Forms.Button();
            this.btnSimpan = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(30, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "ID Barang";
            // 
            // txtIDBarang
            // 
            this.txtIDBarang.Location = new System.Drawing.Point(100, 27);
            this.txtIDBarang.Name = "txtIDBarang";
            this.txtIDBarang.Size = new System.Drawing.Size(120, 20);
            this.txtIDBarang.TabIndex = 1;
            this.txtIDBarang.MaxLength = 50;
            // 
            // btnCekNama
            // 
            this.btnCekNama.Location = new System.Drawing.Point(230, 25);
            this.btnCekNama.Name = "btnCekNama";
            this.btnCekNama.Size = new System.Drawing.Size(75, 23);
            this.btnCekNama.TabIndex = 2;
            this.btnCekNama.Text = "Cek Nama";
            this.btnCekNama.UseVisualStyleBackColor = true;
            this.btnCekNama.Click += new System.EventHandler(this.btnCekNama_Click);
            // 
            // lblNamaBarang
            // 
            this.lblNamaBarang.AutoSize = true;
            this.lblNamaBarang.Location = new System.Drawing.Point(30, 60);
            this.lblNamaBarang.Name = "lblNamaBarang";
            this.lblNamaBarang.Size = new System.Drawing.Size(44, 13);
            this.lblNamaBarang.TabIndex = 3;
            this.lblNamaBarang.Text = "Nama: -";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(30, 90);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "ID Cabang";
            // 
            // txtIDCabang
            // 
            this.txtIDCabang.Location = new System.Drawing.Point(100, 87);
            this.txtIDCabang.Name = "txtIDCabang";
            this.txtIDCabang.Size = new System.Drawing.Size(120, 20);
            this.txtIDCabang.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(30, 120);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(40, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Jumlah";
            // 
            // txtJumlah
            // 
            this.txtJumlah.Location = new System.Drawing.Point(100, 117);
            this.txtJumlah.Name = "txtJumlah";
            this.txtJumlah.Size = new System.Drawing.Size(120, 20);
            this.txtJumlah.TabIndex = 7;
            // 
            // btnSimpan
            // 
            this.btnSimpan.Location = new System.Drawing.Point(100, 160);
            this.btnSimpan.Name = "btnSimpan";
            this.btnSimpan.Size = new System.Drawing.Size(75, 23);
            this.btnSimpan.TabIndex = 8;
            this.btnSimpan.Text = "Simpan";
            this.btnSimpan.UseVisualStyleBackColor = true;
            this.btnSimpan.Click += new System.EventHandler(this.btnSimpan_Click);
            // 
            // FormInputPenjualan
            // 
            this.ClientSize = new System.Drawing.Size(334, 211);
            this.Controls.Add(this.btnSimpan);
            this.Controls.Add(this.txtJumlah);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtIDCabang);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblNamaBarang);
            this.Controls.Add(this.btnCekNama);
            this.Controls.Add(this.txtIDBarang);
            this.Controls.Add(this.label1);
            this.Name = "FormInputPenjualan";
            this.Text = "NARA - Input Penjualan";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
