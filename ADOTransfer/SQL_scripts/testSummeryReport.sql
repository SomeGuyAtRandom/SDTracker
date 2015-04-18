USE [SignalDB];

IF OBJECT_ID('spGetUserRoles', 'P') IS NOT NULL
DROP PROCEDURE spGetUserRoles;

GO

CREATE PROC spGetUserRoles  
@UserName nvarchar(30) = null
AS
BEGIN  

SELECT Roles.RoleType
FROM  Roles
INNER JOIN UserRoles
ON Roles.Id = UserRoles.RoleId
INNER JOIN UserPasswords
ON UserPasswords.Id = UserRoles.UserId
WHERE UserPasswords.UserName =@UserName

END

GO
