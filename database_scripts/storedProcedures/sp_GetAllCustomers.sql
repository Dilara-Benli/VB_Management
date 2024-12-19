CREATE PROCEDURE sp_GetAllCustomers
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
        Customer;
END;
