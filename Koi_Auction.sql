-- Tạo DB
CREATE DATABASE  IF NOT EXISTS `koi`
USE `koi`;

-- Tạo table
CREATE TABLE `Users` (
  `user_id` int PRIMARY KEY AUTO_INCREMENT,
  `fullname` varchar(50),
  `password` varchar(255),
  `email` varchar(100),
  `address` varchar(150),
  `phone_number` varchar(20),
  `created_at` datetime,
  `updated_at` datetime,
  `is_deleted` tinyint(1)
);
ALTER TABLE Users CONVERT TO CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;

CREATE TABLE `KoiFish` (
  `koi_id` int PRIMARY KEY AUTO_INCREMENT,
  `owner_id` int,
  `name` varchar(50),
  `age` int,
  `size` decimal(10,2),
  `health_status` varchar(50),
  `description` varchar(100),
  `created_at` datetime,
  `updated_at` datetime,
  `is_deleted` tinyint(1)
);

CREATE TABLE `Auctions` (
  `auction_id` int PRIMARY KEY AUTO_INCREMENT,
  `koi_id` int,
  `start_price` decimal(10,2),
  `start_time` datetime,
  `end_time` datetime,
  `winner_id` int,
  `current_price` decimal(10,2)
);

CREATE TABLE `Bids` (
  `bid_id` int PRIMARY KEY AUTO_INCREMENT,
  `auction_id` int,
  `bidder_id` int,
  `bid_amount` decimal(10,2),
  `bid_time` datetime
);

CREATE TABLE `Transactions` (
  `transaction_id` int PRIMARY KEY AUTO_INCREMENT,
  `auction_id` int,
  `buyer_id` int,
  `seller_id` int,
  `transaction_amount` decimal(10,2),
  `transaction_fee` decimal(10,2),
  `payment_date` datetime
);

CREATE TABLE `BlogPosts` (
  `post_id` int PRIMARY KEY AUTO_INCREMENT,
  `author_id` int,
  `title` varchar(255),
  `content` text,
  `created_at` datetime
);