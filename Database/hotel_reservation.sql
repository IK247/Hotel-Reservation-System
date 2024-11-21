-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Nov 22, 2024 at 12:11 AM
-- Server version: 10.4.28-MariaDB
-- PHP Version: 8.2.4

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `hrs_v2`
--

-- --------------------------------------------------------

--
-- Table structure for table `admins`
--

CREATE TABLE `admins` (
  `Username` varchar(50) NOT NULL,
  `Password` varchar(255) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `admins`
--

INSERT INTO `admins` (`Username`, `Password`) VALUES
('kingk', 'clowncity'),
('Samuel', 'Password123');

-- --------------------------------------------------------

--
-- Table structure for table `bookings`
--

CREATE TABLE `bookings` (
  `BookingId` int(11) NOT NULL,
  `UserId` int(11) NOT NULL,
  `RoomId` int(11) NOT NULL,
  `CheckInDate` datetime NOT NULL,
  `CheckOutDate` datetime NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `bookings`
--

INSERT INTO `bookings` (`BookingId`, `UserId`, `RoomId`, `CheckInDate`, `CheckOutDate`) VALUES
(13, 1, 7, '2023-08-24 09:51:32', '2023-08-31 09:51:32'),
(14, 2, 1, '2023-08-25 10:08:08', '2023-08-28 10:08:08');

-- --------------------------------------------------------

--
-- Table structure for table `rooms`
--

CREATE TABLE `rooms` (
  `RoomID` int(11) NOT NULL,
  `RoomType` varchar(255) NOT NULL,
  `RoomNumber` varchar(50) NOT NULL,
  `Availability` varchar(50) NOT NULL,
  `Features` varchar(255) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `rooms`
--

INSERT INTO `rooms` (`RoomID`, `RoomType`, `RoomNumber`, `Availability`, `Features`) VALUES
(1, 'Single', '101', 'Reserved', 'Designed for solo travelers or business guests'),
(2, 'Single', '102', 'Available', 'Designed for solo travelers or business guests'),
(3, 'Single', '103', 'Available', 'Designed for solo travelers or business guests'),
(4, 'Single', '104', 'Available', 'Designed for solo travelers or business guests'),
(5, 'Single', '105', 'Available', 'Designed for solo travelers or business guests'),
(6, 'Double', '201', 'Available', 'Caters to pairs of guests, like couples or business colleagues.'),
(7, 'Double', '202', 'Reserved', 'Caters to pairs of guests, like couples or business colleagues.'),
(8, 'Double', '203', 'Available', 'Caters to pairs of guests, like couples or business colleagues.'),
(9, 'Double', '204', 'Available', 'Caters to pairs of guests, like couples or business colleagues.'),
(10, 'Double', '205', 'Available', 'Caters to pairs of guests, like couples or business colleagues.'),
(11, 'Double', '206', 'Available', 'Caters to pairs of guests, like couples or business colleagues.'),
(12, 'Double', '207', 'Available', 'Caters to pairs of guests, like couples or business colleagues.'),
(13, 'Suite', '301', 'Available', 'Luxurious and spacious rooms for pairs of guests'),
(14, 'Suite', '302', 'Available', 'Luxurious and spacious rooms for pairs of guests'),
(15, 'Suite', '303', 'Available', 'Luxurious and spacious rooms for pairs of guests');

-- --------------------------------------------------------

--
-- Table structure for table `userids`
--

CREATE TABLE `userids` (
  `NextUserID` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `userids`
--

INSERT INTO `userids` (`NextUserID`) VALUES
(3);

-- --------------------------------------------------------

--
-- Table structure for table `users`
--

CREATE TABLE `users` (
  `UserId` int(11) NOT NULL,
  `EmailAddress` varchar(255) NOT NULL,
  `Name` varchar(255) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `users`
--

INSERT INTO `users` (`UserId`, `EmailAddress`, `Name`) VALUES
(0, 'ik.yolo223@gmail.com', 'ik smith'),
(1, 'inwakanma247@gmail.com', 'lebron james'),
(2, 'ik.wotpc@gmail.com', 'lebron james senior');

--
-- Indexes for dumped tables
--

--
-- Indexes for table `admins`
--
ALTER TABLE `admins`
  ADD PRIMARY KEY (`Username`);

--
-- Indexes for table `bookings`
--
ALTER TABLE `bookings`
  ADD PRIMARY KEY (`BookingId`),
  ADD KEY `UserId` (`UserId`),
  ADD KEY `RoomId` (`RoomId`);

--
-- Indexes for table `rooms`
--
ALTER TABLE `rooms`
  ADD PRIMARY KEY (`RoomID`);

--
-- Indexes for table `users`
--
ALTER TABLE `users`
  ADD PRIMARY KEY (`UserId`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `bookings`
--
ALTER TABLE `bookings`
  MODIFY `BookingId` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=15;

--
-- Constraints for dumped tables
--

--
-- Constraints for table `bookings`
--
ALTER TABLE `bookings`
  ADD CONSTRAINT `bookings_ibfk_1` FOREIGN KEY (`UserId`) REFERENCES `users` (`UserId`),
  ADD CONSTRAINT `bookings_ibfk_2` FOREIGN KEY (`RoomId`) REFERENCES `rooms` (`RoomID`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
