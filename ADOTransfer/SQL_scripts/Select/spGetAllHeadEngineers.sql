USE [SignalDB];

IF OBJECT_ID('spGetAllHeadEngineers', 'P') IS NOT NULL
DROP PROCEDURE spGetAllHeadEngineers;

GO

CREATE PROC spGetAllHeadEngineers  
AS
BEGIN  
 SELECT Engineers.* FROM Engineers  
 INNER JOIN UserRoles
 ON Engineers.Id = UserRoles.UserId 
 WHERE  UserRoles.RoleId =4
END

GO



