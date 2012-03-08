-- SELECT PART
SELECT [BoxID]
      ,[PositionID]
      ,[Length]
      ,[Width]
      ,[Depth]
      ,[Weight]
  FROM [F12I4PRJ4Gr3].[dbo].[BoxInfo]
GO
-- INSERT PART
INSERT INTO [F12I4PRJ4Gr3].[dbo].[BoxInfo]
           ([PositionID]
           ,[Length]
           ,[Width]
           ,[Depth]
           ,[Weight])
     VALUES
           (1
           ,120
           ,12
           ,22
           ,2.4)
GO

-- UPDATE PART
UPDATE [F12I4PRJ4Gr3].[dbo].[BoxInfo]
   SET [PositionID] = PositionID
      ,[Length] = Length
      ,[Width] = Width
      ,[Depth] = Depth
      ,[Weight] = Weight
 WHERE BoxID > 1
GO



-- DELETE PART
DELETE FROM [F12I4PRJ4Gr3].[dbo].[BoxInfo]
      WHERE Length = 100
GO

