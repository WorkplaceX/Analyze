/* Call WAITFOR DELAY inside SQL View */
/* Usefull for async testing */

CREATE FUNCTION WaitForDelay()
RETURNS INT
AS
BEGIN
  RETURN (
    SELECT Value FROM OPENROWSET (
	'SQLOLEDB', 'Trusted_Connection=yes;  Integrated Security=SSPI; Server=localhost; Initial_Catalog=master;',
	'WAITFOR DELAY ''00:00:02'' SELECT 0 AS Value'
  ))
END

GO
CREATE VIEW Wait AS
SELECT dbo.WaitForDelay() AS Value

GO
SELECT * FROM Wait /* Takes view 2 seconds to respond */