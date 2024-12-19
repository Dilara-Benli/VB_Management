CREATE PROCEDURE sp_TransferMoney
    @SourceCustomerID BIGINT,
    @SourceAccountID BIGINT,
    @TargetAccountID BIGINT,
    @Amount DECIMAL(18,2),
    @Explanation VARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;
	
    -- Transfer iþlemi
    UPDATE Account
    SET AccountBalance = AccountBalance - @Amount
    WHERE AccountID = @SourceAccountID;

    UPDATE Account
    SET AccountBalance = AccountBalance + @Amount
    WHERE AccountID = @TargetAccountID;

	-- TransactionID'yi tutmak için table variable oluþtur
    DECLARE @TransactionTable TABLE (TransactionID INT);

    INSERT INTO [Transaction] (AccountID, TransactionTypeName, TransactionAmount, TransactionDate, TransactionExplanation)
    OUTPUT INSERTED.TransactionID INTO @TransactionTable
    VALUES (@SourceAccountID, 'Transfer', @Amount, GETDATE(), @Explanation);

    -- TransactionID'yi al
	DECLARE @TransactionID INT;
	SELECT TOP 1 @TransactionID = TransactionID FROM @TransactionTable;

    -- Transfer kaydýný ekle
    INSERT INTO [Transfer] (TransactionID, SourceAccountID, TargetAccountID)
    VALUES (@TransactionID, @SourceAccountID, @TargetAccountID);

    -- Yeni bakiyeyi döndür
    SELECT @TransactionID AS TransactionID, 
           (SELECT AccountBalance FROM Account WHERE AccountID = @SourceAccountID) AS SourceNewBalance,
           (SELECT AccountBalance FROM Account WHERE AccountID = @TargetAccountID) AS TargetNewBalance;
END;
