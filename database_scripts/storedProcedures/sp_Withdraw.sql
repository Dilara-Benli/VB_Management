CREATE PROCEDURE sp_Withdraw
    @CustomerID BIGINT,
    @AccountID BIGINT,
    @Amount DECIMAL(18,2),
    @Explanation NVARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;

    -- Para çekme iþlemi
    UPDATE Account
    SET AccountBalance = AccountBalance - @Amount
    WHERE AccountID = @AccountID AND CustomerID = @CustomerID;

    -- Ýþlem kaydýný ekle
    INSERT INTO [Transaction] (AccountID, TransactionTypeName, TransactionAmount, TransactionDate, TransactionExplanation)
    VALUES (@AccountID, 'Para Çekme', @Amount, GETDATE(), @Explanation);

	SELECT SCOPE_IDENTITY() AS TransactionID, 
           (SELECT AccountBalance FROM Account WHERE AccountID = @AccountID AND CustomerID = @CustomerID) AS NewBalance;

END;
