CREATE PROCEDURE sp_GetAccountBalance
    @CustomerID BIGINT,
    @AccountID BIGINT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT AccountBalance
    FROM Account
    WHERE AccountID = @AccountID AND CustomerID = @CustomerID;
END;
