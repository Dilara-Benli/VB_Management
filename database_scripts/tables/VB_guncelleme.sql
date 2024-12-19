/* s�tunun veri tipini g�ncelleme */
ALTER TABLE Customer
ALTER COLUMN CustomerPasswordHash VARCHAR(60) NOT NULL;

/* stored procedure silme */
DROP PROCEDURE sp_TransferMoney;

/* CurrencyType default de�er olu�turma */
INSERT INTO Currency (CurrencyType)
VALUES ('TL');

/* TransactionTypeName default de�er olu�turma */
INSERT INTO TransactionType (TransactionTypeName)
VALUES ('Para Yat�rma'), ('Para �ekme'), ('Transfer');
