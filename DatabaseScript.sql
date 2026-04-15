-- Buat Database
CREATE DATABASE MIMultiCabangDB;
GO

USE MIMultiCabangDB;
GO

-- 1. Tabel Cabang (Buat pertama karena akan direferensikan oleh tabel lain)
CREATE TABLE Cabang (
    ID_Cabang INT IDENTITY(1,1) PRIMARY KEY,
    NamaCabang VARCHAR(100) NOT NULL,
    Alamat TEXT
);
GO

-- 2. Tabel Users
CREATE TABLE Users (
    UserID INT IDENTITY(1,1) PRIMARY KEY,
    Username VARCHAR(50) NOT NULL UNIQUE,
    Password VARCHAR(100) NOT NULL,
    Role VARCHAR(20) NOT NULL CHECK (Role IN ('Admin', 'Staff')),
    ID_Cabang INT, -- Bisa NULL jika Admin berpusat, atau sesuaikan kebutuhan
    FOREIGN KEY (ID_Cabang) REFERENCES Cabang(ID_Cabang)
);
GO

-- 3. Tabel Barang
CREATE TABLE Barang (
    KodeBarang VARCHAR(20) PRIMARY KEY,
    NamaBarang VARCHAR(100) NOT NULL,
    Kategori VARCHAR(50),
    Harga DECIMAL(18, 2) NOT NULL,
    Stok INT NOT NULL DEFAULT 0
);
GO

-- 4. Tabel Transaksi
CREATE TABLE Transaksi (
    ID_Transaksi INT IDENTITY(1,1) PRIMARY KEY,
    ID_Cabang INT NOT NULL,
    UserID INT NOT NULL,
    KodeBarang VARCHAR(20) NOT NULL,
    QtyTerjual INT NOT NULL,
    TotalHarga DECIMAL(18, 2) NOT NULL,
    TanggalTransaksi DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (ID_Cabang) REFERENCES Cabang(ID_Cabang),
    FOREIGN KEY (UserID) REFERENCES Users(UserID),
    FOREIGN KEY (KodeBarang) REFERENCES Barang(KodeBarang)
);
GO

-- ==========================================
-- Insert Data Dummy untuk Testing Awal
-- ==========================================
INSERT INTO Cabang (NamaCabang, Alamat) VALUES ('Pusat Jakarta', 'Jl. Sudirman No 1');
INSERT INTO Cabang (NamaCabang, Alamat) VALUES ('Cabang Bandung', 'Jl. Asia Afrika No 2');

-- Akun Default Admin Pusat
INSERT INTO Users (Username, Password, Role, ID_Cabang) 
VALUES ('admin', 'admin123', 'Admin', 1);

-- Akun Default Staff Cabang
INSERT INTO Users (Username, Password, Role, ID_Cabang) 
VALUES ('staff1', 'staff123', 'Staff', 2);
