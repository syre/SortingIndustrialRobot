-- SELECT PART
SELECT [ID]
      ,[Name]
  FROM [F12I4PRJ4Gr3].[dbo].[Permissions]
GO

-- INSERT PART
INSERT INTO [F12I4PRJ4Gr3].[dbo].[Permissions]
           ([Name])
     VALUES
           ('ddadsadasdadd1')
GO

-- UPDATE PART
UPDATE [F12I4PRJ4Gr3].[dbo].[Permissions]
   SET [Name] = Name
 WHERE ID < 3
GO

-- DELETE PART
DELETE FROM [F12I4PRJ4Gr3].[dbo].[Permissions]
      WHERE ID > 5
GO