/* sequence tanýmlama */
CREATE SEQUENCE CustomerID_sequence
	AS BIGINT
	START WITH 120000000001 
	INCREMENT BY 1;

CREATE SEQUENCE AccountID_sequence
	AS BIGINT	
	START WITH 150000000001 
	INCREMENT BY 1;

CREATE SEQUENCE TransactionID_sequence 
	AS INT
	START WITH 1 
	INCREMENT BY 1;