CREATE PROCEDURE sp_DeleteAccount
    @AccountID BIGINT
AS
BEGIN
    SET NOCOUNT ON;

    DELETE FROM Account WHERE AccountID = @AccountID;
END;
