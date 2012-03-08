-- SELECT PART
SELECT [Name]
      ,[Description]
      ,[Producer]
  FROM [F12I4PRJ4Gr3].[dbo].[Software]
GO

-- INSERT PART
INSERT INTO [F12I4PRJ4Gr3].[dbo].[Software]
           ([Name]
           ,[Description]
           ,[Producer])
     VALUES
           ('F'
           ,'F'
           ,2)
GO

-- UPDATE PART
UPDATE [F12I4PRJ4Gr3].[dbo].[Software]
   SET [Name] = Name
      ,[Description] = Description
      ,[Producer] = Producer
 WHERE ID = 1
GO

-- DELETE PART
DELETE FROM [F12I4PRJ4Gr3].[dbo].[Software]
      WHERE ID = 3
GO


