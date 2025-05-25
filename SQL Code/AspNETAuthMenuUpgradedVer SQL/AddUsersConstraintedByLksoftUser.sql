use db_ab6e43_lkacsoftdb
go

insert into AspNetUsers (Id, UserName, Email, PasswordHash, EmailConfirmed, PhoneNumberConfirmed, TwoFactorEnabled, LockoutEnabled, AccessFailedCount)
values
('U006', 'giaonhan1@gmail.com', 'giaonhan1n@gmail.com', 'AQAAAAIAAYagAAAAEHM0U3ehOINoPHPbYsw6BL5ofxpbnZ8m+WeTRoWo0HbN7SlnrFLGjfcv8iD1Sa/ApA==', 0, 0, 0, 1, 0),
('U007', 'giaonhan2@gmail.com', 'giaonhan2@gmail.com', 'AQAAAAIAAYagAAAAEHM0U3ehOINoPHPbYsw6BL5ofxpbnZ8m+WeTRoWo0HbN7SlnrFLGjfcv8iD1Sa/ApA==', 0, 0, 0, 1, 0),
('U005', 'nvketoan@gmail.com', 'nvketoan@gmail.com', 'AQAAAAIAAYagAAAAEHM0U3ehOINoPHPbYsw6BL5ofxpbnZ8m+WeTRoWo0HbN7SlnrFLGjfcv8iD1Sa/ApA==', 0, 0, 0, 1, 0),
('U009', 'tnketoan@gmail.com', 'tnketoan@gmail.com', 'AQAAAAIAAYagAAAAEHM0U3ehOINoPHPbYsw6BL5ofxpbnZ8m+WeTRoWo0HbN7SlnrFLGjfcv8iD1Sa/ApA==', 0, 0, 0, 1, 0),
('U003', 'nvhanhchinh1@gmail.com', 'nvhanhchinh@gmail.com', 'AQAAAAIAAYagAAAAEHM0U3ehOINoPHPbYsw6BL5ofxpbnZ8m+WeTRoWo0HbN7SlnrFLGjfcv8iD1Sa/ApA==', 0, 0, 0, 1, 0),
('U008', 'tpketoan2@gmail.com', 'tpketoan2@gmail.com', 'AQAAAAIAAYagAAAAEHM0U3ehOINoPHPbYsw6BL5ofxpbnZ8m+WeTRoWo0HbN7SlnrFLGjfcv8iD1Sa/ApA==', 0, 0, 0, 1, 0),
('U010', 'nvhanhchinh2@gmail.com', 'nvhanhchinh2@gmail.com', 'AQAAAAIAAYagAAAAEHM0U3ehOINoPHPbYsw6BL5ofxpbnZ8m+WeTRoWo0HbN7SlnrFLGjfcv8iD1Sa/ApA==', 0, 0, 0, 1, 0),
('U004', 'tpketoan1@gmail.com', 'tpketoan@gmail.com', 'AQAAAAIAAYagAAAAEHM0U3ehOINoPHPbYsw6BL5ofxpbnZ8m+WeTRoWo0HbN7SlnrFLGjfcv8iD1Sa/ApA==', 0, 0, 0, 1, 0);


insert into AspNetUserRoles (UserId, RoleId)
values 
('U004', 'EE467945-8651-4327-A3CD-EB3AAE5C6BF2'),
('U006', 'D534BC5A-4D73-4D20-AC81-320D7329AFA0'),
('U007', 'D534BC5A-4D73-4D20-AC81-320D7329AFA0'),
('U005', 'CD2247FE-1084-47BC-939B-584E39E92C2C'),
('U009', 'EBDB3AFF-BB8F-4F87-8305-89CEF57C227A'),
('U003', 'EE467945-8651-4327-A3CD-EB3AAE5C6BF2'),
('U008', 'DA4EB058-1DA4-42E9-B4CE-B5B9813DA5B6'),
('U010', 'EE467945-8651-4327-A3CD-EB3AAE5C6BF2'),
('U004', 'DA4EB058-1DA4-42E9-B4CE-B5B9813DA5B6');

go