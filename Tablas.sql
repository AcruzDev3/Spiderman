use Spiderman;

CREATE TABLE [role] (
	role_id INT PRIMARY KEY IDENTITY(1,1),
	name NVARCHAR(100) NOT NULL UNIQUE  
);

CREATE TABLE danger_level (
	danger_level_id INT PRIMARY KEY IDENTITY(1,1),
	[name] NVARCHAR(20) NOT NULL UNIQUE
);

CREATE TABLE crime_type(
	crime_type_id INT PRIMARY KEY IDENTITY(1,1),
	[name] NVARCHAR(50) NOT NULL UNIQUE
);

CREATE TABLE crime_grade (
	crime_grade_id INT PRIMARY KEY IDENTITY(1,1),
	name NVARCHAR(50) NOT NULL UNIQUE
);

CREATE TABLE [address] (
	address_id INT PRIMARY KEY IDENTITY(1,1),
	number int NOT NULL,
	side NCHAR(4) NOT NULL,
	zip_code NCHAR(5) NOT NULL,
	street NVARCHAR(150) NOT NULL,
	[image] NVARCHAR(150)
);

CREATE TABLE criminal (
	criminal_id INT PRIMARY KEY IDENTITY(1,1),
	[name] NVARCHAR(50) NOT NULL,
	[description] TEXT,
	risk_id INT NOT NULL,
	[image] NVARCHAR(255),
	criminal_since DATETIME NOT NULL,
	FOREIGN KEY (risk_id) REFERENCES danger_level(danger_level_id)
);

CREATE TABLE crime(
	crime_id INT PRIMARY KEY IDENTITY(1,1),
	address_id INT NOT NULL,
	grade INT NOT NULL,
	[type] INT NOT NULL,
	[description] TEXT,
	date_start DATETIME NOT NULL,
	date_end DATETIME,
	status BIT NOT NULL,
	FOREIGN KEY (grade) REFERENCES crime_grade(crime_grade_id),
	FOREIGN KEY (type) REFERENCES crime_type(crime_type_id)
);

CREATE TABLE [user] (
	user_id int PRIMARY KEY IDENTITY(1,1),
	name NVARCHAR(50) NOT NULL UNIQUE,
	email NVARCHAR(255) NOT NULL UNIQUE,
	password NVARCHAR(255) NOT NULL,
	role_id INT NOT NULL,
	image NVARCHAR(300),
	FOREIGN KEY (role_id) REFERENCES [role](role_id)
);