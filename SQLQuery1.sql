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

INSERT INTO Roles (RoleName) VALUES 
('Guest'),
('Member'),
('Koi Breeder'),
('Staff'),
('Manager');
