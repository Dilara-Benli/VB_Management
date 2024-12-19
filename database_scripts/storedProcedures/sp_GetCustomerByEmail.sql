CREATE PROCEDURE sp_GetCustomerByEmail
    @Email VARCHAR(50)
AS
BEGIN
	SET NOCOUNT ON;

    SELECT CustomerID, CustomerName, CustomerLastName, CustomerBirthDate, CustomerIdentityNumber, CustomerEmail, CustomerPasswordHash
    FROM Customer
    WHERE CustomerEmail = @Email;
END
