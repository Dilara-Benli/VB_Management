/* sütunun veri tipini güncelleme */
ALTER TABLE Customer
ALTER COLUMN CustomerPasswordHash VARCHAR(60) NOT NULL;

/* stored procedure silme */
DROP PROCEDURE sp_TransferMoney;

/* CurrencyType default deðer oluþturma */
INSERT INTO Currency (CurrencyType)
VALUES ('TL');

/* TransactionTypeName default deðer oluþturma */
INSERT INTO TransactionType (TransactionTypeName)
VALUES ('Para Yatýrma'), ('Para Çekme'), ('Transfer');
