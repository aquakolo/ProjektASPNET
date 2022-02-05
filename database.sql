USE ProjektASPNET;

DROP TABLE IF EXISTS [Orders];
DROP TABLE IF EXISTS [Cart];
DROP TABLE IF EXISTS [Users];
DROP TABLE IF EXISTS [Products];


CREATE TABLE [Users](
	[ID] int IDENTITY(1,1) PRIMARY KEY NOT NULL,
	[Username] nvarchar(50) NOT NULL,
	[Password] nvarchar(50) NOT NULL,
	[UserRole] nvarchar(30) NOT NULL
);

CREATE TABLE [Products](
	[ID] int IDENTITY(1,1) PRIMARY KEY NOT NULL,
	[Name] nvarchar(50) NOT NULL,
	[Description] nvarchar(150) NOT NULL,
	[Price] nvarchar(30) NOT NULL
);

CREATE TABLE [Cart](
	[ID] int IDENTITY(1,1) PRIMARY KEY NOT NULL,
	[UserID] int FOREIGN KEY REFERENCES Users (ID) NOT NULL,
	[ProductID] int FOREIGN KEY REFERENCES Products (ID) NOT NULL,
);

CREATE TABLE [Orders](
	[ID] int IDENTITY(1,1) PRIMARY KEY NOT NULL,
	[UserID] int FOREIGN KEY REFERENCES Users (ID) NOT NULL,
	[ProductsID] nvarchar(150) NOT NULL, 
	[Date] Date NOT NULL,
	[Status] nvarchar(30) NOT NULL
);