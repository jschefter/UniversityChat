USE [CSS490];

ALTER TABLE [UniversityChat].[Users]
	ADD CONSTRAINT FK_Users_Roles
	FOREIGN KEY(UserRoleId)
	REFERENCES [UniversityChat].[Roles](RoleId);
	
ALTER TABLE [UniversityChat].[Rooms]
	ADD CONSTRAINT FK_Rooms_Classes
	FOREIGN KEY([ClassId])
	REFERENCES [UniversityChat].[Classes](ClassId)
	
ALTER TABLE [UniversityChat].[RoomUsers]  
	ADD CONSTRAINT FK_UserRooms_Users
	FOREIGN KEY([UserId])
	REFERENCES [UniversityChat].[Users](UserId);
	
ALTER TABLE [UniversityChat].[RoomUsers]  
	ADD CONSTRAINT FK_UserRooms_Rooms
	FOREIGN KEY([RoomId])
	REFERENCES [UniversityChat].[Rooms](RoomId)
