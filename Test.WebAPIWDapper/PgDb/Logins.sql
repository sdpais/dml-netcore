CREATE TABLE  IF NOT EXISTS  Logins (
    Id serial PRIMARY KEY NOT NULL, 
    UserName Varchar (256) NOT NULL,
    Password Varchar (256) NOT NULL,
    EncryptionKeyId INT NOT NULL,
    EmployeeId INT NOT NULL
);

ALTER TABLE Logins
ADD CONSTRAINT FK_Logins_EncryptionKeys_Id
FOREIGN KEY (EncryptionKeyId)
REFERENCES EncryptionKeys(Id);

ALTER TABLE Logins
ADD CONSTRAINT FK_Logins_Employees_Id
FOREIGN KEY (EmployeeId)
REFERENCES Employees(Id);