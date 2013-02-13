USE [CSS490]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [UniversityChat].[Rooms]
(
	[RoomId] [uniqueidentifier] NOT NULL,
	[RoomName] [varchar](50) NOT NULL,
	[RoomDesc] [varchar](250) NULL,
	[ClassId] [uniqueidentifier] NOT NULL,
	[IsPrivate] [int] NULL,
	[IsActive] [int] NULL,
	[ExpirationDate] [date] NOT NULL,
	[StartDate] [date] NOT NULL,
	[LastUsedDate] [date] NOT NULL,
	[ModeratorId] [uniqueidentifier] NULL,
	
	CONSTRAINT [PK_Rooms] PRIMARY KEY CLUSTERED 
	(
		[RoomId] ASC
	)
	WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, 
		ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]


GO

ALTER TABLE [UniversityChat].[Rooms]
	ADD CONSTRAINT UNQ_Rooms_RoomName
	UNIQUE([RoomName]);

SET ANSI_PADDING OFF
GO


