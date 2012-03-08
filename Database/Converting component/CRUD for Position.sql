-- SELECT PART
SELECT [PositionID]
      ,[X]
      ,[Y]
      ,[Z]
  FROM [F12I4PRJ4Gr3].[dbo].[Position]
GO

-- INSERT PART
INSERT INTO [F12I4PRJ4Gr3].[dbo].[Position]
           ([X]
           ,[Y]
           ,[Z])
     VALUES
           (2
           ,3
           ,3)
GO

-- UPDATE PART
UPDATE [F12I4PRJ4Gr3].[dbo].[Position]
   SET [X] = X
      ,[Y] = Y
      ,[Z] = Z
 WHERE Y = 2
GO

-- DELETE PART
DELETE FROM [F12I4PRJ4Gr3].[dbo].[Position]
      WHERE Y = 3
GO




