CREATE PROCEDURE sp_GetAllAccounts
AS
BEGIN

    SELECT AccountID, CustomerID, AccountName, CurrencyType, AccountBalance 
    FROM Account
END
