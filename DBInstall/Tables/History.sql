USE [ucdatabase];

CREATE TABLE [UniversityChat].[History]
(
	[Id] [uniqueidentifier] NOT NULL,
	[Text] [nvarchar](500) NULL,
	[SenderId] [uniqueidentifier] NOT NULL,
	[DateTimeStamp] [datetime] NOT NULL,
	[SessionId] [uniqueidentifier] NULL
);
