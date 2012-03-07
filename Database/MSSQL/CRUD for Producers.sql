-- SELECT PART
SELECT [ID]
      ,[Name]
      ,[Description]
  FROM [F12I4PRJ4Gr3].[dbo].[Producers]
GO

-- INSERT PART
INSERT INTO [F12I4PRJ4Gr3].[dbo].[Producers]
           ([ID]
           ,[Name]
           ,[Description])
     VALUES
           (3
           ,'C'
           ,'C')
GO



-- UPDATE PART
UPDATE [F12I4PRJ4Gr3].[dbo].[Producers]
   SET [ID] = ID
      ,[Name] = Name
      ,[Description] = Description
 WHERE ID > 1
GO


-- DELETE PART
DELETE FROM [F12I4PRJ4Gr3].[dbo].[Producers]
      WHERE Description = 'B'
GO

