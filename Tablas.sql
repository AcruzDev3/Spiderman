use Spiderman;

CREATE TABLE [role] (
	role_id INT PRIMARY KEY IDENTITY(1,1),
	name NVARCHAR(100) NOT NULL UNIQUE  
);

CREATE TABLE criminal_risk_level (
	criminal_risk_level_id INT PRIMARY KEY IDENTITY(1,1),
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
	FOREIGN KEY (risk_id) REFERENCES criminal_risk_level(criminal_risk_level_id)
);

CREATE TABLE crime(
	crime_id INT PRIMARY KEY IDENTITY(1,1),
	address_id INT NOT NULL,
	grade_id INT NOT NULL,
	type_id INT NOT NULL,
	[description] TEXT,
	date_start DATETIME NOT NULL,
	date_end DATETIME,
	status BIT NOT NULL,
	FOREIGN KEY (grade_id) REFERENCES crime_grade(crime_grade_id),
	FOREIGN KEY (type_id) REFERENCES crime_type(crime_type_id),
	FOREIGN KEY (address_id) REFERENCES [address](address_id)
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

CREATE TABLE crime_heros (
	crime_id INT NOT NULL, 
	hero_id INT NOT NULL,
	PRIMARY KEY (crime_id, hero_id),
	FOREIGN KEY (crime_id) REFERENCES crime(crime_id),
	FOREIGN KEY (hero_id) REFERENCES [user](user_id)
);

CREATE TABLE crime_criminals (
	crime_id INT NOT NULL,
	criminal_id INT NOT NULL,
	PRIMARY KEY (crime_id, criminal_id),
	FOREIGN KEY (crime_id) REFERENCES crime(crime_id),
	FOREIGN KEY (criminal_id) REFERENCES criminal(criminal_id)
);