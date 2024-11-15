CREATE DATABASE AuctionKoi;
GO

USE AuctionKoi;
GO

CREATE TABLE Roles (
    RoleID INT PRIMARY KEY IDENTITY(1,1),
    RoleName NVARCHAR(50) NOT NULL
);

CREATE TABLE Users (
    UserID INT PRIMARY KEY IDENTITY(1,1),
    Username NVARCHAR(50) NOT NULL,
    PasswordHash NVARCHAR(255) NOT NULL,
    FullName NVARCHAR(100),
    Email NVARCHAR(100),
    PhoneNumber NVARCHAR(20),
    RoleID INT FOREIGN KEY REFERENCES Roles(RoleID),
    CreatedAt DATETIME DEFAULT GETDATE()
);

CREATE TABLE KoiFish (
    KoiID INT PRIMARY KEY IDENTITY(1,1),
    KoiName NVARCHAR(100) NOT NULL,
    Age INT,
    Breed NVARCHAR(100),
    Description NVARCHAR(255),
    ImageUrl NVARCHAR(255),
    OwnerID INT FOREIGN KEY REFERENCES Users(UserID)
);

CREATE TABLE Auctions (
    AuctionID INT PRIMARY KEY IDENTITY(1,1),
    KoiID INT FOREIGN KEY REFERENCES KoiFish(KoiID),
    StartPrice DECIMAL(18,2),
    StartTime DATETIME,
    EndTime DATETIME,
    Status NVARCHAR(50) -- Ví dụ: 'Upcoming', 'Ongoing', 'Completed'
);

CREATE TABLE Bids (
    BidID INT PRIMARY KEY IDENTITY(1,1),
    AuctionID INT FOREIGN KEY REFERENCES Auctions(AuctionID),
    UserID INT FOREIGN KEY REFERENCES Users(UserID),
    BidAmount DECIMAL(18,2),
    BidTime DATETIME DEFAULT GETDATE()
);

CREATE TABLE Employees (
    EmployeeID INT PRIMARY KEY IDENTITY(1,1),
    UserID INT FOREIGN KEY REFERENCES Users(UserID),
    Position NVARCHAR(50), -- Ví dụ: 'Staff', 'Manager'
    HireDate DATETIME DEFAULT GETDATE()
);

CREATE TABLE TransactionHistory (
    TransactionID INT PRIMARY KEY IDENTITY(1,1),
    AuctionID INT FOREIGN KEY REFERENCES Auctions(AuctionID),
    BuyerID INT FOREIGN KEY REFERENCES Users(UserID),
    TotalAmount DECIMAL(18,2),
    TransactionDate DATETIME DEFAULT GETDATE()
);
ALTER TABLE KoiFish
    ADD Length DECIMAL(5,2),
	KoiSex NVARCHAR(10); 


INSERT INTO Roles (RoleName) VALUES 
('Guest'),
('Member'),
('Koi Breeder'),
('Staff'),
('Manager');

SET IDENTITY_INSERT dbo.Auctions ON;

INSERT INTO dbo.Auctions (AuctionID, KoiID, StartTime, EndTime, StartPrice, Status)
VALUES 
(1, 101, '2024-10-30 09:00:00', '2024-10-31 18:00:00', 5000000, 'Open'),
(2, 102, '2024-10-29 10:00:00', '2024-11-01 17:00:00', 10000000, 'Closed'),
(3, 103, '2024-10-28 11:00:00', '2024-10-30 16:00:00', 7500000, 'Open');

SET IDENTITY_INSERT dbo.Auctions OFF;

SET IDENTITY_INSERT dbo.KoiFish ON;

INSERT INTO dbo.KoiFish (KoiId, KoiName, Age, Breed, Description, ImageUrl, OwnerId, Length, KoiSex)
VALUES 
(101, 'Satsuki', 3, 'Kohaku', 'Koi trắng với đốm đỏ đẹp mắt', 'http://example.com/satsuki.jpg', 501, 35.5, 'Female'),
(102, 'Mizu', 4, 'Sanke', 'Koi ba màu nổi bật', 'http://example.com/mizu.jpg', 502, 40.2, 'Male'),
(103, 'Hoshi', 2, 'Showa', 'Koi đen với đốm đỏ và trắng', 'http://example.com/hoshi.jpg', 503, 38.0, 'Female');

SET IDENTITY_INSERT dbo.KoiFish OFF;

SET IDENTITY_INSERT dbo.Users ON;
INSERT INTO dbo.Users (UserId, Username, PasswordHash, FullName, Email, PhoneNumber, RoleId, CreatedAt)
VALUES 
(501, 'thanh', '1', 'Người dùng Một', 'user1@example.com', '0123456789', 1, GETDATE()),
(502, 'user2', 'hashpassword2', 'Người dùng Hai', 'user2@example.com', '0987654321', 2, GETDATE()),
(503, 'user3', 'hashpassword3', 'Người dùng Ba', 'user3@example.com', '0112233445', 1, GETDATE());
SET IDENTITY_INSERT dbo.Users OFF;

SET IDENTITY_INSERT dbo.Bids ON;

INSERT INTO dbo.Bids (BidId, AuctionId, UserId, BidAmount, BidTime)
VALUES 
(1, 1, 501, 1100.00, GETDATE()),  
(2, 1, 502, 1150.00, GETDATE()),  
(3, 2, 503, 1600.00, GETDATE()),  
(4, 3, 501, 2100.00, GETDATE());  

SET IDENTITY_INSERT dbo.Bids OFF;


SET IDENTITY_INSERT dbo.Employees ON;

INSERT INTO dbo.Employees (EmployeeId, UserId, Position, HireDate)
VALUES 
(1, 501, 'Quản lý', GETDATE()),
(2, 502, 'Nhân viên bán hàng', GETDATE());

SET IDENTITY_INSERT dbo.Employees OFF;

SET IDENTITY_INSERT dbo.TransactionHistory ON;
INSERT INTO dbo.TransactionHistory (TransactionId, AuctionId, BuyerId, TotalAmount, TransactionDate)
VALUES 
(1, 1, 501, 1500.00, GETDATE()),
(2, 2, 502, 2000.00, GETDATE()),
(3, 3, 503, 1800.00, GETDATE());

SET IDENTITY_INSERT dbo.TransactionHistory OFF;
-- 1. Thêm các trường mới vào bảng Auctions
ALTER TABLE dbo.Auctions
ADD CurrentPrice DECIMAL(18, 2) NULL,  -- Thêm trường để lưu giá hiện tại cho đấu giá lên hoặc đấu giá xuống
    WinnerId INT NULL;                 -- Thêm trường để lưu ID người chiến thắng phiên đấu giá

-- 2. Thiết lập WinnerId làm khóa ngoại tham chiếu tới bảng Users
ALTER TABLE dbo.Auctions
ADD CONSTRAINT FK_Auctions_WinnerId FOREIGN KEY (WinnerId) REFERENCES dbo.Users(UserId);

-- 3. Thêm trường AuctionType vào bảng TransactionHistory
ALTER TABLE dbo.TransactionHistory
ADD AuctionType NVARCHAR(50) NULL;  -- Thêm trường để lưu loại đấu giá cho từng giao dịch

-- 4. Thêm CHECK CONSTRAINT cho trường Status trong bảng Auctions
ALTER TABLE dbo.Auctions
ADD CONSTRAINT CHK_Auctions_Status CHECK (Status IN ('OPEN', 'CLOSED', 'COMPLETED', 'CANCELLED'));

-- 5. Cập nhật giá trị mặc định cho CurrentPrice từ StartPrice
UPDATE dbo.Auctions
SET CurrentPrice = StartPrice
WHERE CurrentPrice IS NULL;

