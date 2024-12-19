CREATE PROCEDURE sp_GetTransactionsByAccountID
    @AccountID BIGINT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        TransactionID,
        AccountID,
        TransactionTypeName,
        TransactionAmount,
        TransactionDate,
        TransactionExplanation
    FROM 
        [Transaction]
    WHERE 
        AccountID = @AccountID;
END;
