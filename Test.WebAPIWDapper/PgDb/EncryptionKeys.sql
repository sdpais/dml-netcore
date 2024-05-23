CREATE TABLE  IF NOT EXISTS EncryptionKeys (
    Id serial PRIMARY KEY NOT NULL, 
    KeyString Varchar (256) NOT NULL,
    ExpiryDate Date,
    ReplacedById INT NULL, 
    CreatedDate Date NOT NULL DEFAULT CURRENT_DATE
);

ALTER TABLE EncryptionKeys
ADD CONSTRAINT FK_EncryptionKeys_ReplacedById
FOREIGN KEY (ReplacedById)
REFERENCES EncryptionKeys(Id);
