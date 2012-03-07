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
    LogID             NCHAR(10) NOT NULL UNIQUE,
	LogEvent		  VARCHAR(50) NULL,
	LogTime			  DATE NULL,
	Description		  VARCHAR(50), NULL
	Position          INT NULL 
CONSTRAINT pk_SystemComponentTable PRIMARY KEY CLUSTERED (LogID))
GO