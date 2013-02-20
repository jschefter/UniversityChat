USE [ucdatabase];

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
	
ALTER TABLE [UniversityChat].[History]  
	ADD CONSTRAINT FK_History_Sessions
	FOREIGN KEY([SessionId])
	REFERENCES [UniversityChat].[Sessions](SessionId)

ALTER TABLE [UniversityChat].[History]  
	ADD CONSTRAINT FK_History_Users
	FOREIGN KEY([SenderId])
	REFERENCES [UniversityChat].[Users](UserId)
