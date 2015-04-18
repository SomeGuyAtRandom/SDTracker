USE [SignalDB];

IF OBJECT_ID('createTblRoles', 'P') IS NOT NULL
DROP PROCEDURE createTblRoles;

GO


IF OBJECT_ID('addToTblRoles', 'P') IS NOT NULL
DROP PROCEDURE addToTblRoles;

GO

CREATE PROC createTblRoles
AS
BEGIN

IF OBJECT_ID('dbo.Roles', 'U') IS NOT NULL
DROP TABLE dbo.Roles;

CREATE TABLE Roles
(
	Id int IDENTITY PRIMARY KEY,
	RoleType nvarchar(100) not null, 
	unique(RoleType)
);

END


GO

CREATE PROC addToTblRoles
AS
BEGIN

SET IDENTITY_INSERT Roles ON;

INSERT INTO Roles (Id,RoleType) values
(1,'Admin'),
(2,'Guest'),
(3,'Design Engineer'),
(4,'Head Engineer');

SET IDENTITY_INSERT Roles OFF;

END

GO

EXEC createTblRoles
GO

EXEC addToTblRoles
GO


DROP PROCEDURE createTblRoles
GO

DROP PROCEDURE addToTblRoles
GO


SELECT * FROM Roles;
