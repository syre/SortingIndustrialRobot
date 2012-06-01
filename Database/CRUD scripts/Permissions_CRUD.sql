SELECT [ID]
      ,[Name]
  FROM [F12I4PRJ4Gr3].[dbo].[Permissions]
GO


INSERT INTO [F12I4PRJ4Gr3].[dbo].[Permissions]
           ([Name])
     VALUES
           (<Name, varchar(20),>)
GO

UPDATE [F12I4PRJ4Gr3].[dbo].[Permissions]
   SET [Name] = <Name, varchar(20),>
 WHERE <Search Conditions,,>
GO

DELETE FROM [F12I4PRJ4Gr3].[dbo].[Permissions]
      WHERE <Search Conditions,,>
GO

