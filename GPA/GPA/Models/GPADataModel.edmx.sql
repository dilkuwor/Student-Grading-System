
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 01/05/2014 22:15:46
-- Generated from EDMX file: C:\Users\sreng-kh\Documents\GitHub\Student-Grading-System\GPA\GPA\Models\GPADataModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [GPA];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_CourseUser_Courses]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CourseUsers] DROP CONSTRAINT [FK_CourseUser_Courses];
GO
IF OBJECT_ID(N'[dbo].[FK_Student_GPACourse]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[StudentGrades] DROP CONSTRAINT [FK_Student_GPACourse];
GO
IF OBJECT_ID(N'[dbo].[FK_CourseUser_Registration]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CourseUsers] DROP CONSTRAINT [FK_CourseUser_Registration];
GO
IF OBJECT_ID(N'[dbo].[FK_Feedbacks_RegistrationDetails]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Feedbacks] DROP CONSTRAINT [FK_Feedbacks_RegistrationDetails];
GO
IF OBJECT_ID(N'[dbo].[FK_Feedbacks_Registrations]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Feedbacks] DROP CONSTRAINT [FK_Feedbacks_Registrations];
GO
IF OBJECT_ID(N'[dbo].[FK_GradeStudent_GPA]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[StudentGrades] DROP CONSTRAINT [FK_GradeStudent_GPA];
GO
IF OBJECT_ID(N'[dbo].[FK_Registrations_Users]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Registrations] DROP CONSTRAINT [FK_Registrations_Users];
GO
IF OBJECT_ID(N'[dbo].[FK_RoleTask_Roles]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[RoleTasks] DROP CONSTRAINT [FK_RoleTask_Roles];
GO
IF OBJECT_ID(N'[dbo].[FK_UserRole_Roles]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserRoles] DROP CONSTRAINT [FK_UserRole_Roles];
GO
IF OBJECT_ID(N'[dbo].[FK_RoleTask_Tasks]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[RoleTasks] DROP CONSTRAINT [FK_RoleTask_Tasks];
GO
IF OBJECT_ID(N'[dbo].[FK_UserRole_Users]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserRoles] DROP CONSTRAINT [FK_UserRole_Users];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Courses]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Courses];
GO
IF OBJECT_ID(N'[dbo].[CourseUsers]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CourseUsers];
GO
IF OBJECT_ID(N'[dbo].[Feedbacks]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Feedbacks];
GO
IF OBJECT_ID(N'[dbo].[Grades]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Grades];
GO
IF OBJECT_ID(N'[dbo].[Registrations]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Registrations];
GO
IF OBJECT_ID(N'[dbo].[Roles]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Roles];
GO
IF OBJECT_ID(N'[dbo].[RoleTasks]', 'U') IS NOT NULL
    DROP TABLE [dbo].[RoleTasks];
GO
IF OBJECT_ID(N'[dbo].[StudentGrades]', 'U') IS NOT NULL
    DROP TABLE [dbo].[StudentGrades];
GO
IF OBJECT_ID(N'[dbo].[Tasks]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Tasks];
GO
IF OBJECT_ID(N'[dbo].[UserRoles]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserRoles];
GO
IF OBJECT_ID(N'[dbo].[Users]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Users];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Courses'
CREATE TABLE [dbo].[Courses] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [SubCode] nvarchar(max)  NOT NULL,
    [Level] nvarchar(max)  NOT NULL,
    [CourseName] nvarchar(max)  NOT NULL,
    [Credit] int  NOT NULL
);
GO

-- Creating table 'CourseUsers'
CREATE TABLE [dbo].[CourseUsers] (
    [Courses_Id] int  NOT NULL,
    [Users_Id] int  NOT NULL,
    [Id] int IDENTITY(1,1) NOT NULL
);
GO

-- Creating table 'Feedbacks'
CREATE TABLE [dbo].[Feedbacks] (
    [FeedbackID] int IDENTITY(1,1) NOT NULL,
    [Comment] nvarchar(max)  NOT NULL,
    [FromID] int  NOT NULL,
    [ToID] int  NOT NULL,
    [Subject] varchar(50)  NULL,
    [Date] varchar(10)  NULL
);
GO

-- Creating table 'Grades'
CREATE TABLE [dbo].[Grades] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [GradeScore] nvarchar(max)  NOT NULL,
    [GradeNumber] float  NOT NULL
);
GO

-- Creating table 'Registrations'
CREATE TABLE [dbo].[Registrations] (
    [RegistrationID] int IDENTITY(1,1) NOT NULL,
    [FName] nvarchar(max)  NOT NULL,
    [LName] varchar(50)  NOT NULL,
    [Email] nvarchar(max)  NOT NULL,
    [Address] nvarchar(max)  NULL,
    [City] nvarchar(max)  NULL,
    [State] nvarchar(max)  NULL,
    [Zip] nvarchar(max)  NULL,
    [LandNumber] varchar(50)  NULL,
    [MobileNumber] varchar(50)  NULL,
    [UserID] int  NOT NULL
);
GO

-- Creating table 'Roles'
CREATE TABLE [dbo].[Roles] (
    [Role_ID] int IDENTITY(1,1) NOT NULL,
    [RoleName] varchar(50)  NOT NULL
);
GO

-- Creating table 'RoleTasks'
CREATE TABLE [dbo].[RoleTasks] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [RoleRef_ID] int  NOT NULL,
    [TaskRef_ID] int  NOT NULL
);
GO

-- Creating table 'StudentGrades'
CREATE TABLE [dbo].[StudentGrades] (
    [UserId] int  NOT NULL,
    [CourseId] int  NOT NULL,
    [GradeId] int  NOT NULL,
    [Status] bit  NOT NULL
);
GO

-- Creating table 'Tasks'
CREATE TABLE [dbo].[Tasks] (
    [TaskId] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Description] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'UserRoles'
CREATE TABLE [dbo].[UserRoles] (
    [Id] int  NOT NULL,
    [UserRefID] int  NOT NULL,
    [RoleRef_ID] int  NOT NULL
);
GO

-- Creating table 'Users'
CREATE TABLE [dbo].[Users] (
    [UserID] int IDENTITY(1,1) NOT NULL,
    [UserName] nvarchar(max)  NOT NULL,
    [Password] nvarchar(max)  NOT NULL,
    [VerificationCode] nvarchar(max)  NOT NULL,
    [Role] varchar(50)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Courses'
ALTER TABLE [dbo].[Courses]
ADD CONSTRAINT [PK_Courses]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'CourseUsers'
ALTER TABLE [dbo].[CourseUsers]
ADD CONSTRAINT [PK_CourseUsers]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [FeedbackID] in table 'Feedbacks'
ALTER TABLE [dbo].[Feedbacks]
ADD CONSTRAINT [PK_Feedbacks]
    PRIMARY KEY CLUSTERED ([FeedbackID] ASC);
GO

-- Creating primary key on [Id] in table 'Grades'
ALTER TABLE [dbo].[Grades]
ADD CONSTRAINT [PK_Grades]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [RegistrationID] in table 'Registrations'
ALTER TABLE [dbo].[Registrations]
ADD CONSTRAINT [PK_Registrations]
    PRIMARY KEY CLUSTERED ([RegistrationID] ASC);
GO

-- Creating primary key on [Role_ID] in table 'Roles'
ALTER TABLE [dbo].[Roles]
ADD CONSTRAINT [PK_Roles]
    PRIMARY KEY CLUSTERED ([Role_ID] ASC);
GO

-- Creating primary key on [Id] in table 'RoleTasks'
ALTER TABLE [dbo].[RoleTasks]
ADD CONSTRAINT [PK_RoleTasks]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [UserId], [CourseId] in table 'StudentGrades'
ALTER TABLE [dbo].[StudentGrades]
ADD CONSTRAINT [PK_StudentGrades]
    PRIMARY KEY CLUSTERED ([UserId], [CourseId] ASC);
GO

-- Creating primary key on [TaskId] in table 'Tasks'
ALTER TABLE [dbo].[Tasks]
ADD CONSTRAINT [PK_Tasks]
    PRIMARY KEY CLUSTERED ([TaskId] ASC);
GO

-- Creating primary key on [Id] in table 'UserRoles'
ALTER TABLE [dbo].[UserRoles]
ADD CONSTRAINT [PK_UserRoles]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [UserID] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [PK_Users]
    PRIMARY KEY CLUSTERED ([UserID] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [Courses_Id] in table 'CourseUsers'
ALTER TABLE [dbo].[CourseUsers]
ADD CONSTRAINT [FK_CourseUser_Courses]
    FOREIGN KEY ([Courses_Id])
    REFERENCES [dbo].[Courses]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_CourseUser_Courses'
CREATE INDEX [IX_FK_CourseUser_Courses]
ON [dbo].[CourseUsers]
    ([Courses_Id]);
GO

-- Creating foreign key on [CourseId] in table 'StudentGrades'
ALTER TABLE [dbo].[StudentGrades]
ADD CONSTRAINT [FK_Student_GPACourse]
    FOREIGN KEY ([CourseId])
    REFERENCES [dbo].[Courses]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Student_GPACourse'
CREATE INDEX [IX_FK_Student_GPACourse]
ON [dbo].[StudentGrades]
    ([CourseId]);
GO

-- Creating foreign key on [Users_Id] in table 'CourseUsers'
ALTER TABLE [dbo].[CourseUsers]
ADD CONSTRAINT [FK_CourseUser_Registration]
    FOREIGN KEY ([Users_Id])
    REFERENCES [dbo].[Registrations]
        ([RegistrationID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_CourseUser_Registration'
CREATE INDEX [IX_FK_CourseUser_Registration]
ON [dbo].[CourseUsers]
    ([Users_Id]);
GO

-- Creating foreign key on [FromID] in table 'Feedbacks'
ALTER TABLE [dbo].[Feedbacks]
ADD CONSTRAINT [FK_Feedbacks_RegistrationDetails]
    FOREIGN KEY ([FromID])
    REFERENCES [dbo].[Registrations]
        ([RegistrationID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Feedbacks_RegistrationDetails'
CREATE INDEX [IX_FK_Feedbacks_RegistrationDetails]
ON [dbo].[Feedbacks]
    ([FromID]);
GO

-- Creating foreign key on [ToID] in table 'Feedbacks'
ALTER TABLE [dbo].[Feedbacks]
ADD CONSTRAINT [FK_Feedbacks_Registrations]
    FOREIGN KEY ([ToID])
    REFERENCES [dbo].[Registrations]
        ([RegistrationID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Feedbacks_Registrations'
CREATE INDEX [IX_FK_Feedbacks_Registrations]
ON [dbo].[Feedbacks]
    ([ToID]);
GO

-- Creating foreign key on [GradeId] in table 'StudentGrades'
ALTER TABLE [dbo].[StudentGrades]
ADD CONSTRAINT [FK_GradeStudent_GPA]
    FOREIGN KEY ([GradeId])
    REFERENCES [dbo].[Grades]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_GradeStudent_GPA'
CREATE INDEX [IX_FK_GradeStudent_GPA]
ON [dbo].[StudentGrades]
    ([GradeId]);
GO

-- Creating foreign key on [UserID] in table 'Registrations'
ALTER TABLE [dbo].[Registrations]
ADD CONSTRAINT [FK_Registrations_Users]
    FOREIGN KEY ([UserID])
    REFERENCES [dbo].[Users]
        ([UserID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Registrations_Users'
CREATE INDEX [IX_FK_Registrations_Users]
ON [dbo].[Registrations]
    ([UserID]);
GO

-- Creating foreign key on [RoleRef_ID] in table 'RoleTasks'
ALTER TABLE [dbo].[RoleTasks]
ADD CONSTRAINT [FK_RoleTask_Roles]
    FOREIGN KEY ([RoleRef_ID])
    REFERENCES [dbo].[Roles]
        ([Role_ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_RoleTask_Roles'
CREATE INDEX [IX_FK_RoleTask_Roles]
ON [dbo].[RoleTasks]
    ([RoleRef_ID]);
GO

-- Creating foreign key on [RoleRef_ID] in table 'UserRoles'
ALTER TABLE [dbo].[UserRoles]
ADD CONSTRAINT [FK_UserRole_Roles]
    FOREIGN KEY ([RoleRef_ID])
    REFERENCES [dbo].[Roles]
        ([Role_ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_UserRole_Roles'
CREATE INDEX [IX_FK_UserRole_Roles]
ON [dbo].[UserRoles]
    ([RoleRef_ID]);
GO

-- Creating foreign key on [TaskRef_ID] in table 'RoleTasks'
ALTER TABLE [dbo].[RoleTasks]
ADD CONSTRAINT [FK_RoleTask_Tasks]
    FOREIGN KEY ([TaskRef_ID])
    REFERENCES [dbo].[Tasks]
        ([TaskId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_RoleTask_Tasks'
CREATE INDEX [IX_FK_RoleTask_Tasks]
ON [dbo].[RoleTasks]
    ([TaskRef_ID]);
GO

-- Creating foreign key on [UserRefID] in table 'UserRoles'
ALTER TABLE [dbo].[UserRoles]
ADD CONSTRAINT [FK_UserRole_Users]
    FOREIGN KEY ([UserRefID])
    REFERENCES [dbo].[Users]
        ([UserID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_UserRole_Users'
CREATE INDEX [IX_FK_UserRole_Users]
ON [dbo].[UserRoles]
    ([UserRefID]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------