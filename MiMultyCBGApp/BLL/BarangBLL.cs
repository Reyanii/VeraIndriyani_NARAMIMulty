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
                string query = "INSERT INTO Barang (KodeBarang, ID_Cabang, NamaBarang, Kategori, Harga, Stok) VALUES (@Kode, @Cabang, @Nama, @Kat, @Harga, @Stok)";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
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

        public void UpdateBarang(Barang b)
        {
            using (SqlConnection conn = DbConnection.GetConnection())
            {
                string query = "UPDATE Barang SET NamaBarang=@Nama, Kategori=@Kat, Harga=@Harga, Stok=@Stok WHERE KodeBarang=@Kode";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Kode", b.KodeBarang);
                    cmd.Parameters.AddWithValue("@Nama", b.NamaBarang);
                    cmd.Parameters.AddWithValue("@Kat", b.Kategori);
                    cmd.Parameters.AddWithValue("@Harga", b.Harga);
                    cmd.Parameters.AddWithValue("@Stok", b.Stok);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void DeleteBarang(string kodeBarang)
        {
            using (SqlConnection conn = DbConnection.GetConnection())
            {
                string query = "DELETE FROM Barang WHERE KodeBarang=@Kode";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Kode", kodeBarang);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void RestockBarang(string kodeBarang, int nambahStok)
        {
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
                string query = @"
                    SELECT 
                        b.KodeBarang AS [ID Barang], 
                        c.ID_Cabang AS [ID Cabang], 
                        b.NamaBarang AS [Nama Barang], 
                        c.NamaCabang AS [Lokasi Cabang], 
                        b.Stok AS [Stok Tersedia], 
                        ISNULL(SUM(t.QtyTerjual), 0) AS [Barang Terjual]
                    FROM Barang b
                    INNER JOIN Cabang c ON b.ID_Cabang = c.ID_Cabang
                    LEFT JOIN Transaksi t ON b.KodeBarang = t.KodeBarang AND b.ID_Cabang = t.ID_Cabang
                    GROUP BY b.KodeBarang, c.ID_Cabang, b.NamaBarang, c.NamaCabang, b.Stok";
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
                string query = "DELETE FROM Cabang WHERE ID_Cabang=@ID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ID", idCabang);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void InsertStaff(string username, string password, int idCabang)
        {
            using (SqlConnection conn = DbConnection.GetConnection())
            {
                string query = "INSERT INTO Users (Username, Password, Role, ID_Cabang) VALUES (@User, @Pass, 'Staff', @Cabang)";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@User", username);
                    cmd.Parameters.AddWithValue("@Pass", password);
                    cmd.Parameters.AddWithValue("@Cabang", idCabang);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public DataTable GetDashboardDataStaff(int idCabang)
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = DbConnection.GetConnection())
            {
                string query = @"
                    SELECT 
                        b.KodeBarang AS [ID Barang], 
                        c.ID_Cabang AS [ID Cabang], 
                        b.NamaBarang AS [Nama Barang], 
                        c.NamaCabang AS [Lokasi Cabang], 
                        b.Stok AS [Stok], 
                        ISNULL(SUM(t.QtyTerjual), 0) AS [Barang Terjual]
                    FROM Barang b
                    INNER JOIN Cabang c ON b.ID_Cabang = c.ID_Cabang
                    LEFT JOIN Transaksi t ON b.KodeBarang = t.KodeBarang AND b.ID_Cabang = t.ID_Cabang AND t.ID_Cabang = @ID_Cabang
                    WHERE b.ID_Cabang = @ID_Cabang
                    GROUP BY b.KodeBarang, c.ID_Cabang, b.NamaBarang, c.NamaCabang, b.Stok";
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
    }
}
