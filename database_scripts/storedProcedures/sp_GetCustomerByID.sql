CREATE PROCEDURE sp_GetCustomerByID
    @CustomerID BIGINT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        CustomerID,
        CustomerName,
        CustomerLastName,
        CustomerBirthDate,
        CustomerIdentityNumber,
        CustomerEmail
    FROM 
        Customer
    WHERE 
        CustomerID = @CustomerID;
END;
