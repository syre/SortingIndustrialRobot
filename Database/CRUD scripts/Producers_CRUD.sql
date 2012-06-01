SELECT [ID]
      ,[Name]
      ,[Description]
  FROM [F12I4PRJ4Gr3].[dbo].[Producers]
GO


INSERT INTO [F12I4PRJ4Gr3].[dbo].[Producers]
           ([Name]
           ,[Description])
     VALUES
           (<Name, varchar(20),>
           ,<Description, varchar(255),>)
GO

UPDATE [F12I4PRJ4Gr3].[dbo].[Producers]
   SET [Name] = <Name, varchar(20),>
      ,[Description] = <Description, varchar(255),>
 WHERE <Search Conditions,,>
GO

DELETE FROM [F12I4PRJ4Gr3].[dbo].[Producers]
      WHERE <Search Conditions,,>
GO

