SELECT [ID]
      ,[Name]
      ,[Password]
      ,[Description]
      ,[Permission]
  FROM [F12I4PRJ4Gr3].[dbo].[Users]
GO


INSERT INTO [F12I4PRJ4Gr3].[dbo].[Users]
           ([Name]
           ,[Password]
           ,[Description]
           ,[Permission])
     VALUES
           (<Name, varchar(20),>
           ,<Password, varchar(20),>
           ,<Description, varchar(255),>
           ,<Permission, int,>)
GO

UPDATE [F12I4PRJ4Gr3].[dbo].[Users]
   SET [Name] = <Name, varchar(20),>
      ,[Password] = <Password, varchar(20),>
      ,[Description] = <Description, varchar(255),>
      ,[Permission] = <Permission, int,>
 WHERE <Search Conditions,,>
GO

DELETE FROM [F12I4PRJ4Gr3].[dbo].[Users]
      WHERE <Search Conditions,,>
GO

