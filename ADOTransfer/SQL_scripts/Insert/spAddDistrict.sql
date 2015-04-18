USE [SignalDB];

GO

IF OBJECT_ID('spAddDistrict', 'P') IS NOT NULL
DROP PROCEDURE spAddDistrict;

GO
	
CREATE PROC spAddDistrict
@Name nvarchar(20),
@Code nvarchar(4)

AS
BEGIN  
 INSERT INTO Districts (Name,Code,DateCreated,DateUpdated)  
 VALUES (@Name,@Code,GetDate(),GetDate())  
END;

GO


SELECT name AS procedure_name 
    ,SCHEMA_NAME(schema_id) AS schema_name
    ,type_desc
    ,create_date
    ,modify_date
FROM sys.procedures;
GO