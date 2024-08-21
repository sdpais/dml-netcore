CREATE TABLE  IF NOT EXISTS  Logins (
    Id serial NOT NULL, 
    UserName Varchar (256) NOT NULL,
    Password Varchar (256) NOT NULL,
    EncryptionKeyId INT NOT NULL,
    EmployeeId INT NULL,
    CreatedDate Date DEFAULT CURRENT_DATE,
    UpdatedDate Date DEFAULT CURRENT_DATE,
    CONSTRAINT Pk_Logins_id PRIMARY KEY(Id)
);

ALTER TABLE Logins
ADD CONSTRAINT FK_Logins_EncryptionKeys_Id
FOREIGN KEY (EncryptionKeyId)
REFERENCES EncryptionKeys(Id);

ALTER TABLE Logins
ADD CONSTRAINT FK_Logins_Employees_Id
FOREIGN KEY (EmployeeId)
REFERENCES Employees(Id);

ALTER TABLE Logins
ADD CONSTRAINT U_Logins_UserName
UNIQUE (UserName);