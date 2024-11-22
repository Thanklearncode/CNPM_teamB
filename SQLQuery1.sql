-- Tạo Database
CREATE DATABASE AuctionKoi;
GO

USE AuctionKoi;
GO

-- Tạo bảng Users
CREATE TABLE Users (
    UserID INT PRIMARY KEY,
    Username NVARCHAR(50) NOT NULL,
    PasswordHash NVARCHAR(100),
    FullName NVARCHAR(100),
    Email NVARCHAR(100),
    PhoneNumber NVARCHAR(15),
    RoleID INT,
    CreatedAt DATETIME
);

-- Tạo bảng Roles
CREATE TABLE Roles (
    RoleID INT PRIMARY KEY,
    RoleName NVARCHAR(50)
);

-- Tạo bảng KoiFish
CREATE TABLE KoiFish (
    KoiID INT PRIMARY KEY,
    KoiName NVARCHAR(50) NOT NULL,
    Age INT,
    Breed NVARCHAR(50),
    Description NVARCHAR(255),
    ImageUrl NVARCHAR(255),
    OwnerID INT,
    Length FLOAT,
    KoiSex NVARCHAR(10)
);

-- Tạo bảng Auctions
CREATE TABLE Auctions (
    AuctionID INT PRIMARY KEY,
    KoiID INT,
    StartPrice FLOAT,
    StartTime DATETIME,
    EndTime DATETIME,
    Status NVARCHAR(50),
    CurrentPrice FLOAT,
    WinnerId INT NULL,
    Method NVARCHAR(50),
    FOREIGN KEY (KoiID) REFERENCES KoiFish(KoiID)
);

-- Tạo bảng TransactionHistory
CREATE TABLE TransactionHistory (
    TransactionID INT PRIMARY KEY,
    AuctionID INT,
    BuyerID INT,
    TotalAmount FLOAT,
    TransactionDate DATETIME,
    AuctionType NVARCHAR(50),
    FOREIGN KEY (AuctionID) REFERENCES Auctions(AuctionID)
);

-- Tạo bảng Bids
CREATE TABLE Bids (
    BidID INT PRIMARY KEY,
    AuctionID INT,
    UserID INT,
    BidAmount FLOAT,
    BidTime DATETIME,
    FOREIGN KEY (AuctionID) REFERENCES Auctions(AuctionID),
    FOREIGN KEY (UserID) REFERENCES Users(UserID)
);

-- Tạo bảng Employees
CREATE TABLE Employees (
    EmployeeID INT PRIMARY KEY,
    UserID INT,
    Position NVARCHAR(50),
    HireDate DATETIME,
    RoleID INT,
    FOREIGN KEY (UserID) REFERENCES Users(UserID),
    FOREIGN KEY (RoleID) REFERENCES Roles(RoleID)
);

-- Dữ liệu mẫu cho bảng Roles
INSERT INTO Roles (RoleID, RoleName)
VALUES
(1, 'Guest'),
(2, 'Member'),
(3, 'Koi Breeder'),
(4, 'Staff'),
(5, 'Manager');

-- Dữ liệu mẫu cho bảng Users
INSERT INTO Users (UserID, Username, PasswordHash, FullName, Email, PhoneNumber, RoleID, CreatedAt)
VALUES
(501, 'thanh', NULL, 'Người dùng Một', 'user1@example.com', '0123456789', 2, '2024-10-30 15:26:56.840'),
(502, 'user2', 'hashpassword2', 'Người dùng Hai', 'user2@example.com', '0987654321', 2, '2024-10-30 15:26:56.840'),
(503, 'user3', 'hashpassword3', 'Người dùng Ba', 'user3@example.com', '1234567890', 2, '2024-10-30 15:26:56.840'),
(506, 'admin', 'Tthvtcx159', 'Admin User', 'thanhh1005@gmail.com', '0976543210', 4, '2024-11-18 03:03:42.967');

-- Dữ liệu mẫu cho bảng KoiFish
INSERT INTO [AuctionKoi].[dbo].[KoiFish] ([KoiName], [Age], [Breed], [Description], [ImageUrl], [OwnerID], [Length], [KoiSex])
VALUES
('Haruto', 3, 'Kohaku', 'Koi Kohaku co than trang voi diem do tuoi', 'https://www.kodamakoifarm.com/wp-content/uploads/2024/11/w1028s005.jpg', 509, 45.00, 'Male'),
('Aiko', 2, 'Sanke', 'Koi Sanke voi mau sac do trang va diem den', 'https://www.kodamakoifarm.com/wp-content/uploads/2024/11/w1106a012.jpg', 501, 38.00, 'Female'),
('Takashi', 1, 'Utsuri', 'Koi Utsuri co mau den lan voi trang', 'https://www.kodamakoifarm.com/wp-content/uploads/2024/10/w1025s062-re.jpg', 502, 41.00, 'Male'),
('Mizuki', 4, 'Bekko', 'Koi Bekko co hoa tiet trang den doc dao', 'https://www.kodamakoifarm.com/wp-content/uploads/2024/04/w0411t023.jpg', 512, 39.00, 'Female'),
('Kaoru', 5, 'Ginrin', 'Koi Ginrin anh bac long lanh trong nuoc', 'https://www.kodamakoifarm.com/wp-content/uploads/2024/11/w1028s034.jpg', 513, 47.50, 'Male'),
('Yumi', 2, 'Asagi', 'Koi Asagi co mau xanh noi bat', 'https://www.kodamakoifarm.com/wp-content/uploads/2024/04/w0411t019.jpg', 514, 44.00, 'Female'),
('Hikaru', 3, 'Chagoi', 'Koi Chagoi voi sac nau thanh lich', 'https://www.kodamakoifarm.com/wp-content/uploads/2024/04/w0411t004.jpg', 515, 50.00, 'Male'),
('Ren', 1, 'Koromo', 'Koi Koromo co hoa tiet doc dao voi mau xanh', 'https://www.kodamakoifarm.com/wp-content/uploads/2024/10/w1025s060-re.jpg', 506, 43.00, 'Female'),
('Rika', 6, 'Doitsu', 'Koi Doitsu voi than min khong vay', 'https://www.kodamakoifarm.com/wp-content/uploads/2024/10/w1025s045-re.jpg', 505, 52.00, 'Female'),
('Haru', 2, 'Kumonryu', 'Koi Kumonryu co hoa tiet den trang nhu rong', 'https://www.kodamakoifarm.com/wp-content/uploads/2024/04/w0411t024.jpg', 504, 46.00, 'Male'),
('Akane', 4, 'Benigoi', 'Koi Benigoi co sac do tuoi sang ruc ro', 'https://www.kodamakoifarm.com/wp-content/uploads/2024/11/w1107m009-1.jpg', 503, 49.00, 'Female'),
('Souta', 5, 'Shiro Utsuri', 'Koi Shiro Utsuri co than trang voi diem den noi bat', 'https://www.kodamakoifarm.com/wp-content/uploads/2024/04/w0411t023.jpg', 502, 48.00, 'Male'),
('Kenta', 1, 'Tancho', 'Koi Tancho co diem do noi bat tren dau', 'https://www.kodamakoifarm.com/wp-content/uploads/2024/11/w1106m005.jpg', 501, 40.50, 'Male');

-- Dữ liệu mẫu cho bảng Auctions
INSERT INTO Auctions (AuctionID, KoiID, StartPrice, StartTime, EndTime, Status, CurrentPrice, WinnerId, Method)
VALUES
(7, 108, 3000000.00, '2024-11-01 10:00:00', '2024-11-30 18:00:00', 'OPEN', 3000000.00, NULL, 'Fixed Price'),
(8, 109, 5000000.00, '2024-11-01 10:00:00', '2024-11-30 19:00:00', 'OPEN', NULL, 'One-Time Bid');

-- Dữ liệu mẫu cho bảng TransactionHistory
INSERT INTO TransactionHistory (TransactionID, AuctionID, BuyerID, TotalAmount, TransactionDate, AuctionType)
VALUES
(1, 7, 502, 3500000.00, '2024-11-18 02:16:33.240', NULL);

-- Dữ liệu mẫu cho bảng Bids
INSERT INTO Bids (BidID, AuctionID, UserID, BidAmount, BidTime)
VALUES
(5, 7, 502, 33333333.00, '2024-11-18 02:16:00.197'),
(6, 8, 502, 44444444.00, '2024-11-18 02:16:29.033');

-- Dữ liệu mẫu cho bảng Employees
INSERT INTO Employees (EmployeeID, UserID, Position, HireDate, RoleID)
VALUES
(1, 506, NULL, '2024-11-18 16:29:28.917', 4);
