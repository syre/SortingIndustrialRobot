SELECT [ID]
      ,[Color]
      ,[MinDensity]
      ,[MaxDensity]
  FROM [F12I4PRJ4Gr3].[dbo].[Category]
GO


INSERT INTO [F12I4PRJ4Gr3].[dbo].[Category]
           ([Color]
           ,[MinDensity]
           ,[MaxDensity])
     VALUES
           (<Color, varchar(20),>
           ,<MinDensity, float,>
           ,<MaxDensity, float,>)
GO

UPDATE [F12I4PRJ4Gr3].[dbo].[Category]
   SET [Color] = <Color, varchar(20),>
      ,[MinDensity] = <MinDensity, float,>
      ,[MaxDensity] = <MaxDensity, float,>
 WHERE <Search Conditions,,>
GO

DELETE FROM [F12I4PRJ4Gr3].[dbo].[Category]
      WHERE <Search Conditions,,>
GO

