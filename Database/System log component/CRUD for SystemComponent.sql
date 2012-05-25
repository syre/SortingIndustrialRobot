-- SELECT PART
SELECT [LogID]
      ,[LogEventType]
      ,[LogTime]
      ,[Description]
  FROM [F12I4PRJ4Gr3].[dbo].[Logs]
GO
SELECT [Type] FROM [F12I4PRJ4Gr3].[dbo].[LogType]
GO

-- INSERT PART
INSERT INTO [F12I4PRJ4Gr3].[dbo].[Logs]
           ([LogEventType]
           ,[LogTime]
           ,[Description])
     VALUES
           ('Info'
           ,21-21-11
           ,'Beskrivelse')
GO
INSERT INTO [F12I4PRJ4Gr3].[dbo].[LogType]
			([Type])
	 VALUES
			('CriticalError')
GO

-- UPDATE PART
UPDATE [F12I4PRJ4Gr3].[dbo].[Logs]
   SET [LogEventType] = LogEventType
      ,[LogTime] = LogTime
      ,[Description] = Description
 WHERE LogID > 5
GO

-- DELETE PART
DELETE FROM [F12I4PRJ4Gr3].[dbo].[Logs]
      WHERE LogID > 5
GO
