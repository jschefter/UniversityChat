USE [ucdatabase];

IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'UniversityChat' 
				AND TABLE_NAME = 'RoomUsers' AND TABLE_TYPE = 'BASE TABLE')
				
BEGIN

	CREATE TABLE [UniversityChat].[RoomUsers]
	(
		[RoomId] [uniqueidentifier] NOT NULL,
		[UserId] [uniqueidentifier] NOT NULL		 
	)
	
END;




