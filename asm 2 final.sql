
Create Database QLBH
Use QLBH
CREATE TABLE Customer (
    CustomerID INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(100),
    Phone NVARCHAR(20),
    Address NVARCHAR(200)
);
CREATE TABLE Invoice (
    InvoiceID INT PRIMARY KEY IDENTITY(1,1),
    ProductID INT FOREIGN KEY REFERENCES Product(ProductID),
    CustomerID INT FOREIGN KEY REFERENCES Customer(CustomerID),
    NameProduct NVARCHAR(100),
    NameCustomer NVARCHAR(100),
    PhoneNumber NVARCHAR(20),
    Quantity INT,
    Address NVARCHAR(255),
    Price DECIMAL(18,2),
    OrderDate DATETIME,
    Description NVARCHAR(MAX)
);
Create table account (
Username_ varchar(50) primary key ,
password_ varchar(50) not null,
email varchar(50) )


Create table Product 
(  ProductID int primary key not null,
	Quantity int not null,
	Price int not null,
	description varchar(100) 
)


Create table Employee (
EmpId  varchar(5) not null primary key,
name varchar(100) not null,
Sex nvarchar(20) ,
Birthday datetime ,
Address varchar(100) ,
Email varchar(100)
)
Drop Table Employyee

Create table Customer(
CustomerID int primary key not null identity(1,1),
Name nvarchar(100),
Phone nvarchar(20),
Address nvarchar(200) )

CREATE PROCEDURE SEARCH_BY_Name
@productname nvarchar(50)
as
SELECT *
FROM Product
WHERE Productname=@productname


CREATE MASTER KEY ENCRYPTION BY PASSWORD = 'admin';
CREATE CERTIFICATE MyEncryptionCert WITH SUBJECT = 'Always Encrypted Certificate';
CREATE COLUMN ENCRYPTION KEY MyEncryptionCert
WITH VALUES
(
    COLUMN_MASTER_KEY = MyEncryptionCert,
    ALGORITHM = 'RSA_OAEP',
    ENCRYPTED_VALUE = ENCRYPTED_BY_MASTER_KEY
);
ALTER TABLE Customer
ALTER COLUMN Name ADD ENCRYPTED WITH (COLUMN_ENCRYPTION_KEY = MyEncryptionCert, ENCRYPTION_TYPE = Deterministic,
ALGORITHM = 'AEAD_AES_256_CBC_HMAC_SHA_256');

ALTER TABLE Customer
ALTER COLUMN Phone ADD ENCRYPTED WITH (COLUMN_ENCRYPTION_KEY = MyEncryptionCert, ENCRYPTION_TYPE = Deterministic,
ALGORITHM = 'AEAD_AES_256_CBC_HMAC_SHA_256');

ALTER TABLE Customer
ALTER COLUMN Address ADD ENCRYPTED WITH (COLUMN_ENCRYPTION_KEY = MyEncryptionCert, ENCRYPTION_TYPE = Deterministic, 
ALGORITHM = 'AEAD_AES_256_CBC_HMAC_SHA_256');








select * from account
select * from Product
select * from Employee
select *from Invoice
select * from account