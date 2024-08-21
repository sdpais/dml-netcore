CREATE TABLE IF NOT EXISTS Employees (
    Id serial NOT NULL, 
    Name Varchar (256) NOT NULL,
    Email Varchar (256) NOT NULL,
    DateOfBirth Varchar (256) NULL,
    MobileNumber Varchar (25),
    EncryptionKeyId INT NOT NULL,
    SSN Varchar(500) NULL,
    CreatedDate Date DEFAULT CURRENT_DATE,
    UpdatedDate Date DEFAULT CURRENT_DATE,
    CONSTRAINT Pk_Employees_id PRIMARY KEY(Id)
);


ALTER TABLE Employees
ADD CONSTRAINT FK_Employees_EncryptionKeys_Id
FOREIGN KEY (EncryptionKeyId)
REFERENCES EncryptionKeys(Id);

