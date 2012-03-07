-- SELECT PART
SELECT [ID]
      ,[Name]
  FROM [F12I4PRJ4Gr3].[dbo].[Permissions]
GO

-- INSERT PART
INSERT INTO [F12I4PRJ4Gr3].[dbo].[Permissions]
           ([ID]
           ,[Name])
     VALUES
           (3
           ,'a')
GO

-- UPDATE PART
INSERT INTO [F12I4PRJ4Gr3].[dbo].[Permissions]
           ([ID]
           ,[Name])
     VALUES
           (1
           ,'a')
GO

-- DELETE PART
DELETE FROM [F12I4PRJ4Gr3].[dbo].[Permissions]
      WHERE ID = 1
GO