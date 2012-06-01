SELECT [PositionID]
      ,[X]
      ,[Y]
      ,[Z]
      ,[Pitch]
      ,[Roll]
      ,[Occupied]
      ,[CategoryID]
  FROM [F12I4PRJ4Gr3].[dbo].[Position]
GO


INSERT INTO [F12I4PRJ4Gr3].[dbo].[Position]
           ([X]
           ,[Y]
           ,[Z]
           ,[Pitch]
           ,[Roll]
           ,[Occupied]
           ,[CategoryID])
     VALUES
           (<X, int,>
           ,<Y, int,>
           ,<Z, int,>
           ,<Pitch, int,>
           ,<Roll, int,>
           ,<Occupied, bit,>
           ,<CategoryID, int,>)
GO


UPDATE [F12I4PRJ4Gr3].[dbo].[Position]
   SET [X] = <X, int,>
      ,[Y] = <Y, int,>
      ,[Z] = <Z, int,>
      ,[Pitch] = <Pitch, int,>
      ,[Roll] = <Roll, int,>
      ,[Occupied] = <Occupied, bit,>
      ,[CategoryID] = <CategoryID, int,>
 WHERE <Search Conditions,,>
GO

DELETE FROM [F12I4PRJ4Gr3].[dbo].[Position]
      WHERE <Search Conditions,,>
GO



