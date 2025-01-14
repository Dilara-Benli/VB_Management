CREATE PROCEDURE sp_Withdraw
    @CustomerID BIGINT,
    @AccountID BIGINT,
    @Amount DECIMAL(18,2),
    @Explanation NVARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;

    -- Para �ekme i�lemi
    UPDATE Account
    SET AccountBalance = AccountBalance - @Amount
    WHERE AccountID = @AccountID AND CustomerID = @CustomerID;

    -- ��lem kayd�n� ekle
    INSERT INTO [Transaction] (AccountID, TransactionTypeName, TransactionAmount, TransactionDate, TransactionExplanation)
    VALUES (@AccountID, 'Para �ekme', @Amount, GETDATE(), @Explanation);

	SELECT SCOPE_IDENTITY() AS TransactionID, 
           (SELECT AccountBalance FROM Account WHERE AccountID = @AccountID AND CustomerID = @CustomerID) AS NewBalance;

END;
