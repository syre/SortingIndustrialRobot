-- SELECT PART
SELECT [BoxID]
      ,[Length]
      ,[Width]
      ,[Depth]
      ,[Weight]
  FROM [F12I4PRJ4Gr3].[dbo].[BoxInfo]
GO

-- INSERT PART
INSERT INTO [F12I4PRJ4Gr3].[dbo].[BoxInfo]
           ([Length]
           ,[Width]
           ,[Depth]
           ,[Weight])
     VALUES
           (100
           ,101
           ,20
           ,2.2)
GO

-- UPDATE PART
UPDATE [F12I4PRJ4Gr3].[dbo].[BoxInfo]
   SET [Length] = Length
      ,[Width] = Width
      ,[Depth] = Depth
      ,[Weight] = Weight
 WHERE Length = 100
GO

-- DELETE PART
DELETE FROM [F12I4PRJ4Gr3].[dbo].[BoxInfo]
      WHERE BoxID > 7
GO