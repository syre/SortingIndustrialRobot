-- SELECT PART
SELECT [LogID]
      ,[LogEvent]
      ,[LogTime]
      ,[Description]
      ,[PositionID]
  FROM [F12I4PRJ4Gr3].[dbo].[SystemComponentTable]
GO

-- INSERT PART
INSERT INTO [F12I4PRJ4Gr3].[dbo].[SystemComponentTable]
           ([LogEvent]
           ,[LogTime]
           ,[Description]
           ,[PositionID])
     VALUES
           ('Logevents'
           ,21-21-11
           ,'Beskrivelse'
           ,12)
GO

-- UPDATE PART
UPDATE [F12I4PRJ4Gr3].[dbo].[SystemComponentTable]
   SET [LogEvent] = LogEvent
      ,[LogTime] = LogTime
      ,[Description] = Description
      ,[PositionID] = PositionID
 WHERE LogID > 5
GO

-- DELETE PART
DELETE FROM [F12I4PRJ4Gr3].[dbo].[SystemComponentTable]
      WHERE LogID > 5
GO
