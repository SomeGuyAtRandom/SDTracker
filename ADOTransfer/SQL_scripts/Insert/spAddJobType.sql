USE [SignalDB];

GO

IF OBJECT_ID('spAddJobType', 'P') IS NOT NULL
DROP PROCEDURE spAddJobType;

GO
	

CREATE PROC spAddJobType
@Name nvarchar(255),
@JobCode nvarchar(20)

AS
BEGIN  
 INSERT INTO JobTypes (Name,JobCode,DateCreated,DateUpdated)  
 VALUES (@Name,@JobCode,GetDate(),GetDate())  
END;

GO


SELECT name AS procedure_name 
    ,SCHEMA_NAME(schema_id) AS schema_name
    ,type_desc
    ,create_date
    ,modify_date
FROM sys.procedures;
GO