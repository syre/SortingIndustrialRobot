-- eksempler på CRUD-scripts, mangler at tilføje READ, UPDATE og DELETE til Position table mm.


--
-- Create Table    : 'Position'   
-- PositionID      :   
-- X			   :  
-- Y			   :
-- Z			   :
--
CREATE TABLE Position (
	PositionID			INT PRIMARY KEY NOT NULL IDENTITY,
	X INT,
	Y INT,
	Z INT)


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


