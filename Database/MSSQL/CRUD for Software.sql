-- SELECT PART
SELECT [ID]
      ,[Name]
      ,[Description]
      ,[Producer]
  FROM [F12I4PRJ4Gr3].[dbo].[Software]
GO

-- INSERT PART
INSERT INTO [F12I4PRJ4Gr3].[dbo].[Software]
           ([ID]
           ,[Name]
           ,[Description]
           ,[Producer])
     VALUES
           (2
           ,'F'
           ,'F'
           ,2)
GO

-- UPDATE PART
UPDATE [F12I4PRJ4Gr3].[dbo].[Software]
   SET [ID] = ID
      ,[Name] = Name
      ,[Description] = Description
      ,[Producer] = Producer
 WHERE ID = 1
GO



-- DELETE PART
DELETE FROM [F12I4PRJ4Gr3].[dbo].[Software]
      WHERE ID = 3
GO


