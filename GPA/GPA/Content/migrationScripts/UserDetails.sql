/*
 * Project Name: GPA  
 * Date Started: 01/05/2014
 * Description: Fetch user details innformation
 * Module Name: 006(Reporting)
 * Developer Name:Laxaman Adhikari
 * Version: 0.1
 * Date Modified:
 * 
 */
USE [GPA]
GO
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'GetUserDetails')
DROP PROCEDURE GetUserDetails
USE [GPA]
GO

SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetUserDetails]

 @UserID int

 AS
BEGIN
if(@UserID != 0)
select * from dbo.UserDetails where RegistrationID= @UserID
else 
select * from dbo.UserDetails
END

