SELECT [LogID]
      ,[LogEventType]
      ,[LogTime]
      ,[Description]
  FROM [F12I4PRJ4Gr3].[dbo].[Logs]
GO

INSERT INTO [F12I4PRJ4Gr3].[dbo].[Logs]
           ([LogEventType]
           ,[LogTime]
           ,[Description])
     VALUES
           (<LogEventType, varchar(50),>
           ,<LogTime, datetime,>
           ,<Description, varchar(50),>)
GO


UPDATE [F12I4PRJ4Gr3].[dbo].[Logs]
   SET [LogEventType] = <LogEventType, varchar(50),>
      ,[LogTime] = <LogTime, datetime,>
      ,[Description] = <Description, varchar(50),>
 WHERE <Search Conditions,,>
GO


DELETE FROM [F12I4PRJ4Gr3].[dbo].[Logs]
      WHERE <Search Conditions,,>
GO

