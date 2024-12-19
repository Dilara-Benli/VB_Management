CREATE PROCEDURE sp_GetAllTransactions
AS
BEGIN
    SELECT 
		TransactionID, 
		AccountID, 
		TransactionTypeName, 
		TransactionAmount, 
		TransactionDate, 
		TransactionExplanation
    FROM 
		[Transaction]
END
