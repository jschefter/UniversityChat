USE [CSS490]
GO

CREATE TABLE [UniversityChat].[History]
(
	[Id] [uniqueidentifier] NOT NULL,
	[Text] [nvarchar](500) NULL,
	[SenderId] [uniqueidentifier] NOT NULL,
	[DateTimeStamp] [datetime] NOT NULL,
	[SessionId] [uniqueidentifier] NULL,
	
	CONSTRAINT [PK_History] PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	)
	WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, 
	IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
