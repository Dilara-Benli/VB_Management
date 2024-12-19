CREATE PROCEDURE sp_Deposit
    @CustomerID BIGINT,
    @AccountID BIGINT,
    @Amount DECIMAL(18,2),
    @Explanation VARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE Account
    SET AccountBalance = AccountBalance + @Amount
    WHERE AccountID = @AccountID AND CustomerID = @CustomerID;

    INSERT INTO [Transaction] (AccountID, TransactionTypeName, TransactionAmount, TransactionDate, TransactionExplanation)
    VALUES (@AccountID, 'Para Yatýrma', @Amount, GETDATE(), @Explanation);

    SELECT SCOPE_IDENTITY() AS TransactionID, 
           (SELECT AccountBalance FROM Account WHERE AccountID = @AccountID AND CustomerID = @CustomerID) AS NewBalance;
END;
