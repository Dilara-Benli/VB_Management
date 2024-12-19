CREATE PROCEDURE sp_UpdateCustomer
	@CustomerID BIGINT,
    @CustomerName VARCHAR(50) = NULL,
    @CustomerLastName VARCHAR(50) = NULL,
    @CustomerBirthDate DATE = NULL,
    @CustomerIdentityNumber BIGINT = NULL,
    @CustomerEmail VARCHAR(50) = NULL,
    @CustomerPasswordHash VARCHAR(60) = NULL
AS
BEGIN
	SET NOCOUNT ON;

    UPDATE Customer
    SET 
        CustomerName = ISNULL(@CustomerName, CustomerName),
        CustomerLastName = ISNULL(@CustomerLastName, CustomerLastName),
        CustomerBirthDate = ISNULL(@CustomerBirthDate, CustomerBirthDate),
        CustomerIdentityNumber = ISNULL(@CustomerIdentityNumber, CustomerIdentityNumber),
        CustomerEmail = ISNULL(@CustomerEmail, CustomerEmail),
        CustomerPasswordHash = ISNULL(@CustomerPasswordHash, CustomerPasswordHash)
    WHERE CustomerID = @CustomerID;
END
