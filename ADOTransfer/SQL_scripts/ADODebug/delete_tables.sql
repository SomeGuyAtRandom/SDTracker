USE [SignalDB];

-- Do not drop sdn_main_projects this is required by ADO Transfer
-- Do not drop Places this is required by ADO Transfer
-- It is not necessary to drop .. 
-- sdn_main_projects
-- Windows Forms Authentication builds the following tables:
-- webpages_OAuthMembership
-- webpages_Membership
-- webpages_Roles
-- webpages_UsersInRoles
-- UserProfile (note without the webpages_ prefix..) 

-- I use my own Authentication and Membership Roles
-- TODO: Check it out: I think this is because of cookie enablement and thus the session information is stored on the sql server? 


IF OBJECT_ID('dbo.UserRoles', 'U') IS NOT NULL
DROP TABLE dbo.UserRoles;

IF OBJECT_ID('dbo.Requirements', 'U') IS NOT NULL
DROP TABLE dbo.Requirements;

IF OBJECT_ID('dbo.Places', 'U') IS NOT NULL
DROP TABLE dbo.Places;

IF OBJECT_ID('dbo.Projects', 'U') IS NOT NULL
DROP TABLE dbo.Projects;

IF OBJECT_ID('dbo.Engineers', 'U') IS NOT NULL
DROP TABLE dbo.Engineers;


IF OBJECT_ID('dbo.RequirementType', 'U') IS NOT NULL
DROP TABLE dbo.RequirementType;

IF OBJECT_ID('dbo.JobTypes', 'U') IS NOT NULL
DROP TABLE dbo.JobTypes;


IF OBJECT_ID('dbo.Districts', 'U') IS NOT NULL
DROP TABLE dbo.Districts;

IF OBJECT_ID('dbo.Roles', 'U') IS NOT NULL
DROP TABLE dbo.Roles;

IF OBJECT_ID('dbo.UserPasswords', 'U') IS NOT NULL
DROP TABLE dbo.UserPasswords;
GO


-- For now leave the following tables untouched.
-- I let them stand as part if inital setup 
--   UserProfile
--   webpages_OAuthMembership
--   webpages_Membership
--   webpages_Roles
--   webpages_UsersInRoles




SELECT * FROM INFORMATION_SCHEMA.TABLES

GO
