CREATE PROCEDURE sp_DeleteCustomer
    @CustomerID BIGINT
AS
BEGIN
    SET NOCOUNT ON;

    DELETE FROM Customer WHERE CustomerID = @CustomerID;
END
