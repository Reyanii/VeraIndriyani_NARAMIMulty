using System;
using System.Data.SqlClient;
using System.Windows.Forms;
using MiMultyCBGApp.BLL;
using MiMultyCBGApp.DAL;
using MiMultyCBGApp.Models;

namespace MiMultyCBGApp
{
    public partial class FormInputPenjualan : Form
    {
        private User currentUser;
        private BarangBLL barangBll = new BarangBLL();
        private decimal hargaSatuan = 0; 

        public FormInputPenjualan(User user, string preSelectedKode = "")
        {
            InitializeComponent();
            this.currentUser = user;
            txtIDCabang.Text = user.ID_Cabang.ToString();
            txtIDCabang.ReadOnly = true;

            // Jika form dipanggil lewat double-click row (kode barang sudah ada)
            if (!string.IsNullOrEmpty(preSelectedKode))
            {
                txtIDBarang.Text = preSelectedKode;
                // Kunci textbox agar kode tidak sengaja terubah
                txtIDBarang.ReadOnly = true; 
                
                // Otomatis klik tombol "Cek Nama" buat mengisi label
                btnCekNama_Click(null, null);
                
                // Fokuskan kursor langsung ke field jumlah
                this.ActiveControl = txtJumlah;
            }
        }

        private void btnCekNama_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtIDBarang.Text))
            {
                MessageBox.Show("Masukkan ID Barang terlebih dahulu!");
                return;
            }

            string nama = barangBll.GetNamaBarangDiCabang(txtIDBarang.Text, currentUser.ID_Cabang);
            
            if (string.IsNullOrEmpty(nama))
            {
                lblNamaBarang.Text = "Nama: Tidak Ditemukan / Tidak di Cabang Ini";
                hargaSatuan = 0;
            }
            else
            {
                lblNamaBarang.Text = $"Nama: {nama}";
            }
        }

        private void btnSimpan_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtIDBarang.Text) || string.IsNullOrWhiteSpace(txtJumlah.Text))
            {
                MessageBox.Show("Lengkapi ID Barang dan Jumlah!");
                return;
            }

            string kodeBarang = txtIDBarang.Text;
            int qtyReq = 0;

            if (!int.TryParse(txtJumlah.Text, out qtyReq) || qtyReq <= 0)
            {
                MessageBox.Show("Jumlah harus berupa angka lebih dari 0!");
                return;
            }

            using (SqlConnection conn = DbConnection.GetConnection())
            {
                conn.Open();
                SqlTransaction sqlTrans = conn.BeginTransaction();
                try
                {
                    string queryCheck = "SELECT Stok, Harga FROM Barang WHERE KodeBarang=@Kode AND ID_Cabang=@Cabang";
                    int stokReal = 0;
                    
                    using (SqlCommand cmdCheck = new SqlCommand(queryCheck, conn, sqlTrans))
                    {
                        cmdCheck.Parameters.AddWithValue("@Kode", kodeBarang);
                        cmdCheck.Parameters.AddWithValue("@Cabang", currentUser.ID_Cabang);
                        
                        using (SqlDataReader reader = cmdCheck.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                stokReal = Convert.ToInt32(reader["Stok"]);
                                hargaSatuan = Convert.ToDecimal(reader["Harga"]);
                            }
                            else
                            {
                                MessageBox.Show("Barang tidak ditemukan di cabang ini!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                sqlTrans.Rollback();
                                return;
                            }
                        }
                    }

                    if (stokReal < qtyReq)
                    {
                        MessageBox.Show($"Stok tidak cukup! (Stok saat ini: {stokReal})", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        sqlTrans.Rollback();
                        return;
                    }

                    string queryUpdate = "UPDATE Barang SET Stok = Stok - @Qty WHERE KodeBarang=@Kode AND ID_Cabang=@Cabang";
                    using (SqlCommand cmdUpdate = new SqlCommand(queryUpdate, conn, sqlTrans))
                    {
                        cmdUpdate.Parameters.AddWithValue("@Qty", qtyReq);
                        cmdUpdate.Parameters.AddWithValue("@Kode", kodeBarang);
                        cmdUpdate.Parameters.AddWithValue("@Cabang", currentUser.ID_Cabang);
                        cmdUpdate.ExecuteNonQuery();
                    }

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
                    this.Close();
                }
                catch (Exception ex)
                {
                    sqlTrans.Rollback();
                    MessageBox.Show("Gagal menyimpan transaksi: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
