-- phpMyAdmin SQL Dump
-- version 4.1.6
-- http://www.phpmyadmin.net
--
-- Servidor: 127.0.0.1
-- Tiempo de generación: 21-10-2016 a las 16:24:01
-- Versión del servidor: 5.6.16
-- Versión de PHP: 5.5.9

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;

--
-- Base de datos: `pi`
--

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `acciones`
--

CREATE TABLE IF NOT EXISTS `acciones` (
  `Ac_ID` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `Ac_Accion` varchar(30) NOT NULL,
  `Ac_Cantidad` float NOT NULL,
  `Ac_Un_ID` int(10) unsigned DEFAULT NULL,
  PRIMARY KEY (`Ac_ID`),
  KEY `fk_Accion_Unidad` (`Ac_Un_ID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 AUTO_INCREMENT=1 ;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `romi`
--

CREATE TABLE IF NOT EXISTS `romi` (
  `Ro_ID` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `Ro_St_ID` int(10) unsigned DEFAULT NULL,
  `Ro_Nombre` varchar(30) NOT NULL,
  PRIMARY KEY (`Ro_ID`),
  KEY `fk_romi_status` (`Ro_St_ID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 AUTO_INCREMENT=1 ;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `romi_accion`
--

CREATE TABLE IF NOT EXISTS `romi_accion` (
  `RA_Ro_ID` int(10) unsigned NOT NULL DEFAULT '0',
  `RA_Ac_ID` int(10) unsigned NOT NULL DEFAULT '0',
  PRIMARY KEY (`RA_Ro_ID`,`RA_Ac_ID`),
  KEY `fk_Romi_Accion2` (`RA_Ac_ID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `status`
--

CREATE TABLE IF NOT EXISTS `status` (
  `St_ID` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `St_Estado` varchar(20) NOT NULL,
  PRIMARY KEY (`St_ID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 AUTO_INCREMENT=1 ;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `unidades`
--

CREATE TABLE IF NOT EXISTS `unidades` (
  `Un_ID` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `Un_Unidad` varchar(10) NOT NULL,
  PRIMARY KEY (`Un_ID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 AUTO_INCREMENT=1 ;

--
-- Restricciones para tablas volcadas
--

--
-- Filtros para la tabla `acciones`
--
ALTER TABLE `acciones`
  ADD CONSTRAINT `fk_Accion_Unidad` FOREIGN KEY (`Ac_Un_ID`) REFERENCES `unidades` (`Un_ID`);

--
-- Filtros para la tabla `romi`
--
ALTER TABLE `romi`
  ADD CONSTRAINT `fk_romi_status` FOREIGN KEY (`Ro_St_ID`) REFERENCES `status` (`St_ID`);

--
-- Filtros para la tabla `romi_accion`
--
ALTER TABLE `romi_accion`
  ADD CONSTRAINT `fk_Romi_Accion1` FOREIGN KEY (`RA_Ro_ID`) REFERENCES `romi` (`Ro_ID`),
  ADD CONSTRAINT `fk_Romi_Accion2` FOREIGN KEY (`RA_Ac_ID`) REFERENCES `acciones` (`Ac_ID`);

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
