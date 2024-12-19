CREATE TABLE Customer
(
    CustomerID BIGINT DEFAULT NEXT VALUE FOR CustomerID_sequence PRIMARY KEY,
    CustomerName VARCHAR(50) NOT NULL,
	CustomerLastName VARCHAR(50) NOT NULL,
	CustomerIdentityNumber BIGINT UNIQUE NOT NULL,
	CustomerBirthDate DATE NOT NULL,
	CustomerEmail VARCHAR(50) UNIQUE NOT NULL,
	CustomerPasswordHash VARCHAR(50) NOT NULL
);

CREATE TABLE Currency
(
	CurrencyType VARCHAR(50) PRIMARY KEY
);

CREATE TABLE TransactionType
(
	TransactionTypeName VARCHAR(50) PRIMARY KEY
);

CREATE TABLE Account
(
    AccountID BIGINT DEFAULT NEXT VALUE FOR AccountID_sequence PRIMARY KEY,
	CustomerID BIGINT NOT NULL,
    AccountName VARCHAR(50) NOT NULL,
	CurrencyType VARCHAR(50) NOT NULL,
	AccountBalance DECIMAL(18,2) NOT NULL,
	CONSTRAINT FK_Account_Customer FOREIGN KEY (CustomerID) REFERENCES Customer(CustomerID) 
        ON DELETE CASCADE 
        ON UPDATE CASCADE,
	CONSTRAINT FK_Account_Currency FOREIGN KEY (CurrencyType) REFERENCES Currency(CurrencyType) 
        ON DELETE CASCADE 
        ON UPDATE CASCADE
);

CREATE TABLE [Transaction]
(
    TransactionID INT DEFAULT NEXT VALUE FOR TransactionID_sequence PRIMARY KEY,
	AccountID BIGINT NOT NULL,
    TransactionTypeName VARCHAR(50) NOT NULL,
	TransactionAmount DECIMAL(18,2) NOT NULL,
	TransactionDate DATE NOT NULL,
	TransactionExplanation VARCHAR(50) NOT NULL,
	CONSTRAINT FK_Transaction_Account FOREIGN KEY (AccountID) REFERENCES Account(AccountID) 
        ON DELETE CASCADE 
        ON UPDATE CASCADE,
	CONSTRAINT FK_Transaction_TransactionType FOREIGN KEY (TransactionTypeName) REFERENCES TransactionType(TransactionTypeName) 
        ON DELETE CASCADE 
        ON UPDATE CASCADE
);

CREATE TABLE [Transfer]
(
    TransferID INT DEFAULT NEXT VALUE FOR TransactionID_sequence PRIMARY KEY,
    TransactionID INT NOT NULL,
	SourceAccountID BIGINT NOT NULL,
	TargetAccountID BIGINT NOT NULL
	CONSTRAINT FK_Transfer_Transaction FOREIGN KEY (TransactionID) REFERENCES [Transaction](TransactionID) 
        ON DELETE CASCADE 
        ON UPDATE CASCADE
);

