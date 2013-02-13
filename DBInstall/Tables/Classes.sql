USE [CSS490]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [UniversityChat].[Classes]
(
	[ClassId] [uniqueidentifier] NOT NULL,
	[DepartmentName] [varchar](50) NULL,
	[University] [varchar](50) NULL,
	[Quarter] [int] NULL,
	[Year] [int] NULL,
	[Session] [varchar](25) NULL,
	[Time] [time](7) NULL,
	[Days] [varchar](50) NULL,
	[InstructorName] [varchar](50) NULL,
	[ClassName] [varchar](50) NULL,
	
	CONSTRAINT [PK_Classes] PRIMARY KEY CLUSTERED 
	(
		[ClassId] ASC
	)
	WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF,
	 ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [UniversityChat].[Classes]
	ADD CONSTRAINT UNQ_Classes
	UNIQUE([DepartmentName], [University], [Quarter], 
		[Session], [InstructorName], [ClassName]);
		
SET ANSI_PADDING OFF
GO


