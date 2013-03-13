CREATE TABLE [ucdatabase].[UniversityChat].[File]
(
	[FileId] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY CLUSTERED,
	[UploadDate] [datetime] NOT NULL,
	[FileName] [varchar](255) NOT NULL,
	[MimeType] [varchar](255) NOT NULL,
	[BinaryData] [varbinary](max) NOT NULL
);