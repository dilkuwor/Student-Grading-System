
/*
 * Project Name: GPA  
 * Date Started: 01/05/2014
 * Description: Fetched students vs course information
 * Module Name: 006(Reporting)
 * Developer Name:Laxaman Adhikari
 * Version: 0.1
 * Date Modified:
 * 
 */
USE [GPA]
GO

IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'StudentCourse')
DROP PROCEDURE StudentCourse
 GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[StudentCourse]
 @UserID int
 AS
BEGIN
SELECT C.CourseName,U.RegistrationID, U.Fname, U.Lname, U.Email FROM UserDetails U
JOIN CourseEnrolments CE ON U.RegistrationID = CE.UserRef_ID
JOIN Courses C ON CE.CourseRef_ID = C.Id
WHERE CE.IsApproved = 'True' and RegistrationID= @UserID group by C.CourseName,U.RegistrationID, U.Fname, U.Lname, U.Email 
END
GO