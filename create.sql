-- --------------------------------------------------------
-- Hôte :                        127.0.0.1
-- Version du serveur:           5.7.24 - MySQL Community Server (GPL)
-- SE du serveur:                Win64
-- HeidiSQL Version:             9.5.0.5332
-- --------------------------------------------------------

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;


-- Listage de la structure de la base pour complotdb
DROP DATABASE IF EXISTS `complotdb`;
CREATE DATABASE IF NOT EXISTS `complotdb` /*!40100 DEFAULT CHARACTER SET latin1 */;
USE `complotdb`;

-- Listage de la structure de la table complotdb. complot
DROP TABLE IF EXISTS `complot`;
CREATE TABLE IF NOT EXISTS `complot` (
  `Id` int(11) NOT NULL,
  `UserId` int(11) NOT NULL,
  `Name` varchar(255) NOT NULL,
  `Description` varchar(1500) NOT NULL,
  `Public` bit(1) NOT NULL,
  `Rate` int(11) DEFAULT NULL,
  `Lattitude` double NOT NULL,
  `Longitude` double NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `UserId` (`UserId`),
  CONSTRAINT `complot_ibfk_1` FOREIGN KEY (`UserId`) REFERENCES `users` (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- Les données exportées n'étaient pas sélectionnées.
-- Listage de la structure de la table complotdb. complotgenre
DROP TABLE IF EXISTS `complotgenre`;
CREATE TABLE IF NOT EXISTS `complotgenre` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `GenreId` int(11) NOT NULL,
  `ComplotId` int(11) NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `GenreId` (`GenreId`),
  KEY `ComplotId` (`ComplotId`),
  CONSTRAINT `complotgenre_ibfk_1` FOREIGN KEY (`GenreId`) REFERENCES `genre` (`Id`),
  CONSTRAINT `complotgenre_ibfk_2` FOREIGN KEY (`ComplotId`) REFERENCES `complot` (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- Les données exportées n'étaient pas sélectionnées.
-- Listage de la structure de la table complotdb. genre
DROP TABLE IF EXISTS `genre`;
CREATE TABLE IF NOT EXISTS `genre` (
  `Id` int(11) NOT NULL,
  `Name` varchar(255) NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- Les données exportées n'étaient pas sélectionnées.
-- Listage de la structure de la table complotdb. users
DROP TABLE IF EXISTS `users`;
CREATE TABLE IF NOT EXISTS `users` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Token` longtext COLLATE utf8mb4_bin,
  `Email` longtext COLLATE utf8mb4_bin,
  `Username` longtext COLLATE utf8mb4_bin,
  `Password` longtext COLLATE utf8mb4_bin,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_bin;

-- Les données exportées n'étaient pas sélectionnées.
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IF(@OLD_FOREIGN_KEY_CHECKS IS NULL, 1, @OLD_FOREIGN_KEY_CHECKS) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
