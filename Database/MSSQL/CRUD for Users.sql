-- SELECT PART
SELECT [Name]
      ,[Description]
      ,[Permission]
  FROM [F12I4PRJ4Gr3].[dbo].[Users]
GO

-- INSERT PART
INSERT INTO [F12I4PRJ4Gr3].[dbo].[Users]
           ([Name]
           ,[Description]
           ,[Permission])
     VALUES
           ('G'
           ,'G'
           ,2)
GO

-- UPDATE PART
UPDATE [F12I4PRJ4Gr3].[dbo].[Users]
   SET [Name] = Name
      ,[Description] = Description
      ,[Permission] = Permission
 WHERE Permission = 1
GO

-- DELETE PART
DELETE FROM [F12I4PRJ4Gr3].[dbo].[Users]
      WHERE ID < 2
GO


