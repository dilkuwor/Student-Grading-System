USE [GPA]
GO

IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'FindGPA')
DROP PROCEDURE FindGPA
 GO

Create PROCEDURE [dbo].[FindGPA]

 @UserID int

 AS
BEGIN
SELECT R.FName +' '+R.LName AS Name, R.RegistrationID AS StudentID,c.Id AS CourseID, C.CourseName,c.Credit, G.GradeScore, G.GradeNumber, C.Credit*G.GradeNumber AS StudentCourseGrade

FROM Registrations R 
JOIN CourseUsers CU ON R.RegistrationID = CU.Users_Id 
JOIN Courses C ON CU.Courses_Id = C.Id 
JOIN StudentGrades SG ON C.Id = SG.CourseId 
JOIN Grades G ON SG.GradeId = G.Id

WHERE R.RegistrationID = @UserID;
END

GO