USE [SignalDB];

IF OBJECT_ID('spGetAllRoles', 'P') IS NOT NULL
DROP PROCEDURE spGetAllRoles;

GO

CREATE PROC spGetAllRoles  
AS
BEGIN  
 SELECT * , 0 as IsMember FROM Roles     
END

GO
