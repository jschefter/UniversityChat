USE [ucdatabase];

IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'UniversityChat' 
				AND TABLE_NAME = 'Rooms' AND TABLE_TYPE = 'BASE TABLE')

BEGIN

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
		[ModeratorId] [uniqueidentifier] NULL			
	)

	ALTER TABLE [UniversityChat].[Rooms]
		ADD CONSTRAINT UNQ_Rooms_RoomName
		UNIQUE([RoomName]);

END;


