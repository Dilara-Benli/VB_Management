CREATE PROCEDURE sp_GetAccountsByCustomerID
    @CustomerID BIGINT
AS
BEGIN
	SET NOCOUNT ON;

    SELECT AccountID, CustomerID, AccountName, CurrencyType, AccountBalance
    FROM Account
    WHERE CustomerID = @CustomerID
END
