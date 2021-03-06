USE [master]
GO
/****** Object:  Database [GPA]    Script Date: 1/7/2014 3:29:29 PM ******/
CREATE DATABASE [GPA]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'GPA', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\GPA.mdf' , SIZE = 3072KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'GPA_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\GPA_log.ldf' , SIZE = 2048KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [GPA] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [GPA].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [GPA] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [GPA] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [GPA] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [GPA] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [GPA] SET ARITHABORT OFF 
GO
ALTER DATABASE [GPA] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [GPA] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [GPA] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [GPA] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [GPA] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [GPA] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [GPA] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [GPA] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [GPA] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [GPA] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [GPA] SET  DISABLE_BROKER 
GO
ALTER DATABASE [GPA] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [GPA] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [GPA] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [GPA] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [GPA] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [GPA] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [GPA] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [GPA] SET RECOVERY FULL 
GO
ALTER DATABASE [GPA] SET  MULTI_USER 
GO
ALTER DATABASE [GPA] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [GPA] SET DB_CHAINING OFF 
GO
ALTER DATABASE [GPA] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [GPA] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
EXEC sys.sp_db_vardecimal_storage_format N'GPA', N'ON'
GO
USE [GPA]
GO
/****** Object:  StoredProcedure [dbo].[FindGPA]    Script Date: 1/7/2014 3:29:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[FindGPA]

 @UserID int

 AS
BEGIN
SELECT U.FName +' '+U.LName AS Name, U.RegistrationID AS StudentID,c.Id AS CourseID,
C.CourseName,c.Credit, G.GradeScore, G.GradeNumber, C.Credit*G.GradeNumber AS StudentCourseGrade,
SG.ExtraCredit EC

FROM UserDetails U
JOIN CourseUsers CU ON U.RegistrationID = CU.Users_Id 
JOIN Courses C ON CU.Courses_Id = C.Id 
JOIN StudentGrades SG ON C.Id = SG.CourseId 
JOIN Grades G ON SG.GradeId = G.Id

WHERE U.RegistrationID = @UserID;
END
GO
/****** Object:  StoredProcedure [dbo].[GetEcourses]    Script Date: 1/7/2014 3:29:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetEcourses]

 @UserID int

 AS
BEGIN

SELECT C.Id, C.CourseName FROM Courses C

WHERE C.Id NOT IN (
SELECT CU.Courses_Id FROM CourseUsers CU
WHERE CU.Users_Id = @UserID)

AND C.Id Not in(

SELECT C.Id FROM Courses C
JOIN CourseEnrolments CE ON C.Id = CE.CourseRef_ID
WHERE CE.UserRef_ID = @UserID AND CE.IsApproved = 'True')

END
GO
/****** Object:  Table [dbo].[CourseEnrolments]    Script Date: 1/7/2014 3:29:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CourseEnrolments](
	[RequestID] [int] IDENTITY(1,1) NOT NULL,
	[UserRef_ID] [int] NOT NULL,
	[CourseRef_ID] [int] NOT NULL,
	[Date] [varchar](10) NOT NULL,
	[IsApproved] [bit] NOT NULL,
 CONSTRAINT [PK_CourseRequest] PRIMARY KEY CLUSTERED 
(
	[RequestID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Courses]    Script Date: 1/7/2014 3:29:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Courses](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SubCode] [nvarchar](max) NOT NULL,
	[Level] [nvarchar](max) NOT NULL,
	[CourseName] [nvarchar](max) NOT NULL,
	[Credit] [int] NOT NULL,
 CONSTRAINT [PK_Courses] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[CourseUsers]    Script Date: 1/7/2014 3:29:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CourseUsers](
	[Courses_Id] [int] NOT NULL,
	[Users_Id] [int] NOT NULL,
	[Id] [int] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_CourseUser] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Feedbacks]    Script Date: 1/7/2014 3:29:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Feedbacks](
	[FeedbackID] [int] IDENTITY(1,1) NOT NULL,
	[Comment] [nvarchar](max) NOT NULL,
	[FromID] [int] NOT NULL,
	[ToID] [int] NOT NULL,
	[Subject] [varchar](50) NULL,
	[Date] [varchar](10) NULL,
 CONSTRAINT [PK_Feedbacks] PRIMARY KEY CLUSTERED 
(
	[FeedbackID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Grades]    Script Date: 1/7/2014 3:29:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Grades](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[GradeScore] [nvarchar](10) NOT NULL,
	[GradeNumber] [float] NOT NULL,
 CONSTRAINT [PK_Grades] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Roles]    Script Date: 1/7/2014 3:29:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Roles](
	[Role_ID] [int] IDENTITY(1,1) NOT NULL,
	[RoleName] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED 
(
	[Role_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[RoleTasks]    Script Date: 1/7/2014 3:29:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RoleTasks](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RoleRef_ID] [int] NOT NULL,
	[TaskRef_ID] [int] NOT NULL,
 CONSTRAINT [PK_RoleTask] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[StudentGrades]    Script Date: 1/7/2014 3:29:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StudentGrades](
	[UserId] [int] NOT NULL,
	[CourseId] [int] NOT NULL,
	[GradeId] [int] NOT NULL,
	[ExtraCredit] [int] NULL,
 CONSTRAINT [PK_Student_Grade] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[CourseId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Tasks]    Script Date: 1/7/2014 3:29:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tasks](
	[TaskId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Tasks] PRIMARY KEY CLUSTERED 
(
	[TaskId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[UserDetails]    Script Date: 1/7/2014 3:29:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[UserDetails](
	[RegistrationID] [int] IDENTITY(1,1) NOT NULL,
	[FName] [nvarchar](max) NOT NULL,
	[LName] [varchar](50) NOT NULL,
	[Email] [nvarchar](max) NOT NULL,
	[Address] [nvarchar](max) NULL,
	[City] [nvarchar](max) NULL,
	[State] [nvarchar](max) NULL,
	[Zip] [nvarchar](max) NULL,
	[LandNumber] [varchar](50) NULL,
	[MobileNumber] [varchar](50) NULL,
	[UserID] [int] NOT NULL,
 CONSTRAINT [PK_UserDetails_1] PRIMARY KEY CLUSTERED 
(
	[RegistrationID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[UserRoles]    Script Date: 1/7/2014 3:29:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserRoles](
	[Id] [int] NOT NULL,
	[UserRefID] [int] NOT NULL,
	[RoleRef_ID] [int] NOT NULL,
 CONSTRAINT [PK_UserRole] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Users]    Script Date: 1/7/2014 3:29:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Users](
	[UserID] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [nvarchar](max) NOT NULL,
	[Password] [nvarchar](max) NOT NULL,
	[VerificationCode] [nvarchar](max) NOT NULL,
	[Role] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[CourseEnrolments] ON 

INSERT [dbo].[CourseEnrolments] ([RequestID], [UserRef_ID], [CourseRef_ID], [Date], [IsApproved]) VALUES (5, 3, 2, N'23-23-2333', 1)
INSERT [dbo].[CourseEnrolments] ([RequestID], [UserRef_ID], [CourseRef_ID], [Date], [IsApproved]) VALUES (11, 3, 5, N'2014-01-07', 0)
INSERT [dbo].[CourseEnrolments] ([RequestID], [UserRef_ID], [CourseRef_ID], [Date], [IsApproved]) VALUES (12, 3, 3, N'2014-01-07', 0)
INSERT [dbo].[CourseEnrolments] ([RequestID], [UserRef_ID], [CourseRef_ID], [Date], [IsApproved]) VALUES (13, 3, 3, N'2014-01-07', 0)
SET IDENTITY_INSERT [dbo].[CourseEnrolments] OFF
SET IDENTITY_INSERT [dbo].[Courses] ON 

INSERT [dbo].[Courses] ([Id], [SubCode], [Level], [CourseName], [Credit]) VALUES (1, N'CS435', N'400', N'Algorithm', 3)
INSERT [dbo].[Courses] ([Id], [SubCode], [Level], [CourseName], [Credit]) VALUES (2, N'CS422', N'400', N'Database', 3)
INSERT [dbo].[Courses] ([Id], [SubCode], [Level], [CourseName], [Credit]) VALUES (3, N'CS430', N'200', N'Project Management', 2)
INSERT [dbo].[Courses] ([Id], [SubCode], [Level], [CourseName], [Credit]) VALUES (4, N'CS522', N'500', N'Big Data', 3)
INSERT [dbo].[Courses] ([Id], [SubCode], [Level], [CourseName], [Credit]) VALUES (5, N'CS525', N'500', N'Guthrie', 3)
SET IDENTITY_INSERT [dbo].[Courses] OFF
SET IDENTITY_INSERT [dbo].[CourseUsers] ON 

INSERT [dbo].[CourseUsers] ([Courses_Id], [Users_Id], [Id]) VALUES (1, 3, 9)
INSERT [dbo].[CourseUsers] ([Courses_Id], [Users_Id], [Id]) VALUES (2, 4, 10)
INSERT [dbo].[CourseUsers] ([Courses_Id], [Users_Id], [Id]) VALUES (4, 3, 11)
INSERT [dbo].[CourseUsers] ([Courses_Id], [Users_Id], [Id]) VALUES (4, 4, 13)
SET IDENTITY_INSERT [dbo].[CourseUsers] OFF
SET IDENTITY_INSERT [dbo].[Feedbacks] ON 

INSERT [dbo].[Feedbacks] ([FeedbackID], [Comment], [FromID], [ToID], [Subject], [Date]) VALUES (10, N'The INSERT statement conflicted with the FOREIGN KEY constraint "FK_dbo.Calibration_dbo.Equipment_EquipID". The conflict occurred in database "Calibration", table "dbo.Equipment", column ''EquipID''. The statement has been terminated. –  jawad hasan Feb 25 ''13 at 10:59', 4, 3, N'Hi', N'2014-01-05')
INSERT [dbo].[Feedbacks] ([FeedbackID], [Comment], [FromID], [ToID], [Subject], [Date]) VALUES (13, N'
To begin, you will create a new ASP.NET MVC project. To keep this walkthrough simple, you will not create the test project that is an option for ASP.NET MVC projects. For more information about how to create an MVC test project, see Walkthrough: Creating a Basic MVC Project with Unit Tests in Visual Studio.', 4, 3, N'Re: hello', N'2014-01-05')
INSERT [dbo].[Feedbacks] ([FeedbackID], [Comment], [FromID], [ToID], [Subject], [Date]) VALUES (14, N'
To begin, you will create a new ASP.NET MVC project. To keep this walkthrough simple, you will not create the test project that is an option for ASP.NET MVC projects. For more information about how to create an MVC test project, see Walkthrough: Creating a Basic MVC Project with Unit Tests in Visual Studio.', 3, 4, N'Re: hello im here', N'2014-01-05')
INSERT [dbo].[Feedbacks] ([FeedbackID], [Comment], [FromID], [ToID], [Subject], [Date]) VALUES (15, N'
To begin, you will create a nekhkj ahdjkfhkajhdfaw ASP.NET MVC project. To keep this walkthrough simple, you will not create the test project that is an option for ASP.NET MVC projects. For more information about how to create an MVC test project, see Walkthrough: Creating a Basic MVC Project with Unit Tests in Visual Studio.', 3, 4, N'Re: hello hihadoifah', N'2014-01-05')
INSERT [dbo].[Feedbacks] ([FeedbackID], [Comment], [FromID], [ToID], [Subject], [Date]) VALUES (16, N'
The INSERT statement conflicted with the FOREIGN KEY constraint "FK_dbo.Calibration_dbo.Equipment_EquipID". The conflict occurred in database "Calibration", table "dbo.Equipment", column ''EquipID''. The statement has been terminated. –  jawad hasan Feb 25 ''13 at 10:59', 3, 4, N'Re: Hi', N'2014-01-05')
INSERT [dbo].[Feedbacks] ([FeedbackID], [Comment], [FromID], [ToID], [Subject], [Date]) VALUES (17, N'hi', 3, 4, N'hi', N'2014-01-06')
INSERT [dbo].[Feedbacks] ([FeedbackID], [Comment], [FromID], [ToID], [Subject], [Date]) VALUES (18, N'uihuh', 3, 4, N'guihiu', N'2014-01-06')
INSERT [dbo].[Feedbacks] ([FeedbackID], [Comment], [FromID], [ToID], [Subject], [Date]) VALUES (19, N'afdalkfjlka', 3, 4, N'adfadsfa', N'2014-01-06')
SET IDENTITY_INSERT [dbo].[Feedbacks] OFF
SET IDENTITY_INSERT [dbo].[Grades] ON 

INSERT [dbo].[Grades] ([Id], [GradeScore], [GradeNumber]) VALUES (1, N'A', 4)
INSERT [dbo].[Grades] ([Id], [GradeScore], [GradeNumber]) VALUES (2, N'A-', 3.7)
INSERT [dbo].[Grades] ([Id], [GradeScore], [GradeNumber]) VALUES (3, N'B', 3.5)
INSERT [dbo].[Grades] ([Id], [GradeScore], [GradeNumber]) VALUES (4, N'B-', 3)
SET IDENTITY_INSERT [dbo].[Grades] OFF
INSERT [dbo].[StudentGrades] ([UserId], [CourseId], [GradeId], [ExtraCredit]) VALUES (1, 1, 1, NULL)
INSERT [dbo].[StudentGrades] ([UserId], [CourseId], [GradeId], [ExtraCredit]) VALUES (1, 2, 2, NULL)
INSERT [dbo].[StudentGrades] ([UserId], [CourseId], [GradeId], [ExtraCredit]) VALUES (1, 3, 1, NULL)
INSERT [dbo].[StudentGrades] ([UserId], [CourseId], [GradeId], [ExtraCredit]) VALUES (1, 4, 3, NULL)
SET IDENTITY_INSERT [dbo].[Tasks] ON 

INSERT [dbo].[Tasks] ([TaskId], [Name], [Description]) VALUES (1, N'View Grade', N'Can view grade')
INSERT [dbo].[Tasks] ([TaskId], [Name], [Description]) VALUES (3, N'Print Grade', N'Print Grade Card')
INSERT [dbo].[Tasks] ([TaskId], [Name], [Description]) VALUES (4, N'Send Feed Back', N'Send Feed Back')
INSERT [dbo].[Tasks] ([TaskId], [Name], [Description]) VALUES (5, N'Send Notification', N'Send Notification')
INSERT [dbo].[Tasks] ([TaskId], [Name], [Description]) VALUES (6, N'Enter Grade', N'Enter Grade')
SET IDENTITY_INSERT [dbo].[Tasks] OFF
SET IDENTITY_INSERT [dbo].[UserDetails] ON 

INSERT [dbo].[UserDetails] ([RegistrationID], [FName], [LName], [Email], [Address], [City], [State], [Zip], [LandNumber], [MobileNumber], [UserID]) VALUES (3, N'Dil', N'Kuwor', N'klafjsdlk', N'jflk', N'jlkjflk', NULL, N'jflkajf', N'lkjkljlk', N'lkfjalkj', 19)
INSERT [dbo].[UserDetails] ([RegistrationID], [FName], [LName], [Email], [Address], [City], [State], [Zip], [LandNumber], [MobileNumber], [UserID]) VALUES (4, N'Laxman', N'aihfdk', N'kjhfajkh', N'kjhfajkhf', N'kjhfjkdh', NULL, N'kjfhj', N'kjhjkh', N'hfjkh', 20)
SET IDENTITY_INSERT [dbo].[UserDetails] OFF
SET IDENTITY_INSERT [dbo].[Users] ON 

INSERT [dbo].[Users] ([UserID], [UserName], [Password], [VerificationCode], [Role]) VALUES (19, N'Dil', N'D25D09E0C46D9A626DE5A8CCF3A15531F80C0ABC', N'1234', N'Student')
INSERT [dbo].[Users] ([UserID], [UserName], [Password], [VerificationCode], [Role]) VALUES (20, N'Laxman', N'965B10EE583ED7C4E4B9173EC5EF0AA7041DE9DB', N'LbzD', N'Staff')
SET IDENTITY_INSERT [dbo].[Users] OFF
/****** Object:  Index [IX_FK_CourseUser_User]    Script Date: 1/7/2014 3:29:29 PM ******/
CREATE NONCLUSTERED INDEX [IX_FK_CourseUser_User] ON [dbo].[CourseUsers]
(
	[Users_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_FK_GradeStudent_GPA]    Script Date: 1/7/2014 3:29:29 PM ******/
CREATE NONCLUSTERED INDEX [IX_FK_GradeStudent_GPA] ON [dbo].[StudentGrades]
(
	[GradeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_FK_Student_GPACourse]    Script Date: 1/7/2014 3:29:29 PM ******/
CREATE NONCLUSTERED INDEX [IX_FK_Student_GPACourse] ON [dbo].[StudentGrades]
(
	[CourseId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[CourseEnrolments]  WITH CHECK ADD  CONSTRAINT [FK_CourseRequest_CourseRequest] FOREIGN KEY([CourseRef_ID])
REFERENCES [dbo].[Courses] ([Id])
GO
ALTER TABLE [dbo].[CourseEnrolments] CHECK CONSTRAINT [FK_CourseRequest_CourseRequest]
GO
ALTER TABLE [dbo].[CourseEnrolments]  WITH CHECK ADD  CONSTRAINT [FK_CourseRequest_Registrations] FOREIGN KEY([UserRef_ID])
REFERENCES [dbo].[UserDetails] ([RegistrationID])
GO
ALTER TABLE [dbo].[CourseEnrolments] CHECK CONSTRAINT [FK_CourseRequest_Registrations]
GO
ALTER TABLE [dbo].[CourseUsers]  WITH CHECK ADD  CONSTRAINT [FK_CourseUser_Courses] FOREIGN KEY([Courses_Id])
REFERENCES [dbo].[Courses] ([Id])
GO
ALTER TABLE [dbo].[CourseUsers] CHECK CONSTRAINT [FK_CourseUser_Courses]
GO
ALTER TABLE [dbo].[CourseUsers]  WITH CHECK ADD  CONSTRAINT [FK_CourseUser_Registration] FOREIGN KEY([Users_Id])
REFERENCES [dbo].[UserDetails] ([RegistrationID])
GO
ALTER TABLE [dbo].[CourseUsers] CHECK CONSTRAINT [FK_CourseUser_Registration]
GO
ALTER TABLE [dbo].[Feedbacks]  WITH CHECK ADD  CONSTRAINT [FK_Feedbacks_RegistrationDetails] FOREIGN KEY([FromID])
REFERENCES [dbo].[UserDetails] ([RegistrationID])
GO
ALTER TABLE [dbo].[Feedbacks] CHECK CONSTRAINT [FK_Feedbacks_RegistrationDetails]
GO
ALTER TABLE [dbo].[Feedbacks]  WITH CHECK ADD  CONSTRAINT [FK_Feedbacks_Registrations] FOREIGN KEY([ToID])
REFERENCES [dbo].[UserDetails] ([RegistrationID])
GO
ALTER TABLE [dbo].[Feedbacks] CHECK CONSTRAINT [FK_Feedbacks_Registrations]
GO
ALTER TABLE [dbo].[RoleTasks]  WITH CHECK ADD  CONSTRAINT [FK_RoleTask_Roles] FOREIGN KEY([RoleRef_ID])
REFERENCES [dbo].[Roles] ([Role_ID])
GO
ALTER TABLE [dbo].[RoleTasks] CHECK CONSTRAINT [FK_RoleTask_Roles]
GO
ALTER TABLE [dbo].[RoleTasks]  WITH CHECK ADD  CONSTRAINT [FK_RoleTask_Tasks] FOREIGN KEY([TaskRef_ID])
REFERENCES [dbo].[Tasks] ([TaskId])
GO
ALTER TABLE [dbo].[RoleTasks] CHECK CONSTRAINT [FK_RoleTask_Tasks]
GO
ALTER TABLE [dbo].[StudentGrades]  WITH CHECK ADD  CONSTRAINT [FK_GradeStudent_GPA] FOREIGN KEY([GradeId])
REFERENCES [dbo].[Grades] ([Id])
GO
ALTER TABLE [dbo].[StudentGrades] CHECK CONSTRAINT [FK_GradeStudent_GPA]
GO
ALTER TABLE [dbo].[StudentGrades]  WITH CHECK ADD  CONSTRAINT [FK_Student_GPACourse] FOREIGN KEY([CourseId])
REFERENCES [dbo].[Courses] ([Id])
GO
ALTER TABLE [dbo].[StudentGrades] CHECK CONSTRAINT [FK_Student_GPACourse]
GO
ALTER TABLE [dbo].[UserDetails]  WITH CHECK ADD  CONSTRAINT [FK_Registrations_Users] FOREIGN KEY([UserID])
REFERENCES [dbo].[Users] ([UserID])
GO
ALTER TABLE [dbo].[UserDetails] CHECK CONSTRAINT [FK_Registrations_Users]
GO
ALTER TABLE [dbo].[UserRoles]  WITH CHECK ADD  CONSTRAINT [FK_UserRole_Roles] FOREIGN KEY([RoleRef_ID])
REFERENCES [dbo].[Roles] ([Role_ID])
GO
ALTER TABLE [dbo].[UserRoles] CHECK CONSTRAINT [FK_UserRole_Roles]
GO
ALTER TABLE [dbo].[UserRoles]  WITH CHECK ADD  CONSTRAINT [FK_UserRole_Users] FOREIGN KEY([UserRefID])
REFERENCES [dbo].[Users] ([UserID])
GO
ALTER TABLE [dbo].[UserRoles] CHECK CONSTRAINT [FK_UserRole_Users]
GO
USE [master]
GO
ALTER DATABASE [GPA] SET  READ_WRITE 
GO
