USE [SignalDB];
-- This script will create the database from scratch.

CREATE TABLE UserPasswords
(
	Id int primary key identity,
	UserName nvarchar(30) not null,
	Password nvarchar(30) not null,
	RememberMe bit,
	IsDisabled bit,
	DateCreated DateTime not null,
	DateUpdated DateTime not null,
	DateAccessed DateTime not null,
	unique(UserName)

)

GO

CREATE TABLE Roles
(
	Id int IDENTITY PRIMARY KEY,
	RoleType nvarchar(100) not null, 
	unique(RoleType)
)

GO


CREATE TABLE Districts
(
	Id int IDENTITY PRIMARY KEY,
	Name nvarchar(20),
	Code nvarchar(4),
	DateCreated DateTime not null, 
	DateUpdated DateTime not null
)

GO

SET IDENTITY_INSERT Districts ON
GO

insert into Districts (Id,Name,Code,DateCreated,DateUpdated)  
values ( 1,'Hollywood','H',GETDATE(), GETDATE()),
(20,'Central','C',GETDATE(), GETDATE()),
(22,'Southern','S',GETDATE(), GETDATE()),
(23,'Western','W',GETDATE(), GETDATE()),
(100,'ATSAC','NULL',GETDATE(), GETDATE()),
(501,'Culver City','CC',GETDATE(), GETDATE()),
(502,'East Valley','EV',GETDATE(), GETDATE()),
(505,'West Valley','WV',GETDATE(), GETDATE())

GO

SET IDENTITY_INSERT Districts OFF

GO

CREATE TABLE JobTypes
(
	Id int IDENTITY PRIMARY KEY,
	Name nvarchar(255),
	JobCode nvarchar(20),
	DateCreated DateTime not null, 
	DateUpdated DateTime not null
)

GO

CREATE TABLE RequirementType
(
	Id int primary key identity,
	RequirementName nvarchar(255),
	DateCreated DateTime,
	DateUpdated DateTime 
)

GO

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
)

GO

CREATE TABLE Projects
(
	Id int primary key identity,
	Location nvarchar(80), 
	DistrictId int FOREIGN KEY REFERENCES Districts(Id) default null,
	CurrRemark nvarchar(80),
	Rush bit,
	ProjNo nvarchar(20),
	FiveDigit nvarchar(10),
	CDs nvarchar(16), 
	JobTypeId int FOREIGN KEY REFERENCES JobTypes(Id) default null,
	HeadEngineerId int FOREIGN KEY REFERENCES Engineers(Id) default null,
	DesignEngineerId int FOREIGN KEY REFERENCES Engineers(Id) default null,
	CurrentComment nvarchar(255),
	DateAssigned datetime,
	StartDate datetime,
	DateCreated DateTime, 
	DateUpdated DateTime
	
)

GO


CREATE TABLE Places
(
	Id int IDENTITY PRIMARY KEY,
	FiveDigit nvarchar(10),
	DistrictId int FOREIGN KEY REFERENCES Districts(Id) default null,
	PlaceLocation nvarchar(80),
	DateCreated DateTime not null, 
	DateUpdated DateTime not null,
	unique(FiveDigit)
)

GO

CREATE TABLE Requirements
(
	Id int primary key identity,
	RequirementId int FOREIGN KEY REFERENCES RequirementType(Id) default 0,
	ProjectId int FOREIGN KEY REFERENCES Projects(Id) default 0,
	Required bit,
	StartDate datetime,
	FinishDate datetime,
	CurrentComment nvarchar(255),
	DateCreated DateTime, 
	DateUpdated DateTime 

)

GO


CREATE TABLE UserRoles
(
	UserId int FOREIGN KEY REFERENCES UserPasswords(Id) default null,
	RoleId int FOREIGN KEY REFERENCES Roles(Id) default null,
)

GO
SELECT * FROM INFORMATION_SCHEMA.TABLES

GO
