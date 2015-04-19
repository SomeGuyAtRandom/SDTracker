﻿USE [SignalDB];

IF OBJECT_ID('createTblEngineers', 'P') IS NOT NULL
DROP PROCEDURE createTblEngineers;

GO

IF OBJECT_ID('addToEngineers', 'P') IS NOT NULL
DROP PROCEDURE addToEngineers;

GO

	
CREATE PROC createTblEngineers
AS
BEGIN


IF OBJECT_ID('dbo.Engineers', 'U') IS NOT NULL
DROP TABLE dbo.Engineers;


 CREATE TABLE Engineers
(
	Id int FOREIGN KEY REFERENCES UserPasswords(Id) default null,
	FirstName nvarchar(30),
	LastName nvarchar(30),
	Email nvarchar(100) not null,
	Initials nvarchar(5),
	UserName nvarchar(30) not null,
	Phone nvarchar(20),
	DateCreated DateTime not null, 
	DateUpdated DateTime not null,
	unique(Id),
	unique(UserName),
	unique(Email)
);


END
GO
	
CREATE PROC addToEngineers
AS
BEGIN

DELETE FROM Engineers;
insert into Engineers 
(Id,FirstName,LastName,Email,Initials,UserName,Phone,
 DateCreated,DateUpdated) values 
(1,'Armando','Cuevas','armando.cuevas@lacity.org','AC','acuevas','(213)928-9677',GETDATE(), GETDATE()),
(2,'Aziz','Mashian','aziz.mashian@lacity.org','AM','amashian','(213)928-9673',GETDATE(), GETDATE()),
(3,'Bernard','Hicks','bernard.hicks@lacity.org','BH','bhicks','',GETDATE(), GETDATE()),
(4,'Bruce','Wheeler','bruce.wheeler@lacity.org','BW','bwheeler','(213)928-9653',GETDATE(), GETDATE()),
(5,'Bill','Chan','bill.chan@lacity.org','BC','chan','(213)928-9640',GETDATE(), GETDATE()),
(6,'chris','Hy','chris.hy@lacity.org','CH','chrishy','',GETDATE(), GETDATE()),
(7,'C','Quan','c.quan@lacity.org','Q','cquan','',GETDATE(), GETDATE()),
(8,'David','Nolasco','david.nolasco@lacity.org','DN','dnolasco','',GETDATE(), GETDATE()),
(9,'Essie','Aghajani','essie.aghajani@lacity.org','EA','eaghajani','',GETDATE(), GETDATE()),
(10,'Ed','Chow','ed.chow@lacity.org','EC','echow','(213)928-9683',GETDATE(), GETDATE()),
(11,'Eduardo','Hermoso','eduardo.hermoso@lacity.org','EH','ehermoso','',GETDATE(), GETDATE()),
(12,'Evelinda','Pena','evelinda.pena@lacity.org','EP','epena','(213)928-9678',GETDATE(), GETDATE()),
(13,'Engdu','Workneh','engdu.workneh@lacity.org','EW','eworkneh','',GETDATE(), GETDATE()),
(14,'Fabio','Arias','fabio.arias@lacity.org','FA','farias','(213)928-9684',GETDATE(), GETDATE()),
(15,'John','Varghese','john.varghese@lacity.org','JV','jvarghese','(213)928-9681',GETDATE(), GETDATE()),
(16,'Manuel','Anaya','manuel.anaya@lacity.org','MA','manaya','(213)928-9670',GETDATE(), GETDATE()),
(17,'M','Delpasand','m.delpasand@lacity.org','MD','mdelpasand','',GETDATE(), GETDATE()),
(18,'Mike','Moshksar','mike.moshksar@lacity.org','MM','mmoshksar','(213)928-9672',GETDATE(), GETDATE()),
(19,'Ray','Andrade','andrade.ray@gmail.com','RA','randrade','(818)317-1293',GETDATE(), GETDATE()),
(20,'Ramone','Larios','ramone.larios@lacity.org','RL','rlarios','',GETDATE(), GETDATE()),
(21,'Richard','Molato','richard.molato@lacity.org','RM','rmolato','',GETDATE(), GETDATE()),
(22,'Ricardo','Rivera','ricardo.rivera@lacity.org','RR','rrivera','(213)928-9680',GETDATE(), GETDATE());

END
GO

EXEC createTblEngineers
GO

EXEC addToEngineers
GO

DROP PROCEDURE createTblEngineers
GO

DROP PROCEDURE addToEngineers
GO

SELECT * FROM Engineers;
