CREATE PROCEDURE sp_RegisterCustomer
    @CustomerName VARCHAR(50),
    @CustomerLastName VARCHAR(50),
    @CustomerBirthDate DATE,
    @CustomerIdentityNumber BIGINT,
    @CustomerEmail VARCHAR(50),
    @CustomerPasswordHash VARCHAR(60)
AS
BEGIN
    SET NOCOUNT ON;
	
    INSERT INTO Customer (CustomerName, CustomerLastName, CustomerBirthDate, CustomerIdentityNumber, CustomerEmail, CustomerPasswordHash)
    VALUES (@CustomerName, @CustomerLastName, @CustomerBirthDate, @CustomerIdentityNumber, @CustomerEmail, @CustomerPasswordHash);

END
