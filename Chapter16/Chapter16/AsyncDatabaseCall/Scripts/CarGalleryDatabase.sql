USE [CarGallery]
GO

-- NOTE:
-- Create a Database named 'CarGallery' (or any other and change the use statement) before executing this script

CREATE TABLE dbo.[Cars] (
	Id INT IDENTITY(1000,1) NOT NULL,
	Model NVARCHAR(50) NULL,
	Make NVARCHAR(50) NULL,
	[Year] INT NOT NULL,
	Price REAL NOT NULL,
	CONSTRAINT [PK_Cars] PRIMARY KEY CLUSTERED (Id) ON [PRIMARY]
) ON [PRIMARY];
GO

CREATE PROCEDURE [dbo].[sp$GetCars]
AS
-- wait for 1 second
WAITFOR DELAY '00:00:01';
SELECT * FROM Cars;
GO

INSERT INTO dbo.Cars VALUES('Car1', 'Model1', 2006, 24950);
INSERT INTO dbo.Cars VALUES('Car2', 'Model1', 2003, 56829);
INSERT INTO dbo.Cars VALUES('Car3', 'Model2', 2006, 17382);
INSERT INTO dbo.Cars VALUES('Car4', 'Model3', 2002, 72733);