/* SQL Copy */
BACKUP DATABASE Database TO DISK = 'C:\Temp\Db\Database.BAK' /* Backup */
GO
RESTORE FILELISTONLY FROM DISK='C:\Temp\Db\Database.BAK' /* Show logical name */
GO
ALTER DATABASE Database2 SET SINGLE_USER WITH ROLLBACK IMMEDIATE /* Disconnect database */
GO
RESTORE DATABASE Database2 FROM DISK='C:\Temp\Db\Database.BAK' /* Restore */
WITH 
   MOVE 'Database' TO 'C:\Temp\Db\MyTempCopy2.mdf',
   MOVE 'Database_log' TO 'C:\Temp\Db\MyTempCopy2_log.ldf'