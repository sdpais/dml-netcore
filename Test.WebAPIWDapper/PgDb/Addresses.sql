CREATE TABLE IF NOT EXISTS Addresses (
    Id serial NOT NULL, 
    Address1 Varchar (256) NOT NULL,
    Address2 Varchar (256) NOT NULL,
    City Varchar (256) NOT NULL,
    Province Varchar(50) NOT NULL,
    Country Varchar(50) NOT NULL,
    EmployeeId INT NOT NULL,
    CreatedDate Date DEFAULT CURRENT_DATE,
    UpdatedDate Date DEFAULT CURRENT_DATE,
    CONSTRAINT Pk_Addresses_id PRIMARY KEY(Id)
);

ALTER TABLE Addresses
ADD CONSTRAINT FK_Addresses_Employees_Id
FOREIGN KEY (EmployeeId)
REFERENCES Employees(Id);