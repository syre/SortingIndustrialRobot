SELECT [ID]
      ,[Name]
      ,[Description]
      ,[Producer]
  FROM [F12I4PRJ4Gr3].[dbo].[Components]
GO


INSERT INTO [F12I4PRJ4Gr3].[dbo].[Components]
           ([Name]
           ,[Description]
           ,[Producer])
     VALUES
           (<Name, varchar(20),>
           ,<Description, varchar(255),>
           ,<Producer, int,>)
GO


UPDATE [F12I4PRJ4Gr3].[dbo].[Components]
   SET [Name] = <Name, varchar(20),>
      ,[Description] = <Description, varchar(255),>
      ,[Producer] = <Producer, int,>
 WHERE <Search Conditions,,>
GO


DELETE FROM [F12I4PRJ4Gr3].[dbo].[Components]
      WHERE <Search Conditions,,>
GO

