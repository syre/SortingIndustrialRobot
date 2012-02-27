-- eksempler på CRUD-scripts, mangler at tilføje READ, UPDATE og DELETE til Position table mm.

-- CREATE
CREATE TABLE BoxDimensions
(BoxID int PRIMARY KEY NOT NULL IDENTITY,
Length float,
Width float,
Depth float)

CREATE TABLE Position
(BoxID int REFERENCES BoxDimensions(BoxID),
Angle int,
Distance float)

INSERT BoxDimensions ([Length],[Width],[Depth]) VALUES (100.0,200.0,300.0)

-- READ
SELECT * FROM BoxDimensions

-- UPDATE
UPDATE BoxDimensions
SET [Length]=200,[Width]=300,[Depth]=400
WHERE BoxID=1

-- DELETE
DELETE FROM BoxDimensions
WHERE BoxID=1
