using System;
using System.Data.SqlClient;
using System.Windows.Forms;
using MiMultyCBGApp.BLL;
using MiMultyCBGApp.DAL;
using MiMultyCBGApp.Models;

namespace MiMultyCBGApp
{
    public partial class FormTransaksi : Form
    {
        private User currentUser;
        private BarangBLL barangBll = new BarangBLL();
        
        // Simpan harga satuan barang saat dipilih
        private decimal hargaSatuan = 0;

        public FormTransaksi(User user)
        {
            InitializeComponent();
            this.currentUser = user;
            lblInfo.Text = $"Login Staff: {user.Username} | ID Cabang: {user.ID_Cabang}";
            txtQty.KeyPress += NumericOnly_KeyPress;
            LoadDataBarang();
        }

        private void NumericOnly_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void LoadDataBarang()
        {
            dgvBarang.DataSource = barangBll.GetAllBarang();
        }

        private void dgvBarang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dgvBarang.Rows[e.RowIndex];
                txtKode.Text = row.Cells["KodeBarang"].Value.ToString();
                hargaSatuan = Convert.ToDecimal(row.Cells["Harga"].Value);
            }
        }

        private void btnProses_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtKode.Text) || string.IsNullOrEmpty(txtQty.Text))
            {
                MessageBox.Show("Pilih barang dan isi Qty terlebih dahulu!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string kodeBarang = txtKode.Text;
            int qtyReq = 0;
            if (!int.TryParse(txtQty.Text, out qtyReq) || qtyReq <= 0)
            {
                MessageBox.Show("Qty harus berupa angka lebih besar dari 0!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SqlConnection conn = DbConnection.GetConnection())
            {
                conn.Open();
                SqlTransaction sqlTrans = conn.BeginTransaction();
                try
                {
                    // 1. Cek Stok di DB
                    string queryCheck = "SELECT Stok FROM Barang WHERE KodeBarang=@Kode AND ID_Cabang=@Cabang";
                    int stokReal = 0;
                    using (SqlCommand cmdCheck = new SqlCommand(queryCheck, conn, sqlTrans))
                    {
                        cmdCheck.Parameters.AddWithValue("@Kode", kodeBarang);
                        cmdCheck.Parameters.AddWithValue("@Cabang", currentUser.ID_Cabang);
                        object result = cmdCheck.ExecuteScalar();
                        if (result != null)
                        {
                            stokReal = Convert.ToInt32(result);
                        }
                    }

                    if (stokReal < qtyReq)
                    {
                        MessageBox.Show("Stok Tidak Cukup!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        sqlTrans.Rollback();
                        return;
                    }

                    // 2. Kurangi Stok menggunakan ExecuteNonQuery
                    string queryUpdate = "UPDATE Barang SET Stok = Stok - @Qty WHERE KodeBarang=@Kode AND ID_Cabang=@Cabang";
                    using (SqlCommand cmdUpdate = new SqlCommand(queryUpdate, conn, sqlTrans))
                    {
                        cmdUpdate.Parameters.AddWithValue("@Qty", qtyReq);
                        cmdUpdate.Parameters.AddWithValue("@Kode", kodeBarang);
                        cmdUpdate.Parameters.AddWithValue("@Cabang", currentUser.ID_Cabang);
                        cmdUpdate.ExecuteNonQuery();
                    }

                    // 3. Catat Transaksi
                    decimal totalHarga = qtyReq * hargaSatuan;
                    string queryInsert = @"INSERT INTO Transaksi (ID_Cabang, UserID, KodeBarang, QtyTerjual, TotalHarga, TanggalTransaksi) 
                                           VALUES (@Cabang, @UID, @Kode, @Qty, @Total, @Tgl)";
                    using (SqlCommand cmdInsert = new SqlCommand(queryInsert, conn, sqlTrans))
                    {
                        cmdInsert.Parameters.AddWithValue("@Cabang", currentUser.ID_Cabang);
                        cmdInsert.Parameters.AddWithValue("@UID", currentUser.UserID);
                        cmdInsert.Parameters.AddWithValue("@Kode", kodeBarang);
                        cmdInsert.Parameters.AddWithValue("@Qty", qtyReq);
                        cmdInsert.Parameters.AddWithValue("@Total", totalHarga);
                        cmdInsert.Parameters.AddWithValue("@Tgl", DateTime.Now);
                        cmdInsert.ExecuteNonQuery();
                    }

                    sqlTrans.Commit();
                    MessageBox.Show($"Transaksi Berhasil!\nTotal Harga: Rp {totalHarga:N2}", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                    // Reset UI
                    txtKode.Clear();
                    txtQty.Clear();
                    hargaSatuan = 0;
                    
                    // Refresh grid stok
                    LoadDataBarang();
                }
                catch (Exception ex)
                {
                    sqlTrans.Rollback();
                    MessageBox.Show("Gagal menyimpan transaksi: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
