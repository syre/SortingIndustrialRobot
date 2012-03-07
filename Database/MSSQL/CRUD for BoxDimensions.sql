-- SELECT PART
SELECT [BoxID]
      ,[Length]
      ,[Width]
      ,[Depth]
  FROM [F12I4PRJ4Gr3].[dbo].[BoxDimensions]
GO

-- INSERT PART
INSERT INTO [F12I4PRJ4Gr3].[dbo].[BoxDimensions]
           ([Length]
           ,[Width]
           ,[Depth])
     VALUES
           (100
           ,10
           ,20)
GO

-- UPDATE PART
UPDATE [F12I4PRJ4Gr3].[dbo].[BoxDimensions]
   SET [Length] = Length
      ,[Width] = Width
      ,[Depth] = Depth
 WHERE Length = 100
GO

-- DELETE PART
DELETE FROM [F12I4PRJ4Gr3].[dbo].[BoxDimensions]
      WHERE BoxID = 7
GO