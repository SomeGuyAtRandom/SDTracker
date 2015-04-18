USE [SignalDB];


IF OBJECT_ID('createTblUserRoles', 'P') IS NOT NULL
DROP PROCEDURE createTblUserRoles;

GO

IF OBJECT_ID('addToUserRoles', 'P') IS NOT NULL
DROP PROCEDURE addToUserRoles;

GO

CREATE PROC createTblUserRoles
AS
BEGIN

IF OBJECT_ID('dbo.UserRoles', 'U') IS NOT NULL
DROP TABLE dbo.UserRoles;

CREATE TABLE UserRoles
(
	UserId int FOREIGN KEY REFERENCES UserPasswords(Id) default null,
	RoleId int FOREIGN KEY REFERENCES Roles(Id) default null,
);
END

GO

CREATE PROC addToUserRoles
AS
BEGIN
INSERT INTO UserRoles(UserId,RoleId)
SELECT Id,1 FROM UserPasswords;

INSERT INTO UserRoles(UserId,RoleId)
SELECT Id,2 FROM UserPasswords;

INSERT INTO UserRoles(UserId,RoleId)
SELECT Id,3 FROM UserPasswords;

INSERT INTO UserRoles(UserId,RoleId)
SELECT Id, 4 FROM UserPasswords;

END
GO
EXEC createTblUserRoles
GO
EXEC createTblUserRoles
GO
DROP PROCEDURE createTblUserRoles;
GO
DROP PROCEDURE addToUserRoles;
GO

SELECT * FROM UserRoles;