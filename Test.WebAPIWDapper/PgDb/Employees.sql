CREATE TABLE IF NOT EXISTS Employees (
    Id serial PRIMARY KEY NOT NULL, 
    Name Varchar (256) NOT NULL,
    DateofBirth Varchar (256) NULL,
    MobileNumber Varchar (25),
    EncryptionKeyId INT NOT NULL,
    SSN Varchar(500) NULL,
    CreatedDate Date

);


ALTER TABLE Employees
ADD CONSTRAINT FK_Employees_EncryptionKeys_Id
FOREIGN KEY (EncryptionKeyId)
REFERENCES EncryptionKeys(Id);

