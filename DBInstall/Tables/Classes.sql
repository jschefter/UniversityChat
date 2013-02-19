USE [CSS490];

IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'UniversityChat' 
				AND TABLE_NAME = 'Classes' AND TABLE_TYPE = 'BASE TABLE')

BEGIN

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
	
	ALTER TABLE [UniversityChat].[Classes]
		ADD CONSTRAINT UNQ_Classes
		UNIQUE([DepartmentName], [University], [Quarter], 
			[Session], [InstructorName], [ClassName]);
			
END;

