USE [CSS490];

ALTER TABLE [UniversityChat].[Users] 
	DROP CONSTRAINT FK_Users_Roles;
	
	
ALTER TABLE [UniversityChat].[Rooms]
	DROP CONSTRAINT FK_Rooms_Classes;
	
	
ALTER TABLE [UniversityChat].[RoomUsers]  
	DROP CONSTRAINT FK_UserRooms_Users;
		
ALTER TABLE [UniversityChat].[RoomUsers]  
	DROP CONSTRAINT FK_UserRooms_Rooms;
		
ALTER TABLE [UniversityChat].[History]  
	DROP CONSTRAINT FK_History_Sessions;
	
ALTER TABLE [UniversityChat].[History]  
	DROP CONSTRAINT FK_History_Users;
	
