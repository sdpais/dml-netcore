CREATE TABLE IF NOT EXISTS Addresses (
    Id serial NOT NULL, 
    Address1 Varchar (256) NOT NULL,
    Address2 Varchar (256) NOT NULL,
    City Varchar (256) NOT NULL,
    Province Varchar(50) NOT NULL,
    Country Varchar(50) NOT NULL,
    CONSTRAINT Pk_Addresses_id PRIMARY KEY(Id)
);