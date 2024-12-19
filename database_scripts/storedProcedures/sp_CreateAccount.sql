CREATE PROCEDURE sp_CreateAccount
    @CustomerID BIGINT,
    @AccountName VARCHAR(50),
    @CurrencyType VARCHAR(50),
    @AccountBalance DECIMAL(18, 2)
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO Account (CustomerID, AccountName, CurrencyType, AccountBalance)
    VALUES (@CustomerID, @AccountName, @CurrencyType, @AccountBalance);

	SELECT SCOPE_IDENTITY() AS AccountID
END;
