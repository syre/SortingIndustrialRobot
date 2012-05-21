

USE F12I4PRJ4Gr3;
GO

--
-- Create Table    : 'Permissions'   
-- ID              :  
-- Name            :  
--
CREATE TABLE Permissions (
    ID             INT IDENTITY(1,1) NOT NULL UNIQUE,
    Name           VARCHAR(20) NOT NULL,
CONSTRAINT pk_Permissions PRIMARY KEY CLUSTERED (ID))
GO

--
-- Create Table    : 'Producers'   
-- ID              :  
-- Name            :  
-- Description     :  
--
CREATE TABLE Producers (
    ID             INT IDENTITY(1,1) NOT NULL UNIQUE,
    Name           VARCHAR(20) NOT NULL,
    Description    VARCHAR(255) NOT NULL,
CONSTRAINT pk_Producers PRIMARY KEY CLUSTERED (ID))
GO

--
-- Create Table    : 'Users'   
-- ID              :  
-- Name            :  
-- Description     :  
-- Permission      :  (references Permissions.ID)
--
CREATE TABLE Users (
    ID             INT IDENTITY(1,1) NOT NULL UNIQUE,
    Name           VARCHAR(20) NOT NULL,
    Description    VARCHAR(255) NOT NULL,
    Permission     INT NOT NULL,
CONSTRAINT pk_Users PRIMARY KEY CLUSTERED (ID),
CONSTRAINT fk_Users FOREIGN KEY (Permission)
    REFERENCES Permissions (ID)
    ON UPDATE CASCADE)
GO

--
-- Create Table    : 'Components'   
-- ID              :  
-- Name            :  
-- Description     :  
-- Producer        :  (references Producers.ID)
--
CREATE TABLE Components (
    ID             INT IDENTITY(1,1) NOT NULL UNIQUE,
    Name           VARCHAR(20) NOT NULL,
    Description    VARCHAR(255) NOT NULL,
    Producer       INT NULL,
CONSTRAINT pk_Components PRIMARY KEY CLUSTERED (ID),
CONSTRAINT fk_Components FOREIGN KEY (Producer)
    REFERENCES Producers (ID)
    ON UPDATE CASCADE)
GO

--
-- Create Table    : 'Software'   
-- ID              :  
-- Name            :  
-- Description     :  
-- Producer        :  (references Producers.ID)
--
CREATE TABLE Software (
    ID             INT IDENTITY(1,1) NOT NULL UNIQUE,
    Name           VARCHAR(20) NOT NULL,
    Description    VARCHAR(255) NOT NULL,
    Producer       INT NULL,
CONSTRAINT pk_Software PRIMARY KEY CLUSTERED (ID),
CONSTRAINT fk_Software FOREIGN KEY (Producer)
    REFERENCES Producers (ID)
    ON UPDATE CASCADE)
GO

--
-- Create Table    : 'Position'   
-- PositionID      :   
-- X			   :  
-- Y			   :
-- Z			   :
--
CREATE TABLE Position (
	PositionID			INT PRIMARY KEY NOT NULL,
	X INT,
	Y INT,
	Z INT)

GO

--
-- Create Table    : 'BoxInfo'   
-- BoxID           :  
-- Length          :  
-- Width	       :  
-- Depth		   :
-- Weight		   :
--
CREATE TABLE BoxInfo (
	BoxID			INT PRIMARY KEY NOT NULL IDENTITY,
	PositionID		INT NOT NULL REFERENCES Position(PositionID),
	Length FLOAT,
	Width FLOAT,
	Depth FLOAT,
	Weight FLOAT)
	
	
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
	PositionID				  INT NOT NULL,
	CONSTRAINT fk_SystemComponentTable FOREIGN KEY (PositionID)
    REFERENCES Position (PositionID)
	)
GO
