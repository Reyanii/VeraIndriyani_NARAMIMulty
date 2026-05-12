README - MiMultyCBGApp

Skenario Keamanan: SQL Injection (UCP 2)
Halaman ini menjelaskan bagaimana celah keamanan SQL Injection bisa terjadi dan bagaimana cara mengatasinya dalam proyek ini.

1. Letak Kerentanan
Celah ini biasanya muncul jika kita menggabungkan input dari user langsung ke dalam string query SQL menggunakan teknik concatenation.

Contoh kode yang berbahaya:

string query = "SELECT * FROM Users WHERE Username='" + username + "' AND Password='" + password + "'";

2. Cara Serangan Bekerja
Seorang penyerang bisa memasukkan payload atau perintah khusus ke dalam kolom input.

Input Payload: ' OR '1'='1

Logika Serangan: Karena '1'='1' selalu bernilai BENAR (TRUE), database akan mengabaikan validasi password yang asli dan memberikan akses login secara paksa.

3. Solusi: Stored Procedure & Parameter
Untuk mencegah hal tersebut, proyek ini sudah menggunakan Stored Procedure dan Command Parameters.

Kode Database (SP_LoginUser):
CREATE PROCEDURE SP_LoginUser
    @Username VARCHAR(50),
    @Password VARCHAR(100)
AS
BEGIN
    SELECT UserID, Username, Role, ID_Cabang 
    FROM Users 
    WHERE Username=@Username AND Password=@Password;
END
Kode C# yang Aman:
string query = "SP_LoginUser";
using (SqlCommand cmd = new SqlCommand(query, conn))
{
    cmd.CommandType = CommandType.StoredProcedure;
    
    // Menggunakan parameter agar input user dianggap teks biasa (bukan kode)
    cmd.Parameters.AddWithValue("@Username", username);
    cmd.Parameters.AddWithValue("@Password", password);

    // Proses login...
}
Dengan teknik ini, input berbahaya seperti ' OR '1'='1 tidak akan dijalankan sebagai perintah SQL, melainkan hanya dianggap sebagai teks biasa saja, sehingga login menjadi jauh lebih aman.