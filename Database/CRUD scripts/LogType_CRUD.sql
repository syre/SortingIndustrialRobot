SELECT [Type]
  FROM [F12I4PRJ4Gr3].[dbo].[LogType]
GO


INSERT INTO [F12I4PRJ4Gr3].[dbo].[LogType]
           ([Type])
     VALUES
           (<Type, varchar(50),>)
GO


UPDATE [F12I4PRJ4Gr3].[dbo].[LogType]
   SET [Type] = <Type, varchar(50),>
 WHERE <Search Conditions,,>
GO


DELETE FROM [F12I4PRJ4Gr3].[dbo].[LogType]
      WHERE <Search Conditions,,>
GO

