UPDATE [F12I4PRJ4Gr3].[dbo].[SystemComponentTable]
   SET [LogID] = LogID
      ,[LogEvent] = LogEvent
      ,[LogTime] = LogTime
      ,[Description] = Description
      ,[Position] = Position
 WHERE LogID > 1
GO


