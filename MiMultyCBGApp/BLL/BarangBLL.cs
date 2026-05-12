using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using MiMultyCBGApp.DAL;
using MiMultyCBGApp.Models;

namespace MiMultyCBGApp.BLL
{
    public class BarangBLL
    {
        public List<Barang> GetAllBarang()
        {
            List<Barang> listBarang = new List<Barang>();
            using (SqlConnection conn = DbConnection.GetConnection())
            {
                string query = "SELECT * FROM Barang";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    try
                    {
                        conn.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                listBarang.Add(new Barang
                                {
                                    KodeBarang = reader["KodeBarang"].ToString(),
                                    NamaBarang = reader["NamaBarang"].ToString(),
                                    Kategori = reader["Kategori"].ToString(),
                                    Harga = Convert.ToDecimal(reader["Harga"]),
                                    Stok = Convert.ToInt32(reader["Stok"])
                                });
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Error Get All Barang: " + ex.Message);
                    }
                }
            }
            return listBarang;
        }

        public void InsertBarang(Barang b)
        {
            using (SqlConnection conn = DbConnection.GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand("SP_InsertBarang", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Kode", b.KodeBarang);
                    cmd.Parameters.AddWithValue("@Cabang", b.ID_Cabang);
                    cmd.Parameters.AddWithValue("@Nama", b.NamaBarang);
                    cmd.Parameters.AddWithValue("@Kat", b.Kategori);
                    cmd.Parameters.AddWithValue("@Harga", b.Harga);
                    cmd.Parameters.AddWithValue("@Stok", b.Stok);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<Barang> GetDistinctBarang()
        {
            List<Barang> listBarang = new List<Barang>();
            using (SqlConnection conn = DbConnection.GetConnection())
            {
                string query = "SELECT DISTINCT KodeBarang, NamaBarang, Kategori, Harga FROM Barang";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    try
                    {
                        conn.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                listBarang.Add(new Barang
                                {
                                    KodeBarang = reader["KodeBarang"].ToString(),
                                    NamaBarang = reader["NamaBarang"].ToString(),
                                    Kategori = reader["Kategori"].ToString(),
                                    Harga = Convert.ToDecimal(reader["Harga"])
                                });
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Error Get Distinct Barang: " + ex.Message);
                    }
                }
            }
            return listBarang;
        }

        public void UpdateBarang(Barang b)
        {
            using (SqlConnection conn = DbConnection.GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand("SP_UpdateBarang", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Kode", b.KodeBarang);
                    cmd.Parameters.AddWithValue("@Cabang", b.ID_Cabang);
                    cmd.Parameters.AddWithValue("@Nama", b.NamaBarang);
                    cmd.Parameters.AddWithValue("@Kat", b.Kategori);
                    cmd.Parameters.AddWithValue("@Harga", b.Harga);
                    cmd.Parameters.AddWithValue("@Stok", b.Stok);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void DeleteBarang(string kodeBarang, int idCabang)
        {
            using (SqlConnection conn = DbConnection.GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand("SP_DeleteBarang", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Kode", kodeBarang);
                    cmd.Parameters.AddWithValue("@Cabang", idCabang);
                    
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void RestockBarang(string kodeBarang, int nambahStok)
        {
            if (nambahStok <= 0) throw new ArgumentException("Jumlah stok atau restock harus minimal 1!");

            using (SqlConnection conn = DbConnection.GetConnection())
            {
                string query = "UPDATE Barang SET Stok = Stok + @Nambah WHERE KodeBarang=@Kode";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Kode", kodeBarang);
                    cmd.Parameters.AddWithValue("@Nambah", nambahStok);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public DataTable GetDashboardData()
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = DbConnection.GetConnection())
            {
                string query = "SELECT [ID Barang], [ID Cabang], [Nama Barang], [Lokasi Cabang], [Stok Tersedia], [Barang Terjual] FROM View_DaftarBarangPerCabang";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }
            }
            return dt;
        }

        public string GetNamaBarang(string kodeBarang)
        {
            string nama = "";
            using (SqlConnection conn = DbConnection.GetConnection())
            {
                string query = "SELECT NamaBarang FROM Barang WHERE KodeBarang=@Kode";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Kode", kodeBarang);
                    conn.Open();
                    object result = cmd.ExecuteScalar();
                    if (result != null) nama = result.ToString();
                }
            }
            return nama;
        }

        public void RestockBarangDashboard(string kodeBarang, int idCabang, int jumlah)
        {
            if (jumlah <= 0) throw new ArgumentException("Jumlah stok atau restock harus minimal 1!");

            using (SqlConnection conn = DbConnection.GetConnection())
            {
                string query = "UPDATE Barang SET Stok = Stok + @Jumlah WHERE KodeBarang=@Kode AND ID_Cabang=@Cabang";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Kode", kodeBarang);
                    cmd.Parameters.AddWithValue("@Cabang", idCabang);
                    cmd.Parameters.AddWithValue("@Jumlah", jumlah);
                    conn.Open();
                    int affected = cmd.ExecuteNonQuery();
                    if(affected == 0) throw new Exception("Barang tidak ditemukan pada cabang tersebut!");
                }
            }
        }

        public void InsertCabang(string namaCabang, string alamat)
        {
            using (SqlConnection conn = DbConnection.GetConnection())
            {
                string query = "INSERT INTO Cabang (NamaCabang, Alamat) VALUES (@Nama, @Alamat)";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Nama", namaCabang);
                    cmd.Parameters.AddWithValue("@Alamat", alamat);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void DeleteCabang(int idCabang)
        {
            using (SqlConnection conn = DbConnection.GetConnection())
            {
                conn.Open();
                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        // 1. Delete related Transactions
                        string deleteTransaksi = "DELETE FROM Transaksi WHERE ID_Cabang=@ID";
                        using (SqlCommand cmd = new SqlCommand(deleteTransaksi, conn, trans))
                        {
                            cmd.Parameters.AddWithValue("@ID", idCabang);
                            cmd.ExecuteNonQuery();
                        }

                        // 2. Delete related Goods (Barang)
                        string deleteBarang = "DELETE FROM Barang WHERE ID_Cabang=@ID";
                        using (SqlCommand cmd = new SqlCommand(deleteBarang, conn, trans))
                        {
                            cmd.Parameters.AddWithValue("@ID", idCabang);
                            cmd.ExecuteNonQuery();
                        }

                        // 3. Delete related Users (Staff)
                        string deleteUsers = "DELETE FROM Users WHERE ID_Cabang=@ID";
                        using (SqlCommand cmd = new SqlCommand(deleteUsers, conn, trans))
                        {
                            cmd.Parameters.AddWithValue("@ID", idCabang);
                            cmd.ExecuteNonQuery();
                        }

                        // 4. Delete the Branch itself
                        string deleteCabang = "DELETE FROM Cabang WHERE ID_Cabang=@ID";
                        using (SqlCommand cmd = new SqlCommand(deleteCabang, conn, trans))
                        {
                            cmd.Parameters.AddWithValue("@ID", idCabang);
                            cmd.ExecuteNonQuery();
                        }

                        trans.Commit();
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        throw new Exception("Gagal menghapus cabang dan data terkait: " + ex.Message);
                    }
                }
            }
        }

        public void InsertStaff(string username, string password, int idCabang)
        {
            using (SqlConnection conn = DbConnection.GetConnection())
            {
                conn.Open();

                // Check username duplication
                string checkQuery = "SELECT COUNT(*) FROM Users WHERE Username=@User";
                using (SqlCommand checkCmd = new SqlCommand(checkQuery, conn))
                {
                    checkCmd.Parameters.AddWithValue("@User", username);
                    int count = (int)checkCmd.ExecuteScalar();
                    if (count > 0)
                    {
                        throw new Exception("Username sudah digunakan! Silakan pilih username lain.");
                    }
                }

                string query = "INSERT INTO Users (Username, Password, Role, ID_Cabang) VALUES (@User, @Pass, 'Staff', @Cabang)";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@User", username);
                    cmd.Parameters.AddWithValue("@Pass", password);
                    cmd.Parameters.AddWithValue("@Cabang", idCabang);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public DataTable GetDashboardDataStaff(int idCabang)
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = DbConnection.GetConnection())
            {
                string query = "SELECT [ID Barang], [ID Cabang], [Nama Barang], [Lokasi Cabang], [Stok Tersedia] AS [Stok], [Barang Terjual] FROM View_DaftarBarangPerCabang WHERE [ID Cabang] = @ID_Cabang";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ID_Cabang", idCabang);
                    conn.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }
            }
            return dt;
        }

        public string GetNamaBarangDiCabang(string kodeBarang, int idCabang)
        {
            string nama = "";
            using (SqlConnection conn = DbConnection.GetConnection())
            {
                string query = "SELECT NamaBarang FROM Barang WHERE KodeBarang=@Kode AND ID_Cabang=@Cabang";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Kode", kodeBarang);
                    cmd.Parameters.AddWithValue("@Cabang", idCabang);
                    conn.Open();
                    object result = cmd.ExecuteScalar();
                    if (result != null) nama = result.ToString();
                }
            }
            return nama;
        }

        public string GenerateKodeBarang()
    {
        string kodeBaru = "BRG001"; // Default jika tabel masih kosong

        using (SqlConnection conn = DbConnection.GetConnection())
        {
            // Ambil 1 kode barang terakhir (berdasarkan urutan abjad/angka menurun)
            string query = "SELECT TOP 1 KodeBarang FROM Barang WHERE KodeBarang LIKE 'BRG%' ORDER BY KodeBarang DESC";

            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                conn.Open();
                object result = cmd.ExecuteScalar();

                if (result != null && result != DBNull.Value)
                {
                    string kodeTerakhir = result.ToString();

                    // Pisahkan string "BRG" (3 huruf) dan ambil sisa angkanya
                    string angkaString = kodeTerakhir.Substring(3);

                    if (int.TryParse(angkaString, out int angka))
                    {
                        angka++; // Tambah 1
                                 // Gabungkan kembali dengan format 3 digit (contoh: dari 1 jadi "002")
                        kodeBaru = "BRG" + angka.ToString("D3");
                    }
                }
            }
        }
        return kodeBaru;
        }
    }
}