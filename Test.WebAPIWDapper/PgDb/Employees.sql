CREATE TABLE IF NOT EXISTS Employees (
    Id serial PRIMARY KEY NOT NULL, 
    Name Varchar (256) NOT NULL,
    Age INT,
    AddressId INT,
    MobileNumber Varchar (25),
    EncryptionKeyId INT NOT NULL,
    SSN Varchar(500) NULL,
    CreatedDate Date

);


ALTER TABLE Employees
ADD CONSTRAINT FK_Employees_EncryptionKeys_Id
FOREIGN KEY (EncryptionKeyId)
REFERENCES EncryptionKeys(Id);

ALTER TABLE Employees
ADD CONSTRAINT FK_Employees_Addresses_Id
FOREIGN KEY (AddressId)
REFERENCES Addresses(Id);
