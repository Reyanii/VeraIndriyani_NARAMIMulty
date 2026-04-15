using System;
using System.Drawing;
using System.Windows.Forms;
using MiMultyCBGApp.BLL;
using MiMultyCBGApp.Models;

namespace MiMultyCBGApp
{
    public class FormTambahBarang : Form
    {
        private TextBox txtKodeBarang;
        private TextBox txtIdCabang;
        private TextBox txtNamaBarang;
        private TextBox txtKategori;
        private TextBox txtHarga;
        private TextBox txtStok;
        private Button btnSimpan;
        private BarangBLL bll = new BarangBLL();

        public FormTambahBarang()
        {
            this.Text = "Tambah Barang Baru";
            this.Size = new Size(350, 350);
            this.StartPosition = FormStartPosition.CenterParent;

            int yPos = 20;
            int yStep = 40;

            this.Controls.Add(new Label() { Text = "Kode Barang:", Location = new Point(20, yPos), Width = 100 });
            txtKodeBarang = new TextBox() { Location = new Point(130, yPos), Width = 150 };
            this.Controls.Add(txtKodeBarang);
            yPos += yStep;

            this.Controls.Add(new Label() { Text = "ID Cabang:", Location = new Point(20, yPos), Width = 100 });
            txtIdCabang = new TextBox() { Location = new Point(130, yPos), Width = 150 };
            this.Controls.Add(txtIdCabang);
            yPos += yStep;

            this.Controls.Add(new Label() { Text = "Nama Barang:", Location = new Point(20, yPos), Width = 100 });
            txtNamaBarang = new TextBox() { Location = new Point(130, yPos), Width = 150 };
            this.Controls.Add(txtNamaBarang);
            yPos += yStep;

            this.Controls.Add(new Label() { Text = "Kategori:", Location = new Point(20, yPos), Width = 100 });
            txtKategori = new TextBox() { Location = new Point(130, yPos), Width = 150 };
            this.Controls.Add(txtKategori);
            yPos += yStep;

            this.Controls.Add(new Label() { Text = "Harga:", Location = new Point(20, yPos), Width = 100 });
            txtHarga = new TextBox() { Location = new Point(130, yPos), Width = 150 };
            this.Controls.Add(txtHarga);
            yPos += yStep;

            this.Controls.Add(new Label() { Text = "Stok Awal:", Location = new Point(20, yPos), Width = 100 });
            txtStok = new TextBox() { Location = new Point(130, yPos), Width = 150 };
            this.Controls.Add(txtStok);
            yPos += yStep;

            btnSimpan = new Button() { Text = "Simpan", Location = new Point(130, yPos), Width = 100, BackColor = Color.LightGreen };
            btnSimpan.Click += BtnSimpan_Click;
            this.Controls.Add(btnSimpan);
        }

        private void BtnSimpan_Click(object sender, EventArgs e)
        {
            try
            {
                Barang b = new Barang()
                {
                    KodeBarang = txtKodeBarang.Text,
                    ID_Cabang = int.Parse(txtIdCabang.Text),
                    NamaBarang = txtNamaBarang.Text,
                    Kategori = txtKategori.Text,
                    Harga = decimal.Parse(txtHarga.Text),
                    Stok = int.Parse(txtStok.Text)
                };
                
                bll.InsertBarang(b);
                MessageBox.Show("Barang berhasil ditambah ke Cabang tersebut!");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saat Menyimpan: " + ex.Message);
            }
        }
    }
}
