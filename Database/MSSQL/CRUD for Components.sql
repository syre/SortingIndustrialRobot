-- SELECT PART
SELECT [ID]
      ,[Name]
      ,[Description]
      ,[Producer]
  FROM [F12I4PRJ4Gr3].[dbo].[Components]
GO

-- INSERT PART
INSERT INTO [F12I4PRJ4Gr3].[dbo].[Components]
           ([ID]
           ,[Name]
           ,[Description]
           ,[Producer])
     VALUES
           (2
           ,'a'
           ,'Hello world'
           ,9)
GO
-- UPDATE PART

UPDATE [F12I4PRJ4Gr3].[dbo].[Components]
   SET [ID] = ID
      ,[Name] = Name
      ,[Description] = Description
      ,[Producer] = Producer
 WHERE ID = 2
GO

-- DELETE PART
DELETE FROM [F12I4PRJ4Gr3].[dbo].[Components]
      WHERE ID = 1
GO



