CREATE PROCEDURE sp_GetTransactionsByCustomerID
    @CustomerID BIGINT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        t.TransactionID,
        t.AccountID,
        t.TransactionTypeName,
        t.TransactionAmount,
        t.TransactionDate,
        t.TransactionExplanation
    FROM 
        [Transaction] t
    JOIN 
        Account a ON t.AccountID = a.AccountID
    WHERE 
        a.CustomerID = @CustomerID;
END;
