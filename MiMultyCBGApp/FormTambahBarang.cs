using System;
using System.Drawing;
using System.Windows.Forms;
using MiMultyCBGApp.BLL;
using MiMultyCBGApp.Models;

namespace MiMultyCBGApp
{
    public class FormTambahBarang : Form
    {
        private CheckBox chkExisting;
        private ComboBox cmbKodeBarang;
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
            this.Size = new Size(350, 400);
            this.StartPosition = FormStartPosition.CenterParent;

            int yPos = 20;
            int yStep = 40;

            chkExisting = new CheckBox() { Text = "Pilih Barang Existing", Location = new Point(20, yPos), Width = 200 };
            chkExisting.CheckedChanged += ChkExisting_CheckedChanged;
            this.Controls.Add(chkExisting);
            yPos += yStep;

            this.Controls.Add(new Label() { Text = "Kode Barang:", Location = new Point(20, yPos), Width = 100 });
            cmbKodeBarang = new ComboBox() { Location = new Point(130, yPos), Width = 150, DropDownStyle = ComboBoxStyle.DropDown, Enabled = false, Text = bll.GenerateKodeBarang() };
            cmbKodeBarang.SelectedIndexChanged += CmbKodeBarang_SelectedIndexChanged;
            this.Controls.Add(cmbKodeBarang);
            yPos += yStep;

            this.Controls.Add(new Label() { Text = "ID Cabang:", Location = new Point(20, yPos), Width = 100 });
            txtIdCabang = new TextBox() { Location = new Point(130, yPos), Width = 150 };
            txtIdCabang.KeyPress += NumericOnly_KeyPress;
            this.Controls.Add(txtIdCabang);
            yPos += yStep;

            this.Controls.Add(new Label() { Text = "Nama Barang:", Location = new Point(20, yPos), Width = 100 });
            txtNamaBarang = new TextBox() { Location = new Point(130, yPos), Width = 150, MaxLength = 100 };
            this.Controls.Add(txtNamaBarang);
            yPos += yStep;

            this.Controls.Add(new Label() { Text = "Kategori:", Location = new Point(20, yPos), Width = 100 });
            txtKategori = new TextBox() { Location = new Point(130, yPos), Width = 150, MaxLength = 50 };
            this.Controls.Add(txtKategori);
            yPos += yStep;

            this.Controls.Add(new Label() { Text = "Harga:", Location = new Point(20, yPos), Width = 100 });
            txtHarga = new TextBox() { Location = new Point(130, yPos), Width = 150 };
            txtHarga.KeyPress += NumericOnly_KeyPress;
            this.Controls.Add(txtHarga);
            yPos += yStep;

            this.Controls.Add(new Label() { Text = "Stok Awal:", Location = new Point(20, yPos), Width = 100 });
            txtStok = new TextBox() { Location = new Point(130, yPos), Width = 150 };
            txtStok.KeyPress += NumericOnly_KeyPress;
            this.Controls.Add(txtStok);
            yPos += yStep;

            btnSimpan = new Button() { Text = "Simpan", Location = new Point(130, yPos), Width = 100, BackColor = Color.LightGreen };
            btnSimpan.Click += BtnSimpan_Click;
            this.Controls.Add(btnSimpan);
        }

        private void ChkExisting_CheckedChanged(object sender, EventArgs e)
        {
            if (chkExisting.Checked)
            {
                cmbKodeBarang.Enabled = true;
                cmbKodeBarang.DropDownStyle = ComboBoxStyle.DropDownList;
                
                txtNamaBarang.ReadOnly = true;
                txtKategori.ReadOnly = true;
                txtHarga.ReadOnly = true;

                cmbKodeBarang.DataSource = bll.GetDistinctBarang();
                cmbKodeBarang.DisplayMember = "KodeBarang";
                cmbKodeBarang.ValueMember = "KodeBarang";
                cmbKodeBarang.SelectedIndex = -1;
                
                txtNamaBarang.Clear();
                txtKategori.Clear();
                txtHarga.Clear();
            }
            else
            {
                cmbKodeBarang.DataSource = null;
                cmbKodeBarang.Items.Clear();
                cmbKodeBarang.Enabled = false;
                cmbKodeBarang.DropDownStyle = ComboBoxStyle.DropDown;
                cmbKodeBarang.Text = bll.GenerateKodeBarang();

                txtNamaBarang.ReadOnly = false;
                txtKategori.ReadOnly = false;
                txtHarga.ReadOnly = false;
                
                txtNamaBarang.Clear();
                txtKategori.Clear();
                txtHarga.Clear();
            }
        }

        private void CmbKodeBarang_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbKodeBarang.SelectedIndex != -1 && cmbKodeBarang.SelectedItem is Barang selectedBarang)
            {
                txtNamaBarang.Text = selectedBarang.NamaBarang;
                txtKategori.Text = selectedBarang.Kategori;
                txtHarga.Text = selectedBarang.Harga.ToString("0");
            }
        }

        private void NumericOnly_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void BtnSimpan_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(cmbKodeBarang.Text) || 
                string.IsNullOrWhiteSpace(txtIdCabang.Text) || 
                string.IsNullOrWhiteSpace(txtNamaBarang.Text) || 
                string.IsNullOrWhiteSpace(txtKategori.Text) || 
                string.IsNullOrWhiteSpace(txtHarga.Text) || 
                string.IsNullOrWhiteSpace(txtStok.Text))
            {
                MessageBox.Show("Semua data harus diisi!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!decimal.TryParse(txtHarga.Text, out decimal hargaParsed))
            {
                MessageBox.Show("Input string was not in a correct format untuk Harga!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (hargaParsed <= 0)
            {
                MessageBox.Show("Harga barang harus lebih besar dari Rp 0!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(txtStok.Text, out int stokParsed))
            {
                MessageBox.Show("Input string was not in a correct format untuk Stok!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (stokParsed <= 0)
            {
                MessageBox.Show("Jumlah stok atau restock harus minimal 1!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                Barang b = new Barang()
                {
                    KodeBarang = cmbKodeBarang.Text,
                    ID_Cabang = int.Parse(txtIdCabang.Text),
                    NamaBarang = txtNamaBarang.Text,
                    Kategori = txtKategori.Text,
                    Harga = hargaParsed,
                    Stok = stokParsed
                };
                
                bll.InsertBarang(b);
                MessageBox.Show("Barang berhasil ditambah ke Cabang tersebut!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saat Menyimpan: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
