INSERT INTO [F12I4PRJ4Gr3].[dbo].[BoxInfo]
           ([PositionID]
           ,[Length]
           ,[Width]
           ,[Depth]
           ,[Weight])
     VALUES
           (<PositionID, int,>
           ,<Length, float,>
           ,<Width, float,>
           ,<Depth, float,>
           ,<Weight, float,>)
GO

SELECT [BoxID]
      ,[PositionID]
      ,[Length]
      ,[Width]
      ,[Depth]
      ,[Weight]
  FROM [F12I4PRJ4Gr3].[dbo].[BoxInfo]
GO

UPDATE [F12I4PRJ4Gr3].[dbo].[BoxInfo]
   SET [PositionID] = <PositionID, int,>
      ,[Length] = <Length, float,>
      ,[Width] = <Width, float,>
      ,[Depth] = <Depth, float,>
      ,[Weight] = <Weight, float,>
 WHERE <Search Conditions,,>
GO



DELETE FROM [F12I4PRJ4Gr3].[dbo].[BoxInfo]
      WHERE <Search Conditions,,>
GO

