GO 
CREATE DATABASE QuanLySieuThi
USE QuanLySieuThi

go
CREATE TABLE NhaCungCap(
	MaNCC VARCHAR(10) PRIMARY KEY,
	TenNCC NVARCHAR(50),
	DiaChi NVARCHAR(20),
	SDT VARCHAR(11)
)

GO
CREATE TABLE NguoiQuanLy(
	MaNQL VARCHAR(10) PRIMARY KEY,
	TenNQL NVARCHAR(50),
	DiaChi NVARCHAR(20)
)

GO
CREATE TABLE GianHang(
	MaGH VARCHAR(10) PRIMARY KEY,
	TenGH NVARCHAR(20),
	ViTri NVARCHAR(10),
	MaNQL VARCHAR(10),
	FOREIGN KEY (MaNQL) REFERENCES dbo.NguoiQuanLy(MaNQL)
)

GO
CREATE TABLE LoaiHang (
	MaLH VARCHAR(10) PRIMARY KEY,
	TenLH NVARCHAR(20),
	SoLuong INT,
	NSX DATE,
	MaGH VARCHAR(10),
	FOREIGN KEY (MaGH) REFERENCES dbo.GianHang(MaGH)
)

GO
CREATE TABLE KhachHang (
	MaKH VARCHAR(10) PRIMARY KEY,
	TenKH NVARCHAR(50),
	DiaChi NVARCHAR(20),
	SDT VARCHAR(11)
)


GO
CREATE TABLE NhanVien(
	MaNV VARCHAR(10) PRIMARY KEY,
	TenNV NVARCHAR(50),
	NgaySinh DATE,
	QueQuan NVARCHAR(20),
	SDT VARCHAR(11),
	Luong INT,
	MaGH VARCHAR(10)
	FOREIGN KEY (MaGH) REFERENCES dbo.GianHang(MaGH)
)

GO
CREATE TABLE HopDong(
	MaHD VARCHAR(10) PRIMARY KEY,
	NgayKi DATE,
	GiaLH INT,
	MaNV VARCHAR(10),
	MaKH VARCHAR(10),
	FOREIGN KEY (MaNV) REFERENCES dbo.NhanVien(MaNV),
	FOREIGN KEY (MaKH) REFERENCES dbo.KhachHang(MaKH)
)

GO
CREATE TABLE ChiTietHopDong(
	MaHD VARCHAR(10),
	MaLH VARCHAR(10),
	SoLuong INT,
	CONSTRAINT PC_MAHD_MALH_PK PRIMARY KEY(MaHD,MaLH),
	CONSTRAINT PC_MAHD_FK FOREIGN KEY (MaHD) REFERENCES dbo.HopDong(MaHD),
	CONSTRAINT PC_MALH_FK FOREIGN KEY (MaLH) REFERENCES dbo.LoaiHang(MaLH)
)

GO
CREATE TABLE CungCap(
	MaNCC VARCHAR(10),
	MaLH VARCHAR(10),
	Gia INT,
	CONSTRAINT PC_MANCC_MALH_PK PRIMARY KEY(MaNCC,MaLH),
	CONSTRAINT PC_MANCC_FK FOREIGN KEY(MaNCC) REFERENCES dbo.NhaCungCap(MaNCC),
	CONSTRAINT PC_MA_LH_FK FOREIGN KEY(MaLH) REFERENCES dbo.LoaiHang(MaLH)
)