USE [GPA]
GO

IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'GetEcourses')
DROP PROCEDURE GetEcourses
 GO

/****** Object:  StoredProcedure [dbo].[GetEcourses]    Script Date: 1/7/2014 11:43:22 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[GetEcourses]

 @UserID int

 AS
BEGIN

SELECT C.Id, C.CourseName FROM Courses C
LEFT JOIN CourseUser CU ON C.Id = CU.Courses_Id
WHERE CU.Users_Id != @UserID  AND C.Id Not in(

SELECT C.Id FROM Courses C
JOIN CourseEnrolment CE ON C.Id = CE.CourseRef_ID
WHERE CE.UserRef_ID = @UserID AND CE.IsApproved = 'True')


END