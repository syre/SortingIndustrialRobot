--
-- Select Database: 'F12I4PRJ4Gr3'
--
USE F12I4PRJ4Gr3
GO

--
-- Create Table    : 'SystemComponentTable'   
-- ID              :  
-- Name            :  
--
CREATE TABLE SystemComponentTable (
    LogID             INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
	LogEvent		  VARCHAR(50) NOT NULL,
	LogTime			  DATETIME NOT NULL,
	Description		  VARCHAR(50) NOT NULL,
	PositionID        INT NOT NULL)
GO
