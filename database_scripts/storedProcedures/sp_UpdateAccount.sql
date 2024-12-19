CREATE PROCEDURE sp_UpdateAccount
    @AccountID BIGINT,
    @AccountName NVARCHAR(50),
    @CurrencyType NVARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE Account
    SET 
        AccountName = ISNULL(@AccountName, AccountName), 
        CurrencyType = ISNULL(@CurrencyType, CurrencyType) 
    WHERE AccountID = @AccountID;

END;
