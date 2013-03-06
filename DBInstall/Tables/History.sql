USE [ucdatabase];

CREATE TABLE [UniversityChat].[History]
(
	[Id] [uniqueidentifier] NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
	[ConnectionId] [uniqueidentifier] NULL,
	[RoomId] [uniqueidentifier] NOT NULL,
	[Text] [nvarchar](500) NULL,	
	[LogDateTimeStamp] [datetime] NOT NULL	
);
