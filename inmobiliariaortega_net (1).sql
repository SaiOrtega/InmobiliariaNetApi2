-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Servidor: 127.0.0.1
-- Tiempo de generación: 10-11-2023 a las 01:09:56
-- Versión del servidor: 10.4.28-MariaDB
-- Versión de PHP: 8.2.4

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Base de datos: `inmobiliariaortega_net`
--

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `contrato`
--

CREATE TABLE `contrato` (
  `Id` int(11) NOT NULL,
  `InmuebleId` int(11) NOT NULL,
  `InquilinoId` int(11) NOT NULL,
  `FechaInicio` date NOT NULL,
  `FechaFinal` date NOT NULL,
  `MontoMensual` decimal(10,0) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `contrato`
--

INSERT INTO `contrato` (`Id`, `InmuebleId`, `InquilinoId`, `FechaInicio`, `FechaFinal`, `MontoMensual`) VALUES
(34, 1, 1, '2023-04-09', '2023-12-31', 3333),
(37, 4, 1, '2023-05-18', '2024-11-26', 99999999),
(59, 7, 2, '2023-07-09', '2024-08-24', 170000);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `inmueble`
--

CREATE TABLE `inmueble` (
  `Id` int(11) NOT NULL,
  `Direccion` varchar(200) NOT NULL,
  `Uso` int(11) NOT NULL,
  `Tipo` int(11) NOT NULL,
  `CantidadDeAmbientes` int(11) NOT NULL,
  `Latitud` decimal(11,0) NOT NULL,
  `Longitud` decimal(11,0) NOT NULL,
  `Superficie` decimal(11,0) NOT NULL,
  `Precio` decimal(11,0) NOT NULL,
  `PropietarioId` int(11) NOT NULL,
  `Estado` tinyint(1) NOT NULL,
  `image` varchar(100) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `inmueble`
--

INSERT INTO `inmueble` (`Id`, `Direccion`, `Uso`, `Tipo`, `CantidadDeAmbientes`, `Latitud`, `Longitud`, `Superficie`, `Precio`, `PropietarioId`, `Estado`, `image`) VALUES
(1, 'lavalle 647', 1, 0, 3, 21, 2, 0, 23, 7, 1, 'image1.jpg'),
(4, 'junin 786', 0, 3, 4, 2323, 4324, 3524, 4, 7, 1, 'image2.jpg'),
(5, 'Colon 340', 1, 5, 2, 0, 0, 0, 20000, 8, 1, 'image3.jpg'),
(6, 'Mitre 800', 0, 0, 2, 0, 0, 0, 15000, 8, 0, 'image4.jpg'),
(7, 'Salta 325', 0, 0, 3, 0, 0, 0, 170000, 9, 1, 'image5.jpg'),
(8, 'Lavalle 450', 0, 1, 2, 0, 0, 0, 25000, 9, 1, 'image6.jpg'),
(9, 'Belgrano 218', 0, 0, 5, 0, 0, 0, 90000, 9, 1, 'image7.jpg'),
(10, 'Chaco 320', 1, 0, 2, 0, 0, 0, 60000, 7, 0, '03bdde7b-778e-4696-9b5c-b92b30eb02e6_campeones.jpg'),
(35, 'Belgrano 10000', 1, 1, 2, 0, 0, 0, 800000, 8, 0, '184262fa-0ebf-48e5-8c58-1bd2d5e425f2.jpg'),
(48, 'salta 1234', 0, 0, 3, 0, 0, 0, 800000, 9, 1, '4a9fed4a-a432-4b75-bfa6-f4172a6ee441.jpg');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `inquilino`
--

CREATE TABLE `inquilino` (
  `Id` int(11) NOT NULL,
  `Dni` varchar(45) NOT NULL,
  `Nombre` varchar(45) NOT NULL,
  `Apellido` varchar(45) NOT NULL,
  `Direccion` varchar(45) NOT NULL,
  `Telefono` varchar(45) NOT NULL,
  `Email` varchar(45) NOT NULL,
  `Nacimiento` datetime NOT NULL,
  `NombreGarante` varchar(50) NOT NULL,
  `TelefonoGarante` varchar(100) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `inquilino`
--

INSERT INTO `inquilino` (`Id`, `Dni`, `Nombre`, `Apellido`, `Direccion`, `Telefono`, `Email`, `Nacimiento`, `NombreGarante`, `TelefonoGarante`) VALUES
(1, '32456765', 'Pedro', 'perez', 'falsa 2345', '2321', 'dsf@sdf.com', '2023-03-01 16:33:00', 'pocho', '123'),
(2, '25340691', 'Mario', 'Luna', 'Av.Siempre Viva 555', '2664253411', 'luna@mail.com', '2023-06-09 21:02:48', 'Lucero Roberto', '2664851422');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `pago`
--

CREATE TABLE `pago` (
  `Id` int(11) NOT NULL,
  `ContratoId` int(11) NOT NULL,
  `Monto` decimal(10,0) NOT NULL,
  `FechaPago` date DEFAULT NULL,
  `Numero` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `pago`
--

INSERT INTO `pago` (`Id`, `ContratoId`, `Monto`, `FechaPago`, `Numero`) VALUES
(23, 34, 33, '2023-04-26', 3),
(24, 34, 23, '2023-04-20', 4),
(26, 59, 170000, '2020-08-10', 1),
(27, 59, 170000, '2020-09-10', 2),
(28, 59, 170000, '2020-10-10', 3);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `propietario`
--

CREATE TABLE `propietario` (
  `Id` int(11) NOT NULL,
  `Dni` varchar(200) NOT NULL,
  `Nombre` varchar(45) NOT NULL,
  `Apellido` varchar(45) NOT NULL,
  `Telefono` varchar(45) NOT NULL,
  `Email` varchar(45) NOT NULL,
  `Nacimiento` datetime NOT NULL,
  `Contraseña` varchar(100) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `propietario`
--

INSERT INTO `propietario` (`Id`, `Dni`, `Nombre`, `Apellido`, `Telefono`, `Email`, `Nacimiento`, `Contraseña`) VALUES
(7, '54345', 'basty', 'Oper', '26555598', 'basty@mail.com', '2014-05-08 12:12:06', '1xWPXpWVqeUJPsIjtUOLI8ECkOK5Nhcneq7UfRTcO/Y='),
(8, '23492012', 'Juan', 'Perez', '2664553447', 'juan@mail.com', '2023-06-09 20:57:45', 'GAKKw6Co5EiIGNiZC1OfQC6offL+e8CoEs3SX0LIrHA='),
(9, '174958', 'Sonias', 'Lucero', '266485417', 'sonia@mail.com', '2023-06-09 21:01:04', 'GAKKw6Co5EiIGNiZC1OfQC6offL+e8CoEs3SX0LIrHA=');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `tipo`
--

CREATE TABLE `tipo` (
  `Id` int(11) NOT NULL,
  `NombreTipo` varchar(100) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `tipo`
--

INSERT INTO `tipo` (`Id`, `NombreTipo`) VALUES
(0, 'Casa'),
(1, 'Departamento'),
(2, 'Local'),
(3, 'Deposito'),
(5, 'Salon');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `uso`
--

CREATE TABLE `uso` (
  `Id` int(11) NOT NULL,
  `NombreUso` varchar(50) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `uso`
--

INSERT INTO `uso` (`Id`, `NombreUso`) VALUES
(0, 'Residencial'),
(1, 'Comercial');

--
-- Índices para tablas volcadas
--

--
-- Indices de la tabla `contrato`
--
ALTER TABLE `contrato`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `InquilinoId` (`InquilinoId`),
  ADD KEY `InmuebleId` (`InmuebleId`);

--
-- Indices de la tabla `inmueble`
--
ALTER TABLE `inmueble`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `PropietarioId` (`PropietarioId`),
  ADD KEY `Uso` (`Uso`),
  ADD KEY `PropietarioId_2` (`PropietarioId`),
  ADD KEY `Tipo` (`Tipo`) USING BTREE;

--
-- Indices de la tabla `inquilino`
--
ALTER TABLE `inquilino`
  ADD PRIMARY KEY (`Id`);

--
-- Indices de la tabla `pago`
--
ALTER TABLE `pago`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `ContratoId` (`ContratoId`);

--
-- Indices de la tabla `propietario`
--
ALTER TABLE `propietario`
  ADD PRIMARY KEY (`Id`);

--
-- Indices de la tabla `tipo`
--
ALTER TABLE `tipo`
  ADD PRIMARY KEY (`Id`);

--
-- Indices de la tabla `uso`
--
ALTER TABLE `uso`
  ADD PRIMARY KEY (`Id`);

--
-- AUTO_INCREMENT de las tablas volcadas
--

--
-- AUTO_INCREMENT de la tabla `contrato`
--
ALTER TABLE `contrato`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=60;

--
-- AUTO_INCREMENT de la tabla `inmueble`
--
ALTER TABLE `inmueble`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=49;

--
-- AUTO_INCREMENT de la tabla `inquilino`
--
ALTER TABLE `inquilino`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- AUTO_INCREMENT de la tabla `pago`
--
ALTER TABLE `pago`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=29;

--
-- AUTO_INCREMENT de la tabla `propietario`
--
ALTER TABLE `propietario`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=12;

--
-- AUTO_INCREMENT de la tabla `tipo`
--
ALTER TABLE `tipo`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=6;

--
-- AUTO_INCREMENT de la tabla `uso`
--
ALTER TABLE `uso`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- Restricciones para tablas volcadas
--

--
-- Filtros para la tabla `contrato`
--
ALTER TABLE `contrato`
  ADD CONSTRAINT `contrato_ibfk_2` FOREIGN KEY (`InquilinoId`) REFERENCES `inquilino` (`Id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  ADD CONSTRAINT `contrato_ibfk_3` FOREIGN KEY (`InmuebleId`) REFERENCES `inmueble` (`Id`) ON DELETE NO ACTION ON UPDATE NO ACTION;

--
-- Filtros para la tabla `inmueble`
--
ALTER TABLE `inmueble`
  ADD CONSTRAINT `inmueble_ibfk_1` FOREIGN KEY (`Tipo`) REFERENCES `tipo` (`Id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  ADD CONSTRAINT `inmueble_ibfk_2` FOREIGN KEY (`PropietarioId`) REFERENCES `propietario` (`Id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  ADD CONSTRAINT `inmueble_ibfk_3` FOREIGN KEY (`Uso`) REFERENCES `uso` (`Id`) ON DELETE NO ACTION ON UPDATE NO ACTION;

--
-- Filtros para la tabla `pago`
--
ALTER TABLE `pago`
  ADD CONSTRAINT `pago_ibfk_1` FOREIGN KEY (`ContratoId`) REFERENCES `contrato` (`Id`) ON DELETE NO ACTION ON UPDATE NO ACTION;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
