USE [CSS490]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [UniversityChat].[Users]
(
	[UserId] [uniqueidentifier] NOT NULL,
	[FName] [varchar](50) NULL,
	[LName] [varchar](50) NULL,
	[NickName] [varchar](50) NOT NULL,
	[Email] [varchar](50) NOT NULL,
	[UserRoleId] [int] NOT NULL,
	
	CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
	(
		[UserId] ASC
	)
	WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, 
	IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [UniversityChat].[Users]
	ADD CONSTRAINT UNQ_Users_NickName
	UNIQUE([NickName]);
	
ALTER TABLE [UniversityChat].[Users]
ADD CONSTRAINT UNQ_Users_Email
	UNIQUE([Email]);

SET ANSI_PADDING OFF
GO


